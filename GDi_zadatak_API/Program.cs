
using GDi_zadatak_API.Data;
using GDi_zadatak_API.Repositories.Cars;
using GDi_zadatak_API.Repositories.Drivers;
using GDi_zadatak_API.Services.CarService;
using GDi_zadatak_API.Services.DriverService;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GDi_zadatak_API {
	/// <summary>
	/// Program.cs
	/// </summary>
	public class Program {
		/// <summary>
		/// Main koment
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(
			//	s => {
			//	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			//	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			//	s.IncludeXmlComments(xmlPath);
			//}
			);

			builder.Services.AddScoped<ICarService, CarService>();
			builder.Services.AddScoped<ICarRepository, CarRepository>();
			builder.Services.AddScoped<IDriverService, DriverService>();
			builder.Services.AddScoped<IDriverRepository, DriverRepository>();

			builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}