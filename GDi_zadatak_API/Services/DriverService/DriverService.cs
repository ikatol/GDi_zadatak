using GDi_zadatak_API.DTOs.Cars;
using GDi_zadatak_API.DTOs.Drivers;
using GDi_zadatak_API.Models;
using GDi_zadatak_API.Models.Cars;
using GDi_zadatak_API.Models.Drivers;
using GDi_zadatak_API.Repositories.Cars;
using GDi_zadatak_API.Repositories.Drivers;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;

namespace GDi_zadatak_API.Services.DriverService {
	public class DriverService : IDriverService {
		private readonly IDriverRepository _driverRepository;
		private readonly ICarRepository _carRepository;

		public DriverService(IDriverRepository driverRepository, ICarRepository carRepository) {
			_driverRepository = driverRepository;
			_carRepository = carRepository;
		}



		public async Task<LayerResponse<List<GetDriverResponseDTO>>> GetAllDriversAsync() {
			var repositoryResponse = await _driverRepository.GetAllAsync();
			List<GetDriverResponseDTO>? payload = null;

			if (repositoryResponse.Code == HttpStatusCode.OK && repositoryResponse.Data is not null) {
				payload = repositoryResponse.Data.Select(d => new GetDriverResponseDTO(d)).ToList();
			}

			return new LayerResponse<List<GetDriverResponseDTO>>(
					data: payload,
					code: repositoryResponse.Code,
					message: repositoryResponse.Message,
					additionalMessage: repositoryResponse.AdditionalMessage
					);
		}

		public async Task<LayerResponse<GetDriverResponseDTO>> GetDriverAsync(int id) {
			var repositoryResponse = await _driverRepository.GetAsync(id);
			GetDriverResponseDTO? payload = null;

			if (repositoryResponse.Code == HttpStatusCode.OK && repositoryResponse.Data is not null) {
				payload = new GetDriverResponseDTO(repositoryResponse.Data);
			}

			return new LayerResponse<GetDriverResponseDTO>(
				data: payload,
				code: repositoryResponse.Code,
				message: repositoryResponse.Message,
				additionalMessage: repositoryResponse.AdditionalMessage
				);
		}

		public async Task<LayerResponse<GetDriverResponseDTO>> UpdateDriverAsync(PutDriverRequestDTO updatedDriver) {
			var driverToUpdate = new Driver() {
				Id = updatedDriver.Id,
				Name = updatedDriver.Name
			};
			var repositoryResponse = await _driverRepository.UpdateAsync(driverToUpdate);

			GetDriverResponseDTO? payload = null;
			if (repositoryResponse.Code == HttpStatusCode.OK && repositoryResponse.Data is not null) {
				payload = new GetDriverResponseDTO(repositoryResponse.Data);
			}

			return new LayerResponse<GetDriverResponseDTO>(
				data: payload,
				code: repositoryResponse.Code,
				message: repositoryResponse.Message,
				additionalMessage: repositoryResponse.AdditionalMessage
				);
		}

		public async Task<LayerResponse<GetDriverResponseDTO>> AddDriverAsync(PostDriverRequestDTO newDriver) {
			var driverToInsert = new Driver() {
				Name = newDriver.Name
			};
			var repositoryResponse = await _driverRepository.InsertAsync(driverToInsert);

			GetDriverResponseDTO? payload = null;
			if (repositoryResponse.Code == HttpStatusCode.OK && repositoryResponse.Data is not null) {
				payload = new GetDriverResponseDTO(repositoryResponse.Data);
			}

			return new LayerResponse<GetDriverResponseDTO>(
				data: payload,
				code: repositoryResponse.Code,
				message: repositoryResponse.Message,
				additionalMessage: repositoryResponse.AdditionalMessage
				);
		}

		public async Task<LayerResponse> DeleteDriverAsync(int id) {
			var repositoryResponse = await _driverRepository.DeleteAsync(id);

			return new LayerResponse(
				code: repositoryResponse.Code,
				message: repositoryResponse.Message,
				additionalMessage: repositoryResponse.AdditionalMessage
				);
		}

		public async Task<LayerResponse> AssignAsync(PatchAssignCarToDriverRequestDTO assign) {
			//Driver? driver = assign.DriverId is not null ? (await _driverRepository.GetAsync((int)assign.DriverId)).Data : null;
			//Car? car = assign.CarId is not null ? (await _carRepository.GetAsync((int)assign.CarId)).Data : null;

			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;


			if (assign.DriverId != -1) {
				// assignamo ili unassignamo auto vozaču
				Driver? driver1 = (await _driverRepository.GetAsync(assign.DriverId)).Data;
				if (driver1 is not null) { 
					// ok, vozač postoji
					if (assign.CarId != -1) {
						// assignamo auto vozaču
						Car? car1 = (await _carRepository.GetAsync((int)assign.CarId)).Data;
						if (car1 is not null) {
							// auto postoji
							if (car1.Driver is not null) {
								// ovaj auto već netko ima assignanog
								if (car1.Driver.Id == driver1.Id) {
									// ovaj vozač već ima assignan ovaj auto
									code = HttpStatusCode.OK;
									message = "This car is already assigned to this driver.";
								} else {
									// neki drugi vozač već ima assignan ovaj auto
									code = HttpStatusCode.OK;
									message = "This car is already assigned to some other driver.";
								}
							} else {
								// assignaj auto
								var repositoryResponse = await _driverRepository.AssignCarToDriverAsync(car1, driver1);
								return new LayerResponse(
									repositoryResponse.Code,
									repositoryResponse.Message,
									repositoryResponse.AdditionalMessage
									);
							}
						} else {
							// auto s tim id-em ne postoji
							code = HttpStatusCode.NotFound;
							message = "Car with this id does not exist and, therefore, cannot be assigned to the driver.";
						}
					} else {
						//unassignamo auto vozaču
						if (driver1.AssignedCar is not null) {
							var repositoryResponse = await _driverRepository.UnassignCarFromDriverAsync(driver1);
								return new LayerResponse(
									repositoryResponse.Code,
									repositoryResponse.Message,
									repositoryResponse.AdditionalMessage
									);
						} else {
							message = "This driver already doesn't have an assigned car";
						}
					}
				} else {
					// vozač s tim id-em ne postoji
					code = HttpStatusCode.NotFound;
					message = "Driver with this ID does not exist.";
				}
			} else if (assign.CarId != -1) {
				// unassignamo vozača autu
				Car? car1 = (await _carRepository.GetAsync((int)assign.CarId)).Data;
				if (car1 is not null) {
					// auto postoji, možemo mu probati unassignati vozača
					if (car1.Driver is not null) {
						// ima vozača, makni mu ga
						var repositoryResponse = await _driverRepository.UnassignCarFromDriverAsync(car1);
						return new LayerResponse(
							repositoryResponse.Code,
							repositoryResponse.Message,
							repositoryResponse.AdditionalMessage
							);
					} else {
						// nema vozača, nemamo što maknuti
						message = "Car is already not assigned to any driver.";
					}
				} else {
					// auto ne postoji.
					code = HttpStatusCode.NotFound;
					message = "Car with this id does not exist.";
				}
			} else {
				// i auto i vozač su null.
				code = HttpStatusCode.NotFound;
				message = "Car and Driver with these IDs do not exist.";
			}


			return new LayerResponse(
				code: HttpStatusCode.OK,
				message: message,
				additionalMessage: null
				);
		}
	}
}
