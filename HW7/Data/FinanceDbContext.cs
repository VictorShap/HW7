using HW7.Data.Configurations;
using HW7.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HW7.Data
{
    public class FinanceDbContext : IdentityDbContext<ApplicationUser>
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

        public override int SaveChanges()
        {
            var deletedUsers = ChangeTracker.Entries<ApplicationUser>()
                .Where(e => e.State == EntityState.Deleted)
                .Select(e => e.Entity)
                .ToList();

            foreach (var user in deletedUsers)
            {
                var userExpenses = Expenses.Where(e => e.UserId == user.Id).ToList();
                Expenses.RemoveRange(userExpenses);

                var userCategories = ExpenseCategories
                    .Where(ec => ec.UserId == user.Id && !ec.isSystem)
                    .ToList();
                ExpenseCategories.RemoveRange(userCategories);
            }

            return base.SaveChanges();
        }
    }
}
