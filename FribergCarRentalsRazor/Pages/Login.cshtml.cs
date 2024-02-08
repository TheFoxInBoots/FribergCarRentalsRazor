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
    public class LoginModel : PageModel
    {
        private readonly ICustomer customerRep;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LoginModel(ICustomer customerRep, IHttpContextAccessor httpContextAccessor)
        {
            this.customerRep = customerRep;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var customer = await customerRep.GetLoggedInCustomer(Customer.Email, Customer.Password); // Kontrollerar att kunden som loggar in finns i databasen

            if (customer == null)
            {
                TempData["Message2"] = "Användarnamn eller lösenord är felaktigt!";
                return Page();
            }
            else
            {
                var loggedId = await customerRep.GetLoggedInCustomerId(Customer.Email); // Hämtar CustomerId från kunden med hjälp av den inloggade emailadressen 
                string customerId = loggedId.ToString(); // Konverterar CustomerId från en int till en string så att den kan sparas i Cookien
                string userRole = "Customer";

                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddHours(0.5); // Sparar Cookien i en halvtimme
                httpContextAccessor.HttpContext.Response.Cookies.Append("CustomerId", customerId, options);
                httpContextAccessor.HttpContext.Response.Cookies.Append("UserRole", userRole, options);

                TempData["Message"] = "Du har loggats in!";
            }

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostRegisterAsync(Customer customer)
        {
            if (!ModelState.IsValid || customerRep == null || Customer == null)
            {
                return Page();
                TempData["Message2"] = "Ny kund kunde inte skapas!";
            }

            await customerRep.CreateCustomer(customer);

            TempData["Message"] = "Ny kund har skapats framgångsrikt!";

            return RedirectToPage("/Login");
        }
    }
}
