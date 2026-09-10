using FisioFlow_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(
            DbContextOptions<AppDbContext> options
        ) : base(options)
        {
        }



        public DbSet<Patient> Patients { get; set; }

        public DbSet<Physiotherapist> Physiotherapists { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Payment> Payments { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);



            // Aplicar todas as Fluent API automaticamente

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly
            );



            // Índices únicos

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.CPF)
                .IsUnique();


            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.Email)
                .IsUnique();



            modelBuilder.Entity<Physiotherapist>()
                .HasIndex(p => p.CPF)
                .IsUnique();


            modelBuilder.Entity<Physiotherapist>()
                .HasIndex(p => p.Email)
                .IsUnique();


            modelBuilder.Entity<Physiotherapist>()
                .HasIndex(p => p.Crefito)
                .IsUnique();

        }
    }
}