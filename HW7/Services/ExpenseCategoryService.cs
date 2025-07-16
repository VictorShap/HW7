using HW7.Data;
using HW7.Models;
using HW7.Models.ServiceResults;
using Microsoft.EntityFrameworkCore;

namespace HW7.Services
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly FinanceDbContext _context;

        public ExpenseCategoryService(FinanceDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "dbContext cannot be null");
        }

        public async Task AddAsync(ExpenseCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "category cannot be null");
            }

            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<DeleteCategoryResult> DeleteAsync(int id)
        {
            var category = await _context.ExpenseCategories
             .Include(c => c.Expenses)
             .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return new DeleteCategoryResult { Success = false, ErrorMessage = "Expense category was not found" };
            }

            if (category.Expenses.Any())
            {
                return new DeleteCategoryResult { Success = false, RelatedExpenses = category.Expenses.ToList(), ErrorMessage = "There are related expenses to this category" };
            }

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return new DeleteCategoryResult() { Success = true };
        }

        public async Task<bool> ForceDeleteAsync(int id)
        {
            var category = await _context.ExpenseCategories.FindAsync(id);

            if (category == null)
            {
                return false;
            }

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ExpenseCategory>> GetAllAsync()
        {
            return await _context.ExpenseCategories.ToListAsync();
        }

        public async Task<ExpenseCategory?> GetByIdAsync(int id)
        {
            return await _context.ExpenseCategories.FindAsync(id);
        }

        public async Task UpdateAsync(ExpenseCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "category cannot be null");
            }

            _context.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
