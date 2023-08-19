using GDi_zadatak_API.Data;
using GDi_zadatak_API.Models;
using GDi_zadatak_API.Models.Cars;
using GDi_zadatak_API.Models.Drivers;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GDi_zadatak_API.Repositories.Drivers {
	public class DriverRepository : IDriverRepository {
		private readonly DataContext _dataContext;

		public DriverRepository(DataContext dataContext) {
			_dataContext = dataContext;
		}

		public async Task<LayerResponse> UnassignCarFromDriverAsync(Driver driver) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;

			try {
				driver.AssignedCar = null;
				await _dataContext.SaveChangesAsync();
				message = "Car has been unassigned from the driver";
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse(code, message, addMessage);
		}
		public async Task<LayerResponse> UnassignCarFromDriverAsync(Car car) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;

			try {
				car.Driver = null;
				await _dataContext.SaveChangesAsync();
				message = "Car has been unassigned from the driver";
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse(code, message, addMessage);
		}
		public async Task<LayerResponse> AssignCarToDriverAsync(Car car, Driver driver) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;

			try {
				if (car.Driver is not null) {
					car.DriverId = null;
					await _dataContext.SaveChangesAsync();
				}
				driver.AssignedCarId = car.Id;
				await _dataContext.SaveChangesAsync();
				message = "Car has been assigned to the driver";
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse(code, message, addMessage);
		}

		public async Task<LayerResponse> DeleteAsync(int id) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;

			try {
				var driverToBeDeleted = await GetAsync(id);
				if (driverToBeDeleted.Data is not null) {
					_dataContext.Remove(driverToBeDeleted.Data);
					await _dataContext.SaveChangesAsync();

					if (_dataContext.Entry(driverToBeDeleted.Data).State == EntityState.Detached) {
						message = "Driver has been deleted.";
					} else {
						code = HttpStatusCode.InternalServerError;
						message = $"Driver with an Id {id} was not deleted.";
					}
				} else {
					code = HttpStatusCode.NotFound;
					message = $"Driver with an id {id} does not exist";
				}
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse(code, message, addMessage);
		}

		public async Task<LayerResponse<List<Driver>>> GetAllAsync() {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;
			List<Driver>? data = null;

			try {
				data = await _dataContext.Drivers.ToListAsync();
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse<List<Driver>>(data, code, message, addMessage);
		}

		public async Task<LayerResponse<Driver>> GetAsync(int id) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;
			Driver? data = null;

			try {
				data = await _dataContext.Drivers.Include(c => c.AssignedCar).FirstOrDefaultAsync(c => c.Id == id);
				if (data == null) {
					code = HttpStatusCode.NotFound;
				}
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse<Driver>(data, code, message, addMessage);
		}

		public async Task<LayerResponse<Driver>> InsertAsync(Driver driver) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;
			Driver? data = null;

			try {
				_dataContext.Add(driver);
				await _dataContext.SaveChangesAsync();

				if (_dataContext.Entry(driver).State == EntityState.Unchanged) {
					data = driver;
				} else {
					code = HttpStatusCode.InternalServerError;
				}
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse<Driver>(data, code, message, addMessage);
		}

		public async Task<LayerResponse<Driver>> UpdateAsync(Driver updatedDriver) {
			HttpStatusCode code = HttpStatusCode.OK;
			string? message = null;
			string? addMessage = null;
			Driver? data = null;

			try {
				_dataContext.Update(updatedDriver);
				await _dataContext.SaveChangesAsync();

				if (_dataContext.Entry(updatedDriver).State == EntityState.Unchanged) {
					data = updatedDriver;
				} else {
					code = HttpStatusCode.InternalServerError;
					// update didn't work.
				}
			} catch (Exception ex) {
				code = HttpStatusCode.InternalServerError;
				message = ex.Message;
				if (ex.InnerException is not null) addMessage = ex.InnerException.Message;
			}

			return new LayerResponse<Driver>(data, code, message, addMessage);
		}
	}
}
