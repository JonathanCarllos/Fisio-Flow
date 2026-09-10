using FisioFlow_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FisioFlow_API.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.PatientId);


            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);


            builder.Property(x => x.CPF)
                .IsRequired()
                .HasMaxLength(14);


            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);


            builder.Property(x => x.RG)
                .IsRequired()
                .HasMaxLength(20);


            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(20);


            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(200);


            builder.Property(x => x.Neighborhood)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(2);


            builder.Property(x => x.PostalCode)
                .IsRequired()
                .HasMaxLength(9);


            builder.Property(x => x.Insurance)
                .HasMaxLength(150);


            builder.Property(x => x.MedicalHistory)
                .HasColumnType("text");


            builder.Property(x => x.Notes)
                .HasColumnType("text");


            builder.Property(x => x.Status)
                .HasDefaultValue(true);



            // Patient 1:N Treatment
            builder.HasMany(x => x.Treatments)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);



            // Patient 1:N Session
            builder.HasMany(x => x.Sessions)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);



            // Patient 1:N MedicalRecord
            builder.HasMany(x => x.MedicalRecords)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}