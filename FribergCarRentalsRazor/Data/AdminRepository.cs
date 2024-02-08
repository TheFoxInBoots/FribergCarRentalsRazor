using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentalsRazor.Data
{
    public class AdminRepository : IAdmin
    {
        private readonly FribergCarRentalsRazorContext applicationDbContext;

        public AdminRepository(FribergCarRentalsRazorContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<AdminUser> GetLoggedInAdmin(string email, string password)
        {
            return await applicationDbContext.Admin.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }


        // Customer
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


        // Car
        public async Task<Car> CreateCar(Car car)
        {
            applicationDbContext.Add(car);
            await applicationDbContext.SaveChangesAsync();
            return car;
        }
        public async Task<Car> EditCar(Car car)
        {
            applicationDbContext.Update(car);
            await applicationDbContext.SaveChangesAsync();
            return car;
        }
        public async Task DeleteCar(Car car)
        {
            applicationDbContext.Remove(car);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Car>> GetAllCars()
        {
            return await applicationDbContext.Car.OrderBy(x => x.Brand).ToListAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            return applicationDbContext.Car.FirstOrDefault(s => s.CarId == id);
        }


        // Booking
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
