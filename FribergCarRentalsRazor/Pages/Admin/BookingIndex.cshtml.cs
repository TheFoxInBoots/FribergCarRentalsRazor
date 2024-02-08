using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;
using Microsoft.AspNetCore.Http;

namespace FribergCarRentalsRazor.Pages.Admin
{
    public class BookingIndexModel : PageModel
    {
        private readonly IBooking bookingRep;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookingIndexModel(IBooking bookingRep, IHttpContextAccessor httpContextAccessor)
        {
            this.bookingRep = bookingRep;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IList<Booking> BookingList { get; set; } = default!;

        [BindProperty]
        public Booking Booking { get; set; }

        public async Task OnGetAsync()
        {
            BookingList = await bookingRep.GetAllBookings(); // hämtar endast de bokningar från det sparade kund Id:t    
        }
    }
}
