using System.ComponentModel.DataAnnotations;

namespace HW7.ViewModels
{
    public class ExpenseCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
