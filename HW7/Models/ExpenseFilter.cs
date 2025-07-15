using HW7.ViewModels;

namespace HW7.Models
{
    public class ExpenseFilter
    {
        public int? SelectedCategoryId { get; set; }
        public DateRange? Date { get; set; }
    }
}
