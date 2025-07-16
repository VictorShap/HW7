using Microsoft.AspNetCore.Identity;

namespace HW7.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ExpenseCategory> ExpenseCategories { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
