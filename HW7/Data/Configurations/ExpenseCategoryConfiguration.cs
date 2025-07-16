using HW7.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

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

            builder.Property(c => c.isSystem)
              .IsRequired();

            builder.HasOne(c => c.User)
           .WithMany(u => u.ExpenseCategories)
           .HasForeignKey(c => c.UserId)
           .OnDelete(DeleteBehavior.SetNull) 
           .IsRequired(false);

            builder.HasData(
           new ExpenseCategory { Id = 1, Name = "Food", isSystem = true },
           new ExpenseCategory { Id = 2, Name = "Transport", isSystem = true },
           new ExpenseCategory { Id = 3, Name = "Cellular communication", isSystem = true },
           new ExpenseCategory { Id = 4, Name = "Internet", isSystem = true },
           new ExpenseCategory { Id = 5, Name = "Entertainment", isSystem = true }
       );
        }
    }
}
