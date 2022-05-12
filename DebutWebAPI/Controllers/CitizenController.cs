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

        public CitizenController(ICitizenRepository citizenRepository)
        {
            this.citizenRepository = citizenRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Citizen>>> Search(string name, Gender? gender)
        {
            try
            {
                var targetCitizen = await citizenRepository.Search(name, gender);

                if (targetCitizen.Any())
                {
                    return Ok(targetCitizen);
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
        public async Task<ActionResult<IEnumerable<Citizen>>> GetCitizens()
        {
            try
            {
                return Ok(await citizenRepository.GetCitizens());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/Citizen/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Citizen>> GetCitizen(long id)
        {
            try
            {
                var targerCitizen = await citizenRepository.GetCitizen(id);
                if (targerCitizen == null)
                {
                    return NotFound();
                }
                return targerCitizen;
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
        public async Task<ActionResult<Citizen>> PutCitizen(long id, Citizen citizen)
        {
            try
            {
                var targerCitizen = await citizenRepository.GetCitizen(citizen.CitizenId);

                if (targerCitizen == null)
                {
                    return NotFound($"Citizen with Id = {citizen.CitizenId} not found!");
                }
                var oldCitizen = await citizenRepository.GetCitizenByEmail(citizen.Email);
                if (oldCitizen != null)
                {
                    return BadRequest("This email has already been in use!");
                }
                return await citizenRepository.UpdateCitizen(citizen);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // POST: api/Citizen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Citizen>> PostCitizen(Citizen citizen)
        {
            try
            {
                if (citizen == null)
                {
                    return BadRequest();
                }

                //var targetCitizen = await citizenRepository.GetCitizenByEmail(citizen.Email);

                //if (targetCitizen != null)
                //{
                //    ModelState.AddModelError("email", "This email has already been in use !");
                //    return BadRequest(ModelState);
                //}

                var newCitizen = await citizenRepository.AddCitizen(citizen);

                return CreatedAtAction(nameof(GetCitizen), new { id = newCitizen.CitizenId }, newCitizen);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // DELETE: api/Citizen/5
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<Citizen>> DeleteCitizen(long id)
        {
            try
            {
                var targetCitizen = await citizenRepository.GetCitizen(id);

                if (targetCitizen == null)
                {
                    return NotFound($"Citizen with Id = {id} not found!");
                }

                return await citizenRepository.DeleteCitizen(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
