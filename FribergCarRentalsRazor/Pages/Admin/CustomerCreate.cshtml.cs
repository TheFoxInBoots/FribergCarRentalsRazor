using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FribergCarRentalsRazor.Data;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentalsRazor.Pages.Admin
{
    public class CustomerCreateModel : PageModel
    {
        private readonly ICustomer customerRep;

        public CustomerCreateModel(ICustomer customerRep)
        {
            this.customerRep = customerRep;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(Customer customer)
        {
            if (!ModelState.IsValid || customerRep == null || Customer == null)
            {
                return Page();
            }

            await customerRep.CreateCustomer(customer);
            return RedirectToPage("./Index");
        }
    }
}
