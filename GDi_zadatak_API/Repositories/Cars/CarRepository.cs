using GDi_zadatak_API.Data;
using GDi_zadatak_API.Models;
using GDi_zadatak_API.Models.Cars;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GDi_zadatak_API.Repositories.Cars {
	public class CarRepository : ICarRepository {
		private readonly DataContext _dataContext;


		public CarRepository(DataContext dataContext) {
			_dataContext = dataContext;
		}


		public async Task<LayerResponse> DeleteAsync(int id) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;

			try {
				var carToBeDeleted = await GetAsync(id);
				if (carToBeDeleted.Data is not null) {
					_dataContext.Remove(carToBeDeleted.Data);
					await _dataContext.SaveChangesAsync();

					if (_dataContext.Entry(carToBeDeleted.Data).State == EntityState.Detached) {
						message = "Car has been deleted.";
					} else {
						code = HttpStatusCode.InternalServerError;
						message = $"Car with an Id {id} was not deleted.";
					}
				} else {
					code = HttpStatusCode.NotFound;
					message = $"Car with an id {id} does not exist";
				}
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse(code, message, addMessage);
		}

		public async Task<LayerResponse<List<Car>>> GetAllAsync() {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;
			List<Car>? data = null;

			try {
				data = await _dataContext.Cars.Include(c => c.Model).ThenInclude(c => c.Brand).ToListAsync();
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse<List<Car>>(data, code, message, addMessage);
		}

		public async Task<LayerResponse<Car>> GetAsync(int id) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;
			Car? data = null;

			try {
				data = await _dataContext.Cars.Include(c => c.Driver).Include(c => c.Model).ThenInclude(c => c.Brand).FirstOrDefaultAsync(c => c.Id == id);
				if (data == null) {
					code = HttpStatusCode.NotFound;
				}
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse<Car>(data, code, message, addMessage);
		}

		public async Task<LayerResponse<Car>> InsertAsync(Car car) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;
			Car? data = null;

			try {
				if (await GetCarModelAsync(car.ModelId) is not null) {
					_dataContext.Add(car);
					await _dataContext.SaveChangesAsync();

					if (_dataContext.Entry(car).State == EntityState.Unchanged) {
						data = car;
					} else {
						code = HttpStatusCode.InternalServerError;
					}
				} else {
					code = HttpStatusCode.BadRequest;
					message = $"There is no car model with provided car model id: {car.ModelId}";
				}
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse<Car>(data, code, message, addMessage);
		}

		public async Task<LayerResponse<Car>> UpdateAsync(Car updatedCar) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;
			Car? data = null;

			try {
				if (await GetCarModelAsync(updatedCar.ModelId) is not null) {
					_dataContext.Update(updatedCar);
					await _dataContext.SaveChangesAsync();

					if (_dataContext.Entry(updatedCar).State == EntityState.Unchanged) {
						data = updatedCar;
					} else {
						code = HttpStatusCode.InternalServerError;
						// update didn't work.
					}
				} else {
					code = HttpStatusCode.BadRequest;
					message = "Model ID doesn't exist.";
				}
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse<Car>(data, code, message, addMessage);
		}

		private async Task<CarModel?> GetCarModelAsync(int modelId) =>
				await _dataContext.CarModels.Include(cm => cm.Brand).FirstOrDefaultAsync(cm => cm.Id == modelId);

	}
}
