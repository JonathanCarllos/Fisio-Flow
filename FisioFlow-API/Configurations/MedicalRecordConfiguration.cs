using FisioFlow_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FisioFlow_API.Data.Configurations
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(x => x.MedicalRecordId);


            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(150);


            builder.Property(x => x.Content)
                .IsRequired()
                .HasColumnType("text");


            builder.Property(x => x.FunctionalDiagnosis)
                .HasColumnType("text");


            builder.Property(x => x.FileUrl)
                .HasMaxLength(500);



            // Enum RecordType salvo como inteiro
            builder.Property(x => x.RecordType)
                .HasConversion<int>();



            // Patient 1:N MedicalRecord
            builder.HasOne(x => x.Patient)
                .WithMany(x => x.MedicalRecords)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);



            // Physiotherapist 1:N MedicalRecord
            builder.HasOne(x => x.Physiotherapist)
                .WithMany(x => x.MedicalRecords)
                .HasForeignKey(x => x.PhysiotherapistId)
                .OnDelete(DeleteBehavior.Restrict);



            // Session 1:N MedicalRecord (opcional)
            builder.HasOne(x => x.Session)
                .WithMany(x => x.MedicalRecords)
                .HasForeignKey(x => x.SessionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}