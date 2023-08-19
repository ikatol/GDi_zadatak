using GDi_zadatak_API.Models.Drivers;
using System.ComponentModel.DataAnnotations;

namespace GDi_zadatak_API.Models.Cars {
	public class Car {
		public int Id { get; set; }
        public int ModelId { get; set; }
        public CarModel Model { get; set; } = null!;
		public string Registration { get; set; } = null!;
        public int YearOfProduction { get; set; }

        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }
    }
}
