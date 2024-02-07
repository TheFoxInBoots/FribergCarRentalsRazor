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
    public class BookingDeleteModel : PageModel
    {
        private readonly IBooking bookingRep;

        public BookingDeleteModel(IBooking bookingRep)
        {
            this.bookingRep = bookingRep;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || bookingRep == null)
            {
                return NotFound(); // 
            }

            var booking = await bookingRep.GetBookingById(id.Value);

            if (booking != null)
            {
                Booking = booking;
                bookingRep.DeleteBooking(Booking);

                TempData["Message"] = "Bokningen har tagits bort!";
            }

            return RedirectToPage("/Cars/CarIndex");
        }
    }
}
