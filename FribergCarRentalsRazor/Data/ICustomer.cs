
namespace FribergCarRentalsRazor.Data
{
    public interface ICustomer
    {
        Task<Customer> GetCustomerById(int id);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> EditCustomer(Customer customer);
        Task DeleteCustomer(Customer customer);
        Task<Customer> GetLoggedInCustomer(string email, string password);
        Task<int> GetLoggedInCustomerId(string email);
    }
}
