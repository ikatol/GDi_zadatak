using GDi_zadatak_API.Models.Cars;

namespace GDi_zadatak_API.Models.Drivers {
	public class Driver {
		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public int? AssignedCarId { get; set; }
		public Car? AssignedCar { get; set; }
	}
}
