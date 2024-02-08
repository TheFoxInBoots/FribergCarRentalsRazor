namespace FribergCarRentalsRazor.Data
{
    public interface IBooking
    {        
        Task<Booking> GetBookingById(int id);
        Task<List<Booking>> GetAllBookings();
        Task<Booking> CreateBooking(Booking booking);
        Task DeleteBooking(Booking booking);
        Task<List<Booking>> GetBookingsByCustomerId(string customerId);
    }
}
