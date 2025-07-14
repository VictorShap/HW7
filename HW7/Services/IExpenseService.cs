using HW7.Models;

namespace HW7.Services
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetAllAsync();
        Task AddAsync(Expense expense);
        Task<bool> DeleteAsync(int id);
        Task<Expense>? GetByIdAsync(int id);
        Task UpdateAsync(Expense expense);
        Task<List<Expense>> GetByCategory(ExpenseCategory category);
    }
}
