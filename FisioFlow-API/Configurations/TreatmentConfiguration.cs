using FisioFlow_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FisioFlow_API.Configurations
{
    public class TreatmentConfiguration
        : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> builder)
        {

            builder.ToTable("Treatments");


            builder.HasKey(t => t.TreatmentId);



            builder.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);



            builder.Property(t => t.Diagnosis)
                .IsRequired()
                .HasColumnType("TEXT");



            builder.Property(t => t.TotalSessions)
                .IsRequired();



            builder.Property(t => t.CompletedSessions)
                .IsRequired();



            builder.Property(t => t.Exercises)
                .HasColumnType("TEXT");



            builder.Property(t => t.Observations)
                .HasColumnType("TEXT");



            builder.Property(t => t.Status)
                .IsRequired();



            builder.Property(t => t.StartDate)
                .IsRequired();



            builder.Property(t => t.EndDate)
                .IsRequired();




            // =========================
            // Patient -> Treatment
            // =========================

            builder.HasOne(t => t.Patient)
                .WithMany(p => p.Treatments)
                .HasForeignKey(t => t.PatientId)
                .OnDelete(DeleteBehavior.Restrict);




            // =========================
            // Physiotherapist -> Treatment
            // =========================

            builder.HasOne(t => t.Physiotherapist)
                .WithMany(p => p.Treatments)
                .HasForeignKey(t => t.PhysiotherapistId)
                .OnDelete(DeleteBehavior.Restrict);




            // =========================
            // Treatment -> Payments
            // =========================

            builder.HasMany(t => t.Payments)
                .WithOne(p => p.Treatment)
                .HasForeignKey(p => p.TreatmentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}