using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FribergCarRentalsRazor.Data;
namespace FribergCarRentalsRazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<FribergCarRentalsRazorContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FribergCarRentalsRazorContext") ?? throw new InvalidOperationException("Connection string 'FribergCarRentalsRazorContext' not found.")));

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // behövs för att använda Cookies

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<ICustomer, CustomerRepository>();
            builder.Services.AddTransient<ICar, CarRepository>();
            builder.Services.AddTransient<IBooking, BookingRepository>();
            builder.Services.AddTransient<IAdmin, AdminRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
