using FisioFlow_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FisioFlow_API.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {

            builder.ToTable("Sessions");


            // Primary Key
            builder.HasKey(s => s.SessionId);



            // Data da sessão
            builder.Property(s => s.Date)
                .IsRequired();



            // Horário
            builder.Property(s => s.Time)
                .IsRequired();



            // Duração em minutos
            builder.Property(s => s.Duration)
                .IsRequired();



            // Status
            builder.Property(s => s.Status)
                .IsRequired();



            // Observações
            builder.Property(s => s.Notes)
                .HasMaxLength(1000);



            // Evolução clínica
            builder.Property(s => s.Evolution)
                .HasMaxLength(4000);




            // ==========================
            // Relacionamento Patient
            // ==========================

            builder.HasOne(s => s.Patient)
                .WithMany(p => p.Sessions)
                .HasForeignKey(s => s.PatientId)
                .OnDelete(DeleteBehavior.Restrict);



            // ==========================
            // Relacionamento Physiotherapist
            // ==========================

            builder.HasOne(s => s.Physiotherapist)
                .WithMany(p => p.Sessions)
                .HasForeignKey(s => s.PhysiotherapistId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}