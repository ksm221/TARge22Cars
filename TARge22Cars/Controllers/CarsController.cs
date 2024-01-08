using Microsoft.AspNetCore.Mvc;
using Targe22Cars.Core.ServiceInterface;
using Targe22Cars.Models.Cars;
using TARge22Cars.Core.Dto;
using TARge22Cars.Data;

namespace Targe22Cars.Controllers
{
    public class CarsController : Controller
    {
        private readonly TARge22CarsContext _context;
        private readonly ICarsServices _Carservices;
        public CarsController
            (
                TARge22CarsContext context,
                ICarsServices CarsServices
            )
        {
            _context = context;
            _Carservices = CarsServices;
        }


        public IActionResult Index()
        {
            var result = _context.Cars
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel result = new CarCreateUpdateViewModel();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
                var dto = new CarDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                EnginePower = vm.EnginePower,
                FuelConsumption = vm.FuelConsumption,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };

            var result = await _Carservices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _Carservices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarCreateUpdateViewModel();

            vm.Id = car.Id;
            vm.Name = car.Name;
            vm.EnginePower = car.EnginePower;
            vm.FuelConsumption = car.FuelConsumption;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;


            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                EnginePower = vm.EnginePower,
                FuelConsumption = vm.FuelConsumption,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _Carservices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _Carservices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }


            var vm = new CarDetailsViewModel();

            vm.Id = car.Id;
            vm.Name = car.Name;
            vm.EnginePower = car.EnginePower;
            vm.FuelConsumption = car.FuelConsumption;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _Carservices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarDeleteViewModel();

            vm.Id = car.Id;
            vm.Name = car.Name;
            vm.EnginePower = car.EnginePower;
            vm.FuelConsumption = car.FuelConsumption;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;


            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _Carservices.Delete(id);

            if (carId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}