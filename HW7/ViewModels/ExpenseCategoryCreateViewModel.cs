using System.ComponentModel.DataAnnotations;

namespace HW7.ViewModels
{
    public class ExpenseCategoryCreateViewModel
    {
        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
