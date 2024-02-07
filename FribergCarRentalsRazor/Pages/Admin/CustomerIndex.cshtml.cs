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
    public class CustomerIndexModel : PageModel
    {
        private readonly ICustomer customerRep;

        public CustomerIndexModel(ICustomer customerRep)
        {
            this.customerRep = customerRep;
        }

        public Task<List<Customer>> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Customer = customerRep.GetAllCustomers();
        }
    }
}
