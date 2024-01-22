using AppointmentManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManager.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
