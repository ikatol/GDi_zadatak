using GDi_zadatak_API.DTOs.Cars;
using GDi_zadatak_API.Models;
using GDi_zadatak_API.Models.Cars;
using GDi_zadatak_API.Repositories.Cars;
using System.Net;

namespace GDi_zadatak_API.Services.CarService {
	public class CarService : ICarService {
		private readonly ICarRepository _carRepository;


		public CarService(ICarRepository carRepository) {
			_carRepository = carRepository;
		}


		public async Task<LayerResponse<GetCarResponseDTO>> AddCarAsync(PostCarRequestDTO newCar) {
			var carToInsert = new Car() {
				ModelId = newCar.ModelId,
				Registration = newCar.Registration,
				YearOfProduction = newCar.YearOfProduction
			};
			var repositoryResponse = await _carRepository.InsertAsync(carToInsert);

			GetCarResponseDTO? payload = null;
			if (repositoryResponse.Code == HttpStatusCode.OK && repositoryResponse.Data is not null) {
				payload = new GetCarResponseDTO(repositoryResponse.Data);
			}

			return new LayerResponse<GetCarResponseDTO>(
				data: payload,
				code: repositoryResponse.Code,
				message: repositoryResponse.Message,
				additionalMessage: repositoryResponse.AdditionalMessage
				);
		}

		public async Task<LayerResponse> DeleteCarAsync(int id) {
			var repositoryResponse = await _carRepository.DeleteAsync(id);

			return new LayerResponse (
				code: repositoryResponse.Code,
				message: repositoryResponse.Message,
				additionalMessage: repositoryResponse.AdditionalMessage
				);
		}

		public async Task<LayerResponse<List<GetCarResponseDTO>>> GetAllCarsAsync() {
			var repositoryResponse = await _carRepository.GetAllAsync();
			List<GetCarResponseDTO>? payload = null;

			if (repositoryResponse.Code == HttpStatusCode.OK && repositoryResponse.Data is not null) {
				payload = repositoryResponse.Data.Select(c => new GetCarResponseDTO(c)).ToList();
			}

			return new LayerResponse<List<GetCarResponseDTO>>(
					data: payload,
					code: repositoryResponse.Code,
					message: repositoryResponse.Message,
					additionalMessage: repositoryResponse.AdditionalMessage
					);
		}

		public async Task<LayerResponse<GetCarResponseDTO>> GetCarAsync(int id) {
			var repositoryResponse = await _carRepository.GetAsync(id);
			GetCarResponseDTO? payload = null;

			if (repositoryResponse.Code == HttpStatusCode.OK && repositoryResponse.Data is not null) {
				payload = new GetCarResponseDTO(repositoryResponse.Data);
			}

			return new LayerResponse<GetCarResponseDTO>(
				data: payload,
				code: repositoryResponse.Code,
				message: repositoryResponse.Message,
				additionalMessage : repositoryResponse.AdditionalMessage
				);
		}

		public async Task<LayerResponse<GetCarResponseDTO>> UpdateCarAsync(PutCarRequestDTO updatedCar) {
			var carToUpdate = new Car() {
				Id = updatedCar.CarId,
				ModelId = updatedCar.ModelId,
				Registration = updatedCar.Registration,
				YearOfProduction = updatedCar.YearOfProduction
			};
			var repositoryResponse = await _carRepository.UpdateAsync(carToUpdate);

			GetCarResponseDTO? payload = null;
			if (repositoryResponse.Code == HttpStatusCode.OK && repositoryResponse.Data is not null) {
				payload = new GetCarResponseDTO(repositoryResponse.Data);
			}

			return new LayerResponse<GetCarResponseDTO>(
				data: payload,
				code: repositoryResponse.Code,
				message: repositoryResponse.Message,
				additionalMessage: repositoryResponse.AdditionalMessage
				);
		}


		//	public async Task<ServiceResponse<List<GetCarResponseDTO>>> GetAllAsync() {
		//		var carsFromRepo = await _carRepository.GetAllCars();
		//		return new ServiceResponse<List<GetCarResponseDTO>>(
		//			data: carsFromRepo.Select(x => new GetCarResponseDTO(car: x)).ToList(),
		//			message: "All cars."
		//			);
		//	}

		//	public async Task<ServiceResponse<GetCarResponseDTO>> GetAsync(int id) {
		//		var car = await _carRepository.GetCar(id);

		//		if (car is not null) {
		//			return new ServiceResponse<GetCarResponseDTO>(new GetCarResponseDTO(car), $"Car with an id: {id}.");
		//		} else {
		//			return new ServiceResponse<GetCarResponseDTO>(data: null, $"Car with an id {id} not found.", false);
		//		}
		//	}

		//	//public async Task<ServiceResponse> UpdateCar(int carId, int newModel) {
		//	//	var car = await _carRepository.GetCar(carId);

		//	//	if (car is not null) {
		//	//		//if (_carRepository.UpdateCar(car, carModel));
		//	//		return new ServiceResponse(message: $"Car with an id: {carId} was updated.");
		//	//	}

		//	//	if (car is null) {
		//	//		return new ServiceResponse(
		//	//			message: $"Car with an id: {carId} does not exist.",
		//	//			success: false
		//	//			);
		//	//	} else {

		//	//		return new ServiceResponse(message: $"Car with an id: {carId} was updated.");
		//	//	}
		//	//}

		//	public async Task<ServiceResponse<GetCarResponseDTO>> InsertAsync(PostCarRequestDTO newCar) {
		//		var carModel = await _carRepository.GetCarModel(newCar.ModelId);
		//		if (carModel is not null) {
		//			var repositoryResponse = await _carRepository.AddCar(new Car() { Model = carModel });

		//			if (repositoryResponse.Success) {
		//				return new ServiceResponse<GetCarResponseDTO>(
		//					data: new GetCarResponseDTO(repositoryResponse),
		//					message: $"Car has been successfuly added. Its ID is {repositoryResponse.Data.Id}."
		//					);
		//			} else {
		//				return new ServiceResponse<GetCarResponseDTO>(
		//					data: null,
		//					message: $"Car could not be added.",
		//					success: false,
		//					internalError: true
		//					);
		//			}
		//		} else {
		//			return new ServiceResponse<GetCarResponseDTO>(
		//				data: null,
		//				message: "Car model could not be found.",
		//				success: false
		//				);
		//		}
		//	}

		//	public async Task<ServiceResponse<GetCarResponseDTO>> UpdateAsync(PutCarRequestDTO updatedCar) {
		//		throw new NotImplementedException();
		//	}

		//	public async Task<ServiceResponse> RemoveAsync(int id) {
		//		throw new NotImplementedException();
		//	}
		}
	}
