using Microsoft.AspNetCore.Mvc;
using DebutWebAPI.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace DebutWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly Models.AppContext appContext;
        private readonly ICitizenRepository _citizenRepository;
        private readonly IConfiguration _configuration;
        public Citizen citizen = new();

        public AuthController(IConfiguration config, ICitizenRepository citizenRepository)
        {
            _configuration = config;
            _citizenRepository = citizenRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Citizen>> Register(CitizenDto citizenDto)
        {
            try
            {
                GeneratePasswordHash(citizenDto.Password, out byte[] hash, out byte[] salt);
                citizen.Username = citizenDto.Username;
                citizen.Email = citizenDto.Email;
                citizen.FullName = citizenDto.FullName;
                citizen.Hash = hash;
                citizen.Salt = salt;

                var newCitizen = await _citizenRepository.AddCitizen(citizen);

                return CreatedAtAction(nameof(Register), new { id = newCitizen.CitizenId }, newCitizen);
                //return Ok("Citizen registered!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"error : { e.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            try
            {
                if (username == null)
                {
                    return BadRequest("Username is missing!");
                }
                if (password == null)
                {
                    return BadRequest("Password is missing!");
                }
                citizen = await _citizenRepository.GetCitizenByUsername(username);

                if (citizen == null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        "Username not found!");
                }
                if (!VerifyPasswordHash(password, citizen.Hash, citizen.Salt))
                {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        "Citizen unauthenticated!");
                }
                string jwtToken = GenerateToken(citizen);
                return jwtToken;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        private string GenerateToken(Citizen citizen)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("username", citizen.Username),
                new Claim("Id", citizen.CitizenId.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:SecretKey").Value));

            var cre = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: cre,
                notBefore: DateTime.Now.AddDays(1)
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void GeneratePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(citizen.Salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hash);
            }
        }
    }
}
