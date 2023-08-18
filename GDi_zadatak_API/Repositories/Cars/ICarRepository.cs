using GDi_zadatak_API.Models;
using GDi_zadatak_API.Models.Cars;

namespace GDi_zadatak_API.Repositories.Cars {
	public interface ICarRepository {
		Task<LayerResponse<List<Car>>> GetAllAsync();
		Task<LayerResponse<Car>> GetAsync(int id);
		Task<LayerResponse<Car>> InsertAsync(Car car);
		Task<LayerResponse<Car>> UpdateAsync(Car updatedCar);
		Task<LayerResponse> DeleteAsync(int id);
	}
}
