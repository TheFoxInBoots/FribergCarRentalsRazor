using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Pages.Customers
{
    public class CustomerEditModel : PageModel
    {
        private readonly ICustomer customerRep;

        public CustomerEditModel(ICustomer customerRep)
        {
            this.customerRep = customerRep;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           Customer = await customerRep.GetCustomerById(id.Value);

            if (Customer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {            
                await customerRep.EditCustomer(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.CustomerId))
                {
                    return NotFound(); 
                }
                else
                {
                    throw;
                }
            }
            TempData["Message"] = "Ändringarna har sparats!";

            return RedirectToPage("/Customers/CustomerDetails");
        }

        private bool CustomerExists(int id)
        {
            // Kontrollera om kunden med det angivna ID:t finns i databasen
            return customerRep.GetCustomerById(id) != null;
        }
    }
}
