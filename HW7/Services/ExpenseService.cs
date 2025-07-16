using HW7.Data;
using HW7.Exceptions;
using HW7.Models;
using Microsoft.EntityFrameworkCore;

namespace HW7.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly FinanceDbContext _context;

        public ExpenseService(FinanceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "dbContext cannot be null");
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await _context.Expenses.Include(e => e.ExpenseCategory).ToListAsync();
        }

        public async Task AddAsync(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), "expense cannot be null");
            }

            var category = await _context.ExpenseCategories.FindAsync(expense.ExpenseCategoryId);

            if (category == null)
            {
                throw new InvalidCategoryException("Such a category doesnt exist");
            }

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task UpdateAsync(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), "category cannot be null");
            }

            var category = await _context.ExpenseCategories.FindAsync(expense.ExpenseCategoryId);

            if (category == null)
            {
                throw new InvalidCategoryException("Such a category doesnt exist");
            }

            _context.Update(expense);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Expense>> GetFilteredExpensesAsync(ExpenseFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "filter cannot be null");
            }

            var query = _context.Expenses.AsQueryable();

            if (filter.SelectedCategoryIds != null && filter.SelectedCategoryIds.Any())
            {
                query = query.Where(e => filter.SelectedCategoryIds.Contains(e.ExpenseCategoryId));
            }

            if (filter.Date != null)
            {
                if (filter.Date.StartDate.HasValue)
                {
                    query = query.Where(e => e.Date >= filter.Date.StartDate.Value);
                }
                if (filter.Date.EndDate.HasValue)
                {
                    query = query.Where(e => e.Date <= filter.Date.EndDate.Value);
                }

            }

            return await query.ToListAsync();
        }

    }
}
