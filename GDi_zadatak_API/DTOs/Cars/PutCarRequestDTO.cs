using GDi_zadatak_API.Models.Cars;

namespace GDi_zadatak_API.DTOs.Cars {
	public record PutCarRequestDTO {
		public int CarId { get; init; }
		public int ModelId { get; init; }
		public string Registration { get; init; } = null!;
		public int YearOfProduction { get; init; }

		//public UpdateCarRequestDTO(Car car) {
		//	ModelId = car.Model.Id;
		//	Registration = car.Registration;
		//	YearOfProduction = car.YearOfProduction;
		//}
	}
}
