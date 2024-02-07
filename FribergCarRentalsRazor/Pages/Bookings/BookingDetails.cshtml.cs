using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Pages.Bookings
{
    public class BookingDetailsModel : PageModel
    {
        private readonly IBooking bookingRep;

        public BookingDetailsModel(IBooking bookingRep)
        {
            this.bookingRep = bookingRep;
        }

        public Booking Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Booking = await bookingRep.GetBookingById(id.Value);
            if (Booking == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
