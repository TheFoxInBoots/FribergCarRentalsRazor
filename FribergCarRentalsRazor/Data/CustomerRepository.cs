


using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentalsRazor.Data
{
    public class CustomerRepository : ICustomer
    {
        private readonly FribergCarRentalsRazorContext applicationDbContext;

        public CustomerRepository(FribergCarRentalsRazorContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await applicationDbContext.Customer.OrderBy(x => x.LastName).ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await applicationDbContext.Customer.FirstOrDefaultAsync(s => s.CustomerId == id);
        }
            
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            applicationDbContext.Add(customer);
            await applicationDbContext.SaveChangesAsync();
            return customer;
        }
        public async Task<Customer> EditCustomer(Customer customer)
        {
            applicationDbContext.Update(customer);
            await applicationDbContext.SaveChangesAsync();
            return customer;
        }
        public async Task DeleteCustomer(Customer customer)
        {
            applicationDbContext.Remove(customer);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetLoggedInCustomer(string email, string password)
        {
            return await applicationDbContext.Customer.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }

        public async Task<int> GetLoggedInCustomerId(string email)
        {
            var customer = await applicationDbContext.Customer.FirstOrDefaultAsync(s => s.Email == email);
            return customer?.CustomerId ?? 0;
        }
    }
}