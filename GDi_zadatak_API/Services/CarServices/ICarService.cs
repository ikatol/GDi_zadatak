using GDi_zadatak_API.DTOs.Cars;
using GDi_zadatak_API.Models;

namespace GDi_zadatak_API.Services.CarService {
	public interface ICarService {
		Task<LayerResponse<List<GetCarResponseDTO>>> GetAllCarsAsync();
		Task<LayerResponse<GetCarResponseDTO>> GetCarAsync(int id);
		Task<LayerResponse<GetCarResponseDTO>> AddCarAsync(PostCarRequestDTO newCar);
		Task<LayerResponse<GetCarResponseDTO>> UpdateCarAsync(PutCarRequestDTO updatedCar);
		Task<LayerResponse> DeleteCarAsync(int id);
	}
}
