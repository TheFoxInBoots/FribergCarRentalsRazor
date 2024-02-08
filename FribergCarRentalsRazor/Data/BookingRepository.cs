
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentalsRazor.Data
{
    public class BookingRepository : IBooking
    {
        private readonly FribergCarRentalsRazorContext applicationDbContext;

        public BookingRepository(FribergCarRentalsRazorContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<Booking> GetBookingById(int id)
        {
            try
            {
                return applicationDbContext.Booking
                    .Include(s => s.Customer)
                    .Include(s => s.Car)
                    .FirstOrDefault(s => s.BookingId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod vid hämtning av bokning.", ex);
            }
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            return await applicationDbContext.Booking
                .Include(x => x.Car)
                .Include(x => x.Customer)
                .OrderBy(x => x.BookingId)
                .ToListAsync();
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            try
            {
                applicationDbContext.Add(booking);
                await applicationDbContext.SaveChangesAsync();
                return booking;
            }
            catch (Exception ex)
            {
                // Logga exception här
                throw new Exception("Ett fel uppstod vid skapande av bokning.", ex);
            }
        }

        public async Task DeleteBooking(Booking booking)
        {
            applicationDbContext.Remove(booking);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetBookingsByCustomerId(string customerId)
        {
            int customerIdAsInt;

            // Försök konvertera customerId till en int
            if (int.TryParse(customerId, out customerIdAsInt))
            {
                return await applicationDbContext.Booking
                    .Include(b => b.Customer)
                    .Include(b => b.Car)
                    .Where(b => b.Customer.CustomerId == customerIdAsInt)
                    .ToListAsync();
            }
            else
            {
                // Hantera fallet då customerId inte kunde konverteras till en int - returnerar en tom lista
                return new List<Booking>();
            }
        }
    }
}
