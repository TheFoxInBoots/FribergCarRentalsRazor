using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FribergCarRentalsRazor.Data;
using Microsoft.AspNetCore.Http;

namespace FribergCarRentalsRazor.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IAdmin adminRep;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AdminModel(IAdmin adminRep, IHttpContextAccessor httpContextAccessor)
        {
            this.adminRep = adminRep;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AdminUser Admin { get; set; } = default!;

        public async Task<IActionResult> OnPostAdminAsync()
        {
            var admin = await adminRep.GetLoggedInAdmin(Admin.Email, Admin.Password); // Kontrollerar att admin finns i databasen

            if (admin == null)
            {
                TempData["Message2"] = "Användarnamn eller lösenord är felaktigt!";
                return Page();
            }
            else
            {
                httpContextAccessor.HttpContext.Response.Cookies.Delete("CustomerId");
                httpContextAccessor.HttpContext.Response.Cookies.Delete("UserRole");

                string userRole = "Admin";
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddHours(0.5); // Sparar Cookien i en halvtimme
                httpContextAccessor.HttpContext.Response.Cookies.Append("UserRole", userRole, options);

                TempData["Message"] = "Du har loggats in!";
            }

            return RedirectToPage("/Index");
        }
    }
}
