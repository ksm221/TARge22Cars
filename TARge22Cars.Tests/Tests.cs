using Xunit;
using TARge22Cars.Core.Dto;
using Targe22Cars.Core.ServiceInterface;
using System.Xml;
using TARge22Cars.Core.Domain;

namespace TARge22Cars.Tests
{
    public class CarsTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyCar_WhenReturnResult()
        {
            var car = new CarDto
            {
                Name = "Audi A6 Avant",
                Price = 50000,
                EnginePower = 201,
                FuelConsumption = 47,
                CreatedAt = new DateTime(2019, 1, 1),
                ModifiedAt = new DateTime(2021, 1, 1)
            };

            var result = await Svc<ICarsServices>().Create(car);

            Assert.NotNull(result);
        }



        [Fact]
        public async Task Should_UpdateCars_WhenUpdateData()
        {
            var guid = new Guid("173d934d-6446-4a36-a200-515ea63d1795");

            CarDto dto = MockCarData();

            Car car = new();

            car.Id = Guid.Parse("173d934d-6446-4a36-a200-515ea63d1795");
            car.Name = "Kia Sorento";
            car.Price = 60000;
            car.EnginePower = 194;
            car.FuelConsumption = 6;
            car.CreatedAt = new DateTime(2019, 3, 6);
            car.ModifiedAt = new DateTime(2022, 4, 8);

            await Svc<ICarsServices>().Update(dto);

            Assert.Equal(car.Id, guid);
            Assert.DoesNotMatch(car.Name, dto.Name);
            Assert.DoesNotMatch(car.Price.ToString(), dto.Price.ToString());
            Assert.NotEqual(car.EnginePower, dto.EnginePower);
        }

        [Fact]
        public async Task ShouldNot_UpdateCar_WhenNotUpdateData()
        {
            CarDto dto = MockCarData();
            var createCar = await Svc<ICarsServices>().Create(dto);

            CarDto nullUpdate = MockNullCar();
            var result = await Svc<ICarsServices>().Update(nullUpdate);

            var nullId = nullUpdate.Id;

            Assert.True(dto.Id == nullId);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdCar_WhenDidNotDeleteCar()
        {
            CarDto car = MockCarData();

            var car1 = await Svc<ICarsServices>().Create(car);
            var car2 = await Svc<ICarsServices>().Create(car);

            var result = await Svc<ICarsServices>().Delete((Guid)car2.Id);

            Assert.NotEqual(result.Id, car1.Id);
        }


        private CarDto MockCarData()
        {
            CarDto car = new()
            {
                Name = "Volvo XC90",
                Price = 24000,
                EnginePower = 400,
                FuelConsumption = 38,
                CreatedAt = new DateTime(2017, 2, 3),
                ModifiedAt = DateTime.Now,
            };

            return car;
        }

        private CarDto MockNullCar()
        {
            CarDto car = new()
            {
                Id = null,
                Name = "Ford",
                Price = 240000,
                EnginePower = 4000,
                FuelConsumption = 380,
                CreatedAt = new DateTime(2024, 1, 2),
                ModifiedAt = DateTime.Now,
            };

            return car;
        }
    }
}