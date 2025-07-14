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

        public async Task<List<Expense>> GetByCategory(ExpenseCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "category cannot be null");
            }

            var expenses = await _context.Expenses.Where(e => e.ExpenseCategoryId == category.Id).ToListAsync();

            return expenses;
        }
    }
}
