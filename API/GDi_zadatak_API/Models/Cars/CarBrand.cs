using Microsoft.EntityFrameworkCore;

namespace GDi_zadatak_API.Models.Cars {
	public class CarBrand {
		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public List<CarModel> CarModels { get; set; } = null!;
    }
}
