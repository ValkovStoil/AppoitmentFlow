using AppoitmentFlow.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppoitmentFlow.API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Business> Businesses { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
