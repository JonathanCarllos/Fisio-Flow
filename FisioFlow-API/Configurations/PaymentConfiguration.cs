using FisioFlow_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FisioFlow_API.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {

            builder.ToTable("Payments");


            builder.HasKey(p => p.PaymentId);



            builder.Property(p => p.PaymentMethod)
                .IsRequired()
                .HasConversion<int>();


            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<int>();



            builder.Property(p => p.PaymentDate)
                .IsRequired();


            builder.Property(p => p.DueDate)
                .IsRequired();



            builder.Property(p => p.Description)
                .HasMaxLength(500)
                .IsRequired(false);



            builder.Property(p => p.InsuranceName)
                .HasMaxLength(150)
                .IsRequired(false);



            // Payment -> Treatment

            builder.HasOne(p => p.Treatment)
                .WithMany(t => t.Payments)
                .HasForeignKey(p => p.TreatmentId)
                .OnDelete(DeleteBehavior.Restrict);



            // Payment -> Patient

            builder.HasOne(p => p.Patient)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}