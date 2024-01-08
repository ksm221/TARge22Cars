using TARge22Cars.Core.Domain;
using TARge22Cars.Core.Dto;

namespace Targe22Cars.Core.ServiceInterface
{
    public interface ICarsServices
    {
        Task<Car> Create(CarDto dto);
        Task<Car> DetailsAsync(Guid id);
        Task<Car> Delete(Guid id);
        Task<Car> Update(CarDto dto);
    }
}