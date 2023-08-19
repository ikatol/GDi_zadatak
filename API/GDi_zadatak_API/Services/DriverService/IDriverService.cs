using GDi_zadatak_API.DTOs.Cars;
using GDi_zadatak_API.DTOs.Drivers;
using GDi_zadatak_API.Models;
using GDi_zadatak_API.Models.Cars;
using GDi_zadatak_API.Models.Drivers;

namespace GDi_zadatak_API.Services.DriverService {
	public interface IDriverService {
		Task<LayerResponse<List<GetDriverResponseDTO>>> GetAllDriversAsync();
		Task<LayerResponse<GetDriverResponseDTO>> GetDriverAsync(int id);
		Task<LayerResponse<GetDriverResponseDTO>> AddDriverAsync(PostDriverRequestDTO newDriver);
		Task<LayerResponse<GetDriverResponseDTO>> UpdateDriverAsync(PutDriverRequestDTO updatedDriver);
		Task<LayerResponse> DeleteDriverAsync(int id);
		Task<LayerResponse> AssignAsync(PatchAssignCarToDriverRequestDTO assignement);
	}
}
