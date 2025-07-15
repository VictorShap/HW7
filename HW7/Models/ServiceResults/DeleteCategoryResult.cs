namespace HW7.Models.ServiceResults
{
    public class DeleteCategoryResult
    {
        public bool Success { get; set; }
        public List<Expense> RelatedExpenses { get; set; } = new();
        public string ErrorMessage { get; set; }
    }
}
