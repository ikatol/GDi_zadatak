using GDi_zadatak_API.Models.Cars;
using GDi_zadatak_API.Models.Drivers;

namespace GDi_zadatak_API.DTOs.Drivers {
	public class GetDriverResponseDTO {
        public int Id { get; init; }
        public string DriverName { get; init; } = null!;


		public GetDriverResponseDTO(Driver driver) {
			Id = driver.Id;
			DriverName = driver.Name!;
			
		}
	}
}
