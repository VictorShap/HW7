using HW7.ViewModels;

namespace HW7.Services
{
    public interface ISummariesService
    {
        Task<MonthlyOverviewViewModel> GetMonthlyOverviewAsync();
    }
}
