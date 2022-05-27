using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DebutWebAPI.Models;
using DebutWebAPI.Models.Constants;
using DebutWebAPI.Models.Dto;

namespace DebutWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartHomeController : ControllerBase
    {
        private readonly ISmartHomeRepository smartHomeRepository;

        public SmartHomeController(ISmartHomeRepository smartHomeRepository)
        {
            this.smartHomeRepository = smartHomeRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<SmartHome>>> Search(SmartHomeType smartHomeType, string address)
        {
            try
            {
                var targetSmartHome = await smartHomeRepository.Search(smartHomeType, address);

                if (targetSmartHome.Any())
                {
                    return Ok(targetSmartHome);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // GET: api/SmartHome
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmartHome>>> GetSmartHomes()
        {
            try
            {
                return Ok(await smartHomeRepository.GetSmartHomes());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/SmartHome/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<SmartHome>> GetSmartHome(long id)
        {
            try
            {
                var targerSmartHome = await smartHomeRepository.GetSmartHome(id);
                if (targerSmartHome == null)
                {
                    return NotFound();
                }
                return targerSmartHome;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // PUT: api/SmartHome/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<SmartHome>> PutSmartHome(long id, SmartHome smartHome)
        {
            try
            {
                var targerSmartHome = await smartHomeRepository.GetSmartHome(smartHome.SmartHomeId);

                if (targerSmartHome == null)
                {
                    return NotFound($"SmartHome with Id = {smartHome.SmartHomeId} not found!");
                }
                return await smartHomeRepository.UpdateSmartHome(smartHome);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // POST: api/SmartHome
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmartHome>> PostSmartHome(SmartHomeDto smartHomeDto, [FromHeader] string jwt)
        {
            try
            {
                if (smartHomeDto == null)
                {
                    return BadRequest();
                }
                SmartHome smartHome = new SmartHome(smartHomeDto);
                var newSmartHome = await smartHomeRepository.AddSmartHome(smartHome);

                return CreatedAtAction(nameof(PostSmartHome), new { id = newSmartHome.SmartHomeId }, newSmartHome);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // DELETE: api/SmartHome/5
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<SmartHome>> DeleteSmartHome(long id)
        {
            try
            {
                var targetSmartHome = await smartHomeRepository.GetSmartHome(id);

                if (targetSmartHome == null)
                {
                    return NotFound($"SmartHome with Id = {id} not found!");
                }

                return await smartHomeRepository.DeleteSmartHome(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
