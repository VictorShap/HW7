using HW7.Data;
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
                throw new ArgumentNullException(nameof(expense), "category cannot be null");

            }

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);

            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Expense>? GetByIdAsync(int id)
        {
            return await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), "category cannot be null");
            }

            _context.Update(expense);
            await _context.SaveChangesAsync();
        }

       public async Task<List<Expense>> GetFilteredExpensesAsync(ExpenseFilter filter)
        {
            var allExpenses = await _context.Expenses.ToListAsync();
            var filteredExpenses = allExpenses;

            if (filter.SelectedCategoryId.HasValue)
                filteredExpenses = allExpenses.Where(e => e.ExpenseCategoryId == filter.SelectedCategoryId.Value).ToList();

            if (filter.Date?.StartDate != null)
                filteredExpenses = filteredExpenses.Where(e => e.Date >= filter.Date.StartDate.Value).ToList();

            if (filter.Date?.EndDate != null)
                filteredExpenses = filteredExpenses.Where(e => e.Date <= filter.Date.EndDate.Value).ToList();

            return filteredExpenses;
        }

    }
}
