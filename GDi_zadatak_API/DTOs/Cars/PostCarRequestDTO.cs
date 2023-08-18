using GDi_zadatak_API.Models.Cars;

namespace GDi_zadatak_API.DTOs.Cars {
	public record PostCarRequestDTO {
		public int ModelId { get; init; }
		public string Registration { get; init; } 
		public int YearOfProduction { get; init; }

		public PostCarRequestDTO() {
			
		}

		public PostCarRequestDTO(Car car) {
			ModelId = car.Model.Id;
			Registration = car.Registration;
			YearOfProduction = car.YearOfProduction;
		}
	}
}
