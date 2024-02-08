namespace FribergCarRentalsRazor.Data
{
    public interface IAdmin : ICustomer, ICar, IBooking
    {       
        Task<AdminUser> GetLoggedInAdmin(string email, string password);
    }
}
