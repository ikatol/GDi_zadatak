using GDi_zadatak_API.DTOs.Cars;
using GDi_zadatak_API.Models;
using GDi_zadatak_API.Models.Cars;
using GDi_zadatak_API.Services.CarService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Net;

namespace GDi_zadatak_API.Controllers {
	[ApiController]
	[Route("API/[controller]")]
	public class CarController : ControllerBase {
		private readonly ICarService _carService;


		public CarController(ICarService carService) {
			this._carService = carService;
		}


		#region GETs
		[HttpGet("GetAllCars")]
		public async Task<ActionResult<LayerResponse<List<GetCarResponseDTO>>>> GetAllCars() {
			var serviceResponse = await _carService.GetAllCarsAsync();
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}

		[HttpGet("GetCar/{id}")]
		public async Task<ActionResult<LayerResponse<GetCarResponseDTO>>> GetCar(int id) {
			var serviceResponse = await _carService.GetCarAsync(id);
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}
		#endregion


		#region POSTs
		[HttpPost("AddNewCar")]
		public async Task<ActionResult<LayerResponse<GetCarResponseDTO>>> AddCar(PostCarRequestDTO newCar) {
			var serviceResponse = await _carService.AddCarAsync(newCar);
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}
		#endregion


		#region PUTs
		[HttpPut("UpdateCar")]
		public async Task<ActionResult<LayerResponse<List<GetCarResponseDTO>>>> UpdateCar(PutCarRequestDTO updatedCar) {
			var serviceResponse = await _carService.UpdateCarAsync(updatedCar);
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}
		#endregion


		#region DELETEs
		[HttpDelete("DeleteCar/{id}")]
		public async Task<ActionResult<LayerResponse>> DeleteCar(int id) {
			var serviceResponse = await _carService.DeleteCarAsync(id);
			return StatusCode((int)serviceResponse.Code, serviceResponse);
		}
		#endregion
	}
}