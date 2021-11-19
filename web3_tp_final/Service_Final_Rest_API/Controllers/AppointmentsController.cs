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
            return await _context.Appointments.Include(e => e.PetAppointments).ToListAsync();
        }

        [HttpGet("{id}/Pets")]
        public async Task<IEnumerable<Pet>> GetPetsFromAppointment(long id)
        {
            Appointment appointment = await _context.Appointments
                .Include(e => e.PetAppointments)
                .ThenInclude(x => x.Pet)
                .FirstOrDefaultAsync();
            List<Pet> pets = new List<Pet>();
            foreach(var petAppointment in appointment.PetAppointments)
            {
                pets.Add(petAppointment.Pet);
            }

            return pets;
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
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
            Appointment appointment = new Appointment();

            appointment.OwnerId = appointmentForm.OwnerId;
            appointment.SitterId = appointmentForm.SitterId;
            appointment.StartDate = appointmentForm.StartDate.ToString();
            appointment.EndDate = appointmentForm.EndDate.ToString();
            appointment.IsActive = 0;
            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            foreach (var petId in appointmentForm.PetIds)
            {
                petAppointment = new PetAppointment();
                petAppointment.PetId = petId;
                petAppointment.AppointmentId = appointment.AppointmentId;
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
