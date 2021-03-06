using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
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
    public class AppointmentsController : ControllerBase
    {
        private readonly sitapupContext _context;

        public AppointmentsController(sitapupContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments
                .Include(e => e.Owner)
                .Include(e => e.Sitter)
                .ToListAsync();
        }

        [HttpGet("{id}/Pets")]
        public async Task<IEnumerable<Pet>> GetPetsFromAppointment(long id)
        {
            Appointment appointment = await _context.Appointments
                .Where(e => e.AppointmentId == id)
                .Include(e => e.PetAppointments)
                .ThenInclude(x => x.Pet)
                .FirstOrDefaultAsync();
            List<Pet> pets = appointment.PetAppointments.Select(petAppointment => petAppointment.Pet).ToList();

            return pets;
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(long id)
        {
            var appointment = await _context.Appointments
                .Include(e => e.Reviews)
                .Include(e => e.Owner)
                .Include(e => e.Sitter)
                .FirstOrDefaultAsync(d => d.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        [HttpGet("UpdateAppointmentStatus")]
        public async Task<IActionResult> UpdateAppointmentStatus([FromQuery] long id, [FromQuery] string newStatus)
        {
            Appointment appointment = _context.Appointments.Find(id);
            appointment.Status = newStatus;
            _context.Entry(appointment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                if (appointment.Status == "accepted")
                {
                    Debug.WriteLine("Appointment is accepted, making changes");
                    await _context.Database.ExecuteSqlRawAsync(@"UPDATE Appointments SET Status == 'cancelled'
	                                                            WHERE
	                                                            Appointments.AppointmentID != {0}
	                                                            AND
                                                                Appointments.SitterId == {1}
                                                                AND
	                                                            Appointments.Status LIKE '%pending%'
	                                                            AND
                                                                Appointments.EndDate >= {2}
                                                                AND
                                                                Appointments.StartDate <= {3}",
                                                                id,
                                                                appointment.SitterId,
                                                                appointment.StartDate.ToString(),
                                                                appointment.EndDate.ToString());
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(long id, Appointment appointment)
        {

            if (id != appointment.AppointmentId)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                if (appointment.Status == "accepted")
                {
                    Debug.WriteLine("Appointment status accepted, making changes");
                    await _context.Database.ExecuteSqlRawAsync(@"UPDATE Appointments SET Appointments.Status == 'cancelled'
	                                                            WHERE
	                                                            Appointments.AppointmentID != {0}
	                                                            AND
                                                                Appointments.SitterId == {1}
                                                                AND
	                                                            Appointments.Status LIKE '%pending%'
	                                                            AND
	                                                            (Appointments.StartDate BETWEEN {2} AND {3})
	                                                            AND
	                                                            (Appointments.EndDate BETWEEN {2} AND {3})
	                                                            AND
	                                                            (Appointments.StartDate <= {2} AND Appointments.EndDate >= {3});",
                                                                id,
                                                                appointment.SitterId,
                                                                appointment.StartDate,
                                                                appointment.EndDate);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.AppointmentId }, appointment);
        }

        [HttpPost("CreateAppointment")]
        public async Task<ActionResult<Appointment>> PostTest([FromBody] AppointmentDTO appointmentForm)
        {
            PetAppointment petAppointment;
            Appointment appointment = new Appointment
            {
                OwnerId = appointmentForm.OwnerId,
                SitterId = appointmentForm.SitterId,
                StartDate = appointmentForm.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = appointmentForm.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                IsActive = 0
            };

            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            foreach (var petId in appointmentForm.PetIds)
            {
                petAppointment = new PetAppointment
                {
                    PetId = petId,
                    AppointmentId = appointment.AppointmentId
                };
                _context.PetAppointments.Add(petAppointment);
            }

            await _context.SaveChangesAsync();

            return appointment;
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(long id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
