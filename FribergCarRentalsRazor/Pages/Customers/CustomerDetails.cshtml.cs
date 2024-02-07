using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Pages.Customers
{
    public class CustomerDetailsModel : PageModel
    {
        private readonly ICustomer customerRep;

        public CustomerDetailsModel(ICustomer customerRep)
        {
            this.customerRep = customerRep;
        }
      
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
    }
}
