using HW7.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HW7.Data.Configurations
{
    public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
