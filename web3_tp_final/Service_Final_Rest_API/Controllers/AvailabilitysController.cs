using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Final_Rest_API.DTO;
using Service_Final_Rest_API.Models;

namespace Service_Final_Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilitysController : ControllerBase
    {
        private readonly sitapupContext _context;

        public AvailabilitysController(sitapupContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/AvailabilitiesFor")]
        public async Task<ActionResult<bool>> GetAvailabilitiesForDate(long id, [FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate)
        {
            Debug.WriteLine("DateStart: " + StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            Debug.WriteLine("DateEnd: " + EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            Debug.WriteLine("ID: " + id);
            return _context.Availabilities.FromSqlRaw(@"SELECT * FROM Availabilities
                                                        WHERE UserId == {0} AND 
                                                        Availabilities.StartDate <= {1} AND
                                                        Availabilities.EndDate >= {2}", 
                                                        id, 
                                                        StartDate.ToString("yyyy-MM-dd HH:mm:ss"), 
                                                        EndDate.ToString("yyyy-MM-dd HH:mm:ss")).Any();

        }

        // GET: api/Availabilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilities()
        {
            return await _context.Availabilities.ToListAsync();
        }

        // GET: api/Availabilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Availability>> GetAvailability(long id)
        {
            var availability = await _context.Availabilities.FindAsync(id);

            if (availability == null)
            {
                return NotFound();
            }

            return availability;
        }

        // PUT: api/Availabilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvailability(long id, Availability availability)
        {
            if (id != availability.AvailabilityId)
            {
                return BadRequest();
            }

            _context.Entry(availability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailabilityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Availabilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Availability>> PostAvailability([FromBody] AvailabilityDTO availabilityDTO)
        {
            Debug.WriteLine("Form StartDate: " + availabilityDTO.StartDate.ToString());
            Debug.WriteLine("Form EndDate: " + availabilityDTO.EndDate.ToString());
            Debug.WriteLine("Form userId: " + availabilityDTO.UserId);
            Availability availability = new Availability {
                UserId = availabilityDTO.UserId,
                StartDate = availabilityDTO.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = availabilityDTO.EndDate.ToString("yyyy-MM-dd HH:mm:ss")
            };

            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();

             return CreatedAtAction("GetAvailability", new { id = availability.AvailabilityId }, availability);
        }

        [HttpPost("CreateAvailability")]
        public async Task<ActionResult<Availability>> PostAvailabilityDTO([FromBody] AvailabilityDTO availabilityDTO)
        {
            Debug.WriteLine("Form StartDate: " + availabilityDTO.StartDate.ToString());
            Debug.WriteLine("Form EndDate: " + availabilityDTO.EndDate.ToString());
            Debug.WriteLine("Form userId: " + availabilityDTO.UserId);
            Availability availability = new Availability
            {
                UserId = availabilityDTO.UserId,
                StartDate = availabilityDTO.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = availabilityDTO.EndDate.ToString("yyyy-MM-dd HH:mm:ss")
            };

            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();

            return availability;
        }

        [HttpPost("DeleteAvailability")]
        public async Task<IActionResult> DeleteAvailabilityDTO([FromBody] AvailabilityDTO availabilityDTO)
        {
            Availability availability = new Availability
            {
                //AvailabilityId = availabilityDTO.
                UserId = availabilityDTO.UserId,
                StartDate = availabilityDTO.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = availabilityDTO.EndDate.ToString("yyyy-MM-dd HH:mm:ss")
            };

            _context.Availabilities.Remove(availability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AvailabilityExists(long id)
        {
            return _context.Availabilities.Any(e => e.AvailabilityId == id);
        }
    }
}
