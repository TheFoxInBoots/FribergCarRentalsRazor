using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;

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
        public IBooking BookingRep { get; }
        public List<Car> Cars { get; set; } = new List<Car>(); // Lägg till en lista med bilar
        [BindProperty]
        public int SelectedCarId { get; set; }
        public Car Car { get; set; }

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

            Cars = await carRep.GetAllCars(); // Fyll listan med bilar

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Booking booking)
        {            
            try
            {
                Booking.Car = await carRep.GetCarById(SelectedCarId);
                Booking.Price = Booking.CalculatedPrice;
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

            TempData["Message"] = "Ändringarna har sparats!";

            if (HttpContext.Request.Cookies["UserRole"] == "Customer")
            {
                return RedirectToPage("/Customers/CustomerBookingIndex");
            }

            return RedirectToPage("/Admin/BookingIndex");
        }

        private bool BookingExists(int id)
        {
            // Kontrollera om kunden med det angivna ID:t finns i databasen
            return bookingRep.GetBookingById(id) != null;
        }
    }
}
