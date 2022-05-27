using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DebutWebAPI.Models;

namespace DebutWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ICitizenRepository citizenRepository;
        private List<CitizenDto> citizenDtos = new List<CitizenDto>();

        public CitizenController(ICitizenRepository citizenRepository)
        {
            this.citizenRepository = citizenRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<CitizenDto>>> Search(string name, Gender? gender)
        {
            try
            {
                var targetCitizens = await citizenRepository.Search(name, gender);
                if (targetCitizens.Any())
                {
                    citizenDtos = citizenRepository.ConvertToCitizenDTO(targetCitizens.ToList());
                    return Ok(citizenDtos);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // GET: api/Citizen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitizenDto>>> GetCitizens()
        {
            try
            {
                var citizens = await citizenRepository.GetCitizens();
                citizenDtos = citizenRepository.ConvertToCitizenDTO(citizens.ToList());
                return Ok(citizenDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/Citizen/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<CitizenDto>> GetCitizen(long id)
        {
            try
            {
                var targerCitizen = await citizenRepository.GetCitizen(id);
                if (targerCitizen == null)
                {
                    return NotFound();
                }
                CitizenDto targetCitizenDto = new CitizenDto(targerCitizen);
                return targetCitizenDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // PUT: api/Citizen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CitizenDto>> PutCitizen(long id, CitizenDto citizenDto)
        {
            try
            {
                var targerCitizen = await citizenRepository.GetCitizen(citizenDto.CitizenId);

                if (targerCitizen == null)
                {
                    return NotFound($"Citizen with Id = {citizenDto.CitizenId} not found!");
                }
                var oldCitizen = await citizenRepository.GetCitizenByEmail(citizenDto.Email);
                if (oldCitizen != null)
                {
                    return BadRequest("This email has already been in use!");
                }

                Citizen citizen = new Citizen(citizenDto);
                citizen = await citizenRepository.UpdateCitizen(citizen);
                CitizenDto targetCitizenDto = new CitizenDto(citizen);

                return targetCitizenDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // POST: api/Citizen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<CitizenDto>> PostCitizen(CitizenDto citizenDto)
        //{
        //    try
        //    {
        //        if (citizenDto == null)
        //        {
        //            return BadRequest();
        //        }

        //        //var targetCitizen = await citizenRepository.GetCitizenByEmail(citizen.Email);

        //        //if (targetCitizen != null)
        //        //{
        //        //    ModelState.AddModelError("email", "This email has already been in use !");
        //        //    return BadRequest(ModelState);
        //        //}
        //        Citizen citizen = new Citizen(citizenDto);
        //        citizen = await citizenRepository.AddCitizen(citizen);
        //        CitizenDto newCitizen = new CitizenDto(citizen);

        //        return CreatedAtAction(nameof(GetCitizen), new { id = newCitizen.CitizenId }, newCitizen);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error retrieving data from the database");
        //    }
        //}

        // DELETE: api/Citizen/5
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<CitizenDto>> DeleteCitizen(long id)
        {
            try
            {
                var targetCitizen = await citizenRepository.GetCitizen(id);

                if (targetCitizen == null)
                {
                    return NotFound($"Citizen with Id = {id} not found!");
                }
                targetCitizen = await citizenRepository.DeleteCitizen(id);
                return new CitizenDto(targetCitizen);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
