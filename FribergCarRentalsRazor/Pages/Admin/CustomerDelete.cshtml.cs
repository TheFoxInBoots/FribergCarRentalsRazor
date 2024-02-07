using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Pages.Admin
{
    public class CustomerDeleteModel : PageModel
    {
        private readonly ICustomer customerRep;

        public CustomerDeleteModel(ICustomer customerRep)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Kontrollerar om CarId är null eller om customerRep är null
            if (id == null || customerRep == null)
            {
                return NotFound(); // Returnera NotFound om ID är null eller om customerRep är null
            }

            // Hämtar kunden från CustomerRepository med det angivna CarId:t
            var customer = await customerRep.GetCustomerById(id.Value);

            // Om kunden finns
            if (customer != null)
            {
                Customer = customer; // Tilldela den befintliga kunden till egenskapen Customer

                // Ta bort kunden från CustomerRepository
                customerRep.DeleteCustomer(Customer);

                TempData["Message"] = "Kunden har tagits bort!";
            }

            return RedirectToPage("./Index"); // Redirect till indexsidan efter framgångsrik borttagning
        }
    }
}
