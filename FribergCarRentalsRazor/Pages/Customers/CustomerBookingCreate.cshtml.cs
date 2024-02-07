using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Pages.Customers
{
    public class CustomerBookingCreateModel : PageModel
    {
        private readonly IBooking bookingRep;
        private readonly ICustomer customerRep;
        private readonly ICar carRep;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CustomerBookingCreateModel(IBooking bookingRep, ICustomer customerRep, ICar carRep, IHttpContextAccessor httpContextAccessor)
        {
            this.bookingRep = bookingRep;
            this.customerRep = customerRep;
            this.carRep = carRep;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CarId = id;
            Car = await carRep.GetCarById(CarId);
            if (Car == null)
            {
                // Hantera fallet när bilen inte hittades...
                return Page();
            }

            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public Car Car { get; set; }
        [BindProperty]
        public int CarId { get; set; }
        
        public async Task<IActionResult> OnPostAsync(int id)
        {
            CarId = id;
            Car = await carRep.GetCarById(CarId);
            if (Car == null)
            {
                // Hantera fallet när bilen inte hittades...
                return Page();
            }
            Booking.Car = Car; // Sätt Car för Booking

            if (int.TryParse(httpContextAccessor.HttpContext.Request.Cookies["CustomerId"], out int customerId))
            {
                var loggedInCustomer = await customerRep.GetCustomerById(customerId);
                Booking.Customer = loggedInCustomer; //sätter Customer för Booking 
            }

            Booking.Price = Booking.CalculatedPrice; // Sätt Price för Booking

            await bookingRep.CreateBooking(Booking);
            if (Booking != null)
            {
                TempData["Message"] = "Ny bokning har skapats!";
            }

            return RedirectToPage("/Customers/CustomerBookingIndex" );
        }
    }
}
