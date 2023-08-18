using GDi_zadatak_API.Models.Cars;
using GDi_zadatak_API.Models;
using GDi_zadatak_API.Models.Drivers;

namespace GDi_zadatak_API.Repositories.Drivers {
	public interface IDriverRepository {
		Task<LayerResponse<List<Driver>>> GetAllAsync();
		Task<LayerResponse<Driver>> GetAsync(int id);
		Task<LayerResponse<Driver>> InsertAsync(Driver driver);
		Task<LayerResponse<Driver>> UpdateAsync(Driver updatedDriver);
		Task<LayerResponse> DeleteAsync(int id);
		Task<LayerResponse> UnassignCarFromDriverAsync(Driver driver);
		Task<LayerResponse> UnassignCarFromDriverAsync(Car car);
		Task<LayerResponse> AssignCarToDriverAsync(Car car, Driver driver);
	}
}
