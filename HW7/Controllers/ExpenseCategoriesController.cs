using HW7.Data;
using HW7.Models;
using HW7.Services;
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
            if (expenseCategoryServie == null)
            {
                throw new ArgumentNullException(nameof(expenseCategoryServie), "expenseCategoryServie cannot be null");
            }
            _expenseCategoryService = expenseCategoryServie;
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
        public async Task<IActionResult> Create(ExpenseCategory expenseCategory)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();// TODO: change  
            }

            await _expenseCategoryService.AddAsync(expenseCategory);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _expenseCategoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _expenseCategoryService.DeleteAsync(id))
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }



    }
}