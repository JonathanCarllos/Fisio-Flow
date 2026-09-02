using FisioFlow_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Physiotherapist> Physiotherapists { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Session> Sessions { get; set; }

    }
}
