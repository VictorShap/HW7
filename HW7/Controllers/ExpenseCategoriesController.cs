using HW7.Data;
using HW7.Models;
using HW7.Services;
using HW7.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HW7.Controllers
{
    public class ExpenseCategoriesController : Controller
    {
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoriesController(IExpenseCategoryService expenseCategoryServie)
        {
            _expenseCategoryService = expenseCategoryServie ?? throw new ArgumentNullException(nameof(expenseCategoryServie), "expenseCategoryServie cannot be null");
        }

        public async Task<IActionResult> Index()
        {
            var catagories = await _expenseCategoryService.GetAllAsync();

            return View(catagories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = new ExpenseCategory
            {
                Name = model.Name
            };

            await _expenseCategoryService.AddAsync(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _expenseCategoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new ExpenseCategoryViewModel() { Id = id, Name = category.Name };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = await _expenseCategoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = model.Name;

            await _expenseCategoryService.UpdateAsync(category);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var result = await _expenseCategoryService.DeleteAsync(id);

            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            if (result.RelatedExpenses?.Any() == true)
            {
                var viewModel = new DeleteCategoryFailedViewModel
                {
                    CategoryId = id,
                    ErrorMessage = result.ErrorMessage,
                    RelatedExpenses = result.RelatedExpenses
                };

                return View("DeleteFailed", viewModel);
            }

            return NotFound();
        }
}
}