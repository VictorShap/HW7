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
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Missing DefaultConnection")));

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<ISummariesService, SummariesService>();

            var app = builder.Build();

            app.UseRouting();
            app.UseAuthorization();

            app.MapDefaultControllerRoute(); 

            app.Run();
        }
    }
}
