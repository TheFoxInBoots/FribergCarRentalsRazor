using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;
using System.Runtime.ConstrainedExecution;

namespace FribergCarRentalsRazor.Pages.Bookings
{
    public class BookingEditModel : PageModel
    {
        private readonly IBooking bookingRep;
        private readonly ICar carRep;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookingEditModel(IBooking bookingRep, ICar carRep, IHttpContextAccessor httpContextAccessor)
        {
            this.bookingRep = bookingRep;
            this.carRep = carRep;
            this.httpContextAccessor = httpContextAccessor;
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
                
        public async Task<IActionResult> OnPostAsync(Booking booking)
        {
            Booking.Price = Booking.CalculatedPrice;

            try
            {
                await bookingRep.EditBooking(booking);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(Booking.BookingId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("/Customers/CustomerBookingIndex");
        }

        private bool BookingExists(int id)
        {
            return bookingRep.GetBookingById(id) != null;
        }
    }
}
