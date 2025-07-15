using HW7.Models;
using HW7.Models.ServiceResults;

namespace HW7.Services
{
    public interface IExpenseCategoryService
    {
        Task<List<ExpenseCategory>> GetAllAsync();
        Task<ExpenseCategory?> GetByIdAsync(int id);
        Task AddAsync(ExpenseCategory category);
        Task UpdateAsync(ExpenseCategory category);
        Task<DeleteCategoryResult> DeleteAsync(int id);
    }
}
