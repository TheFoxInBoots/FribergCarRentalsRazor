namespace FribergCarRentalsRazor.Data
{
    public interface IAdmin : ICustomer, ICar
    {       
        Task<AdminUser> GetLoggedInAdmin(string email, string password);
    }
}
