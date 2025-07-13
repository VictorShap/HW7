namespace HW7.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int? ExpenseCategoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
