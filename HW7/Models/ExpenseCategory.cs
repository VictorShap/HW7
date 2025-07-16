namespace HW7.Models
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool isSystem { get; set; } = false;

        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
