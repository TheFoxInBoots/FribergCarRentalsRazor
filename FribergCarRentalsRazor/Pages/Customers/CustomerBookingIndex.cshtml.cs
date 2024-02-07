using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;
using Microsoft.AspNetCore.Http;

namespace FribergCarRentalsRazor.Pages.Customers
{
    public class CustomerBookingIndexModel : PageModel
    {
        private readonly ICustomer customerRep;
        private readonly IBooking bookingRep;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CustomerBookingIndexModel(ICustomer customerRep, IBooking bookingRep, IHttpContextAccessor httpContextAccessor)
        {
            this.customerRep = customerRep;
            this.bookingRep = bookingRep;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IList<Booking> BookingList { get; set; } = default!;

        [BindProperty]
        public Booking Booking { get; set; }
        [BindProperty]
        public IList<Customer> Customer { get; set; } = new List<Customer>();
        [BindProperty]
        public Customer LoggedInCustomer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var customerId = httpContextAccessor.HttpContext.Request.Cookies["CustomerId"];
            BookingList = await bookingRep.GetBookingsByCustomerId(customerId); // hämtar endast de bokningar från det sparade kund Id:t            
        }
    }
}
