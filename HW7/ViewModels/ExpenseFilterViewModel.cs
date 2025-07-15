using HW7.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW7.ViewModels
{
    public class ExpenseFilterViewModel
    {
        public int? SelectedCategoryId { get; set; }
        public DateRange? Date { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public List<Expense> FilteredExpenses { get; set; }

        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
    }
}
