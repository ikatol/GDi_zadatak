using GDi_zadatak_API.Models.Cars;

namespace GDi_zadatak_API.DTOs.Cars {
	public record GetCarResponseDTO {
		public int Id { get; init; }
		public string Model { get; init; } = null!;
		public string Brand { get; init; } = null!;
		public string Registration { get; init; } = null!;
		public int YearOfProduction { get; init; }	
		public int MaxLoadCapacityKg { get; init; }

		public GetCarResponseDTO(Car car) {
			Id = car.Id;
			Model = car.Model.Name!;
			Brand = car.Model.Brand.Name!;
			Registration = car.Registration;
			YearOfProduction = car.YearOfProduction;
			MaxLoadCapacityKg = car.Model.MaxLoadCapacityKg;
		}
	}
}
