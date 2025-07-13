using HW7.Data;
using HW7.Services;
using Microsoft.EntityFrameworkCore;

namespace HW7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<FinanceDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();

            var app = builder.Build();

            app.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.MapDefaultControllerRoute();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
