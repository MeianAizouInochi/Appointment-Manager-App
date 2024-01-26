using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentManager.Data;
using AppointmentManager.Data.Models;

namespace AppointmentManager.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
          if (_context.Appointments == null)
          {
              return NotFound("No Data Found.");
          }
            return await _context.Appointments.Where(e=> !e.Deleted && !e.Done).ToListAsync();
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
          if (_context.Appointments == null)
          {
              return NotFound("No Data Found.");
          }
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
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.ID)
            {
                return BadRequest("Wrong Modification Requested.");
            }

            //            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                Appointment? _entry = await _context.Appointments.FirstAsync(e=> e.ID==appointment.ID);

                if (_entry != null)
                {
                    if (!appointment.Title.Equals(_entry.Title))
                    {
                        _entry.Title = appointment.Title;
                    }

                    if (!appointment.Description.Equals(_entry.Description))
                    {
                        _entry.Description = appointment.Description;
                    }

                    if (!appointment.Address.Equals(_entry.Address))
                    {
                        _entry.Address = appointment.Address;
                    }

                    if (appointment.LevelOfImportance != _entry.LevelOfImportance)
                    {
                        _entry.LevelOfImportance = appointment.LevelOfImportance;
                    }

                    if (appointment.Done!=_entry.Done)
                    {
                        _entry.Done = appointment.Done;
                    }

                    if (appointment.Deleted != _entry.Deleted)
                    {
                        _entry.Deleted = appointment.Deleted;
                    }

                    if (appointment.Date != _entry.Date)
                    {
                        _entry.Date = appointment.Date;
                    }

                    if (!appointment.Time.Equals(_entry.Time))
                    {
                        _entry.Time = appointment.Time;
                    }
                }

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
          if (_context.Appointments == null)
          {
              return Problem("Entity set 'AppDbContext.Appointments'  is null.");
          }

            try
            {
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest("Appointment Object Invalid Error." + e.Message);
            }
            
            return CreatedAtAction("GetAppointment", new { id = appointment.ID }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments.FirstAsync(e=>e.ID==id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Deleted = true;

            appointment.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Appointment Deleted Successfully.");
        }

        [HttpPost("filters")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetfilteredData(Filters _filter)
        {
            if (_context.Appointments == null)
            {
                return NotFound("Not Found!");
            }

            List<Appointment> allData = await _context.Appointments.ToListAsync();

            if (_filter.All)
            {
                return allData;
            }
            
            if (_filter.LevelOfImportance!=null)
            {
                allData = allData.Where(e=> e.LevelOfImportance==_filter.LevelOfImportance).ToList();
            }

            if (_filter.SpecifiedDate != null)
            {
                allData = allData.Where(e=>e.Date==_filter.SpecifiedDate).ToList();
            }

            if(_filter.StartDate != null && _filter.EndDate!=null)
            {
                allData = allData.Where(e=>e.Date>=_filter.StartDate && e.Date <=_filter.EndDate).ToList();
            }

            if(_filter.SpecifiedTime != null) 
            {
                allData = allData.Where(e=> e.Time.Equals(_filter.SpecifiedTime)).ToList();
            }

            allData = allData.Where(e => e.Done == _filter.Done).ToList();

            allData = allData.Where(e => e.Deleted == _filter.Deleted).ToList();

            return allData;
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
