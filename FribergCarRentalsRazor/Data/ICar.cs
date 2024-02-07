namespace FribergCarRentalsRazor.Data
{
    public interface ICar
    {
        Task<Car> GetCarById(int id);
        Task<List<Car>> GetAllCars();
        Task<Car> CreateCar(Car car);
        Task<Car> EditCar(Car car);
        Task DeleteCar(Car car);
    }
}