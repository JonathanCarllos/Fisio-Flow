using FisioFlow_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FisioFlow_API.Data.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.ExpenseId);


            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(150);


            builder.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();


            builder.Property(e => e.Date)
                .IsRequired();


            builder.Property(e => e.Notes)
                .HasMaxLength(1000);


            builder.Property(e => e.Status)
                .HasDefaultValue(true);


            builder.Property(e => e.Category)
                .IsRequired()
                .HasConversion<int>();


            builder.Property(e => e.PaymentMethod)
                .IsRequired()
                .HasConversion<int>();
        }
    }
}