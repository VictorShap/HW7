using HW7.Models;
using HW7.Services;
using HW7.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseCategoryService _categoryService;

        public ExpensesController(IExpenseService expenseService, IExpenseCategoryService categoryService)
        {
            _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService), "expenseService cannot be null");
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService), "categoryService cannot be null");
        }

        public async Task<IActionResult> Index(ExpenseFilterViewModel model)
        {
            var allExpenses = await _expenseService.GetAllAsync();
            var filteredExpenses = allExpenses;

            if (model.SelectedCategoryId.HasValue)
                filteredExpenses = allExpenses.Where(e => e.ExpenseCategoryId == model.SelectedCategoryId.Value).ToList();

            if (model.DateRange.HasValue)
            {
                filteredExpenses = allExpenses
       .Where(e => e.Date >= model.DateRange.Value.startDate && e.Date <= model.DateRange.Value.endDate)
       .ToList();
            }

            var categories = await _categoryService.GetAllAsync();

            var viewModel = new ExpenseFilterViewModel
            {
                SelectedCategoryId = model.SelectedCategoryId,
                DateRange = model.DateRange,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),
                FilteredExpenses = filteredExpenses,
                TotalAmount = allExpenses.Sum(e => e.Amount),
                TotalCount = allExpenses.Count
            };

            return View(viewModel);
        }

        public IActionResult FilterByCategory(int categoryId)
        {
            return RedirectToAction("Index", new ExpenseFilterViewModel()
            {
                SelectedCategoryId = categoryId
            });
        }


        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            var model = new ExpenseViewModel
            {
                Date = DateTime.Today,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = (await _categoryService.GetAllAsync()).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(model);
            }

            var expense = new Expense
            {
                Comment = model.Comment,
                Amount = model.Amount,
                Date = model.Date,
                ExpenseCategoryId = model.ExpenseCategoryId
            };

            await _expenseService.AddAsync(expense);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _expenseService.DeleteAsync(id))
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expenseService.GetByIdAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllAsync();

            var viewModel = new ExpenseViewModel
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Date = expense.Date,
                Comment = expense.Comment,
                ExpenseCategoryId = expense.ExpenseCategoryId.HasValue ? expense.ExpenseCategoryId.Value : -1,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = (await _categoryService.GetAllAsync())
       .Select(c => new SelectListItem
       {
           Value = c.Id.ToString(),
           Text = c.Name
       }).ToList();

                return View(viewModel);
            }

            var category = await _categoryService.GetByIdAsync(viewModel.ExpenseCategoryId);

            if (category == null)
            {
                ModelState.AddModelError("ExpenseCategoryId", "Category was not found");

                return View(viewModel);
            }

            var expense = await _expenseService.GetByIdAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            expense.Amount = viewModel.Amount;
            expense.Comment = viewModel.Comment;
            expense.Date = viewModel.Date;
            expense.ExpenseCategoryId = viewModel.ExpenseCategoryId;

            await _expenseService.UpdateAsync(expense);

            return RedirectToAction(nameof(Index));
        }
    }
}
