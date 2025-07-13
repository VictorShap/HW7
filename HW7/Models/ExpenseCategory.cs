namespace HW7.Models
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}
