using HW7.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Controllers
{
    public class SummariesController : Controller
    {
        private readonly ISummariesService _statisticService;

        public SummariesController(ISummariesService statisticService)
        {
            _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService), "statistic service cannot be null");
        }

        public async Task<IActionResult> Index()
        {
            var summary = await _statisticService.GetMonthlyOverviewAsync();

            return View(summary);
        }
    }
}
