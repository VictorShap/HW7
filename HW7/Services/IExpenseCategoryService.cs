using HW7.Models;

namespace HW7.Services
{
    public interface IExpenseCategoryService
    {
        Task<List<ExpenseCategory>> GetAllAsync();
        Task<ExpenseCategory?> GetByIdAsync(int id);
        Task AddAsync(ExpenseCategory category);
        Task UpdateAsync(ExpenseCategory category);
        Task<bool> DeleteAsync(int id);
    }
}
