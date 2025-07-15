using HW7.Models;

namespace HW7.ViewModels
{
    public class DeleteCategoryFailedViewModel
    {
        public int CategoryId { get; set; }
        public string ErrorMessage { get; set; }
        public List<Expense> RelatedExpenses { get; set; } = new();
    }
}
