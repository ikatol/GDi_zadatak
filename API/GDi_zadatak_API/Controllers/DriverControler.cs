using GDi_zadatak_API.DTOs.Cars;
using GDi_zadatak_API.DTOs.Drivers;
using GDi_zadatak_API.Models;
using GDi_zadatak_API.Services.DriverService;
using Microsoft.AspNetCore.Mvc;

namespace GDi_zadatak_API.Controllers {
	[ApiController]
	[Route("API/[controller]")]
	public class DriverControler : Controller {
		private readonly IDriverService _driverService;

		public DriverControler(IDriverService driverService) {
			_driverService = driverService;
		}



		#region GETs
		[HttpGet("GetAllDrivers")]
		public async Task<ActionResult<LayerResponse<List<GetDriverResponseDTO>>>> GetAllDrivers() {
			var serviceResponse = await _driverService.GetAllDriversAsync();
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}

		[HttpGet("GetDriver/{id}")]
		public async Task<ActionResult<LayerResponse<GetDriverResponseDTO>>> GetDriver(int id) {
			var serviceResponse = await _driverService.GetDriverAsync(id);
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}
		#endregion

		#region POSTs
		[HttpPost("AddNewDriver")]
		public async Task<ActionResult<LayerResponse<GetDriverResponseDTO>>> AddCar(PostDriverRequestDTO newDriver) {
			var serviceResponse = await _driverService.AddDriverAsync(newDriver);
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}
		#endregion


		#region PUTs
		[HttpPut("UpdateDriver")]
		public async Task<ActionResult<LayerResponse<List<GetDriverResponseDTO>>>> UpdateDriver(PutDriverRequestDTO updatedDriver) {
			var serviceResponse = await _driverService.UpdateDriverAsync(updatedDriver);
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}
		#endregion

		#region PATCHes
		[HttpPatch("AssignCarToDriver")]
		public async Task<ActionResult<LayerResponse>> AssignCarToDriver(PatchAssignCarToDriverRequestDTO assignement) {
			var serviceResponse = await _driverService.AssignAsync(assignement);
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}
		#endregion


		#region DELETEs
		[HttpDelete("DeleteDriver/{id}")]
		public async Task<ActionResult<LayerResponse>> DeleteDriver(int id) {
			var serviceResponse = await _driverService.DeleteDriverAsync(id);
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}
		#endregion
	}
}
