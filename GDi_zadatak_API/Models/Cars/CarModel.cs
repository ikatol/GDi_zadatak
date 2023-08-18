using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GDi_zadatak_API.Models.Cars {
	public class CarModel {
		public int Id { get; set; }
        public int BrandId { get; set; }
		public CarBrand Brand { get; set; } = null!;
		public string Name { get; set; } = null!;
		public int MaxLoadCapacityKg { get; set; }


        public List<Car> Cars { get; set; } = null!;
    }
}
