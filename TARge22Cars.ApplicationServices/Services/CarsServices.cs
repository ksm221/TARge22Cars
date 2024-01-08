using Microsoft.EntityFrameworkCore;
using Targe22Cars.Core.ServiceInterface;
using TARge22Cars.Core.Domain;
using TARge22Cars.Core.Dto;
using TARge22Cars.Data;

namespace TARge22Cars.ApplicationServices.Services
{
    public class CarsServices : ICarsServices
    {
        private readonly TARge22CarsContext _context;

        public CarsServices
            (
                TARge22CarsContext context
            )
        {
            _context = context;
        }


        public async Task<Car> Create(CarDto dto)
        {
            Car car = new Car();

            car.Id = Guid.NewGuid();
            car.CreatedAt = DateTime.Now;
            car.ModifiedAt = DateTime.Now;


            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> DetailsAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Car> Update(CarDto dto)
        {
            var domain = new Car()
            {
                Id = dto.Id,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now,
            };

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();

            return carId;
        }
    }
}
