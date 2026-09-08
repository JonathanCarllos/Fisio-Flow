using FisioFlow_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Physiotherapist> Physiotherapists { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Payment> Payments { get; set; }

    }
}
