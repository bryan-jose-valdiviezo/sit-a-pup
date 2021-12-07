using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Final_Rest_API.Models;

namespace Service_Final_Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {
        private readonly sitapupContext _context;

        public UsersController(sitapupContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .Include(e => e.Availabilities)
                .Include(e => e.Pets)
                .Include(e => e.Messages)
                .ToListAsync();
        }

        [HttpGet("{id}/GetUserName")]
        public async Task<ActionResult<string>> GetUserName(int id)
        {
            return await _context.Users.Where(u => u.UserId == id).Select(u => u.UserName).SingleOrDefaultAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users
                .Include(e => e.Availabilities)
                .Include(e => e.Reviews)
                .Include(e => e.Pets)
                .Include(e => e.Messages)
                .FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("GetAvailableSittersForDates")]
        public async Task<ActionResult<IEnumerable<User>>> GetAvailableSitterForDates([FromQuery] string StartDate, [FromQuery] string EndDate)
        {
            Debug.WriteLine("Start Date: " + StartDate);
            Debug.WriteLine("Start Date: " + EndDate);
            string sqlQuery = @"SELECT Users.* FROM Users
                                WHERE Users.UserID NOT IN (
	                                SELECT Appointments.SitterId FROM Appointments
	                                WHERE ({0} <= Appointments.EndDate AND {1} >= Appointments.StartDate)
                                    AND
                                    Appointments.Status == 'accepted'
                                ) AND
                                Users.UserID IN (
	                                SELECT Availabilities.UserId FROM Availabilities
	                                WHERE ({0} >= Availabilities.StartDate AND {1} <= Availabilities.EndDate)
                                ) GROUP BY Users.UserID;";

            return await _context.Users
                .FromSqlRaw(sqlQuery, StartDate, EndDate)
                .ToListAsync();
        }

        [HttpGet("{id}/Appointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsForUsers(int id)
        {
            return await _context.Appointments
                .Where(entity => entity.OwnerId == id || entity.SitterId == id)
                .Include(e => e.Reviews)
                .Include(e => e.Sitter)
                .Include(e => e.Owner)
                .ToListAsync();
        }

        [HttpGet("{id}/Availabilities")]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilitiesForUser(int id)
        {
            return await _context.Availabilities.Where(availability => availability.UserId == id).ToListAsync();
        }

        [HttpGet("LogIn")]
        public async Task<ActionResult<User>> LogIn([FromQuery]string username, [FromQuery]string password)
        {
            var user = await _context.Users.Where(x => x.UserName == username).Where(x => x.Password == password).FirstOrDefaultAsync();
            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
