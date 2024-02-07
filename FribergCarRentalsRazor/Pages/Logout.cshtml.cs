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
    public class LogoutModel : PageModel
    {
        private readonly ICustomer customerRep;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LogoutModel(ICustomer customerRep, IHttpContextAccessor httpContextAccessor)
        {
            this.customerRep = customerRep;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            httpContextAccessor.HttpContext.Response.Cookies.Delete("CustomerId");
            httpContextAccessor.HttpContext.Response.Cookies.Delete("UserRole");

            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {

            return RedirectToPage("./Index");
        }
    }
}

