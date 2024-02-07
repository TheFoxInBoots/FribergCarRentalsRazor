
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentalsRazor.Data
{
    public class CarRepository : ICar
    {
        private readonly FribergCarRentalsRazorContext applicationDbContext;

        public CarRepository(FribergCarRentalsRazorContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

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
    }
}
