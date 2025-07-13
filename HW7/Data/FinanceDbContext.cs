using HW7.Data.Configurations;
using HW7.Models;
using Microsoft.EntityFrameworkCore;

namespace HW7.Data
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
        {

        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
