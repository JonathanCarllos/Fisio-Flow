using FisioFlow_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PhysiotherapistConfiguration
    : IEntityTypeConfiguration<Physiotherapist>
{
    public void Configure(EntityTypeBuilder<Physiotherapist> builder)
    {

        builder.ToTable("Physiotherapists");


        builder.HasKey(p => p.PhysiotherapistId);


        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);


        builder.Property(p => p.Crefito)
            .IsRequired()
            .HasMaxLength(30);


        builder.Property(p => p.Specialty)
            .IsRequired()
            .HasMaxLength(100);


        builder.Property(p => p.CPF)
            .IsRequired()
            .HasMaxLength(14);


        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(150);


        builder.Property(p => p.RG)
            .HasMaxLength(20);


        builder.Property(p => p.Phone)
            .HasMaxLength(20);


        builder.Property(p => p.Address)
            .HasMaxLength(200);


        builder.Property(p => p.Neighborhood)
            .HasMaxLength(100);


        builder.Property(p => p.City)
            .HasMaxLength(100);


        builder.Property(p => p.State)
            .HasMaxLength(2);


        builder.Property(p => p.PostalCode)
            .IsRequired()
            .HasMaxLength(9);


        builder.Property(p => p.AvailableHours)
            .HasMaxLength(200);


        builder.Property(p => p.Color)
            .HasMaxLength(50);



        // Physiotherapist -> Treatment

        builder.HasMany(p => p.Treatments)
            .WithOne(t => t.Physiotherapist)
            .HasForeignKey(t => t.PhysiotherapistId)
            .OnDelete(DeleteBehavior.Restrict);



        // Physiotherapist -> Session

        builder.HasMany(p => p.Sessions)
            .WithOne(s => s.Physiotherapist)
            .HasForeignKey(s => s.PhysiotherapistId)
            .OnDelete(DeleteBehavior.Restrict);



        // Physiotherapist -> MedicalRecord

        builder.HasMany(p => p.MedicalRecords)
            .WithOne(m => m.Physiotherapist)
            .HasForeignKey(m => m.PhysiotherapistId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}