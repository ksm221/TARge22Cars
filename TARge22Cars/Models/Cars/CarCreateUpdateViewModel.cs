namespace Targe22Cars.Models.Cars
{
    public class CarCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int EnginePower { get; set; }
        public int FuelConsumption { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
