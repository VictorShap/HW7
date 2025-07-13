using HW7.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HW7.Data.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Comment)
                .HasMaxLength(100);

            builder.Property(e => e.Amount)
                .IsRequired();

            builder.Property(e => e.Date)
                .IsRequired();

            builder.HasOne(e => e.ExpenseCategory)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.ExpenseCategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
