using HW7.Data;
using HW7.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HW7.Services
{
    public class SummariesService : ISummariesService
    {
        private readonly FinanceDbContext _context;

        public SummariesService(FinanceDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "context cannot be null");
        }

        public async Task<MonthlyOverviewViewModel> GetMonthlyOverviewAsync()
        {
            var monthlyStats = await _context.Expenses
                .Include(e => e.ExpenseCategory)
                .GroupBy(e => new { e.Date.Year, e.Date.Month, e.ExpenseCategory.Name })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new MonthlyCategoryStatsViewModel
                {
                    MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy"),
                    CategoryName = g.Key.Name,
                    ExpenseCount = g.Count(),
                    TotalAmount = g.Sum(e => e.Amount)
                })
                .ToListAsync();

            return new MonthlyOverviewViewModel
            {
                MonthlyStats = monthlyStats
            };
        }
    }
}
