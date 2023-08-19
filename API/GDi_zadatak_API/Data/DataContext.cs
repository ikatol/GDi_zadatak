using GDi_zadatak_API.Models.Cars;
using GDi_zadatak_API.Models.Drivers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GDi_zadatak_API.Data {
	public class DataContext : DbContext {
		public DbSet<Driver> Drivers { get; set; }
		public DbSet<Car> Cars { get; set; }
		public DbSet<CarModel> CarModels { get; set; }
		public DbSet<CarBrand> CarBrands { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options) {
                
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CarBrand>().HasIndex(cb => cb.Name).IsUnique();

			modelBuilder.Entity<CarModel>().HasIndex(cm => cm.Name).IsUnique();

			modelBuilder.Entity<Car>().HasIndex(c => c.Registration).IsUnique();

			modelBuilder.Entity<Driver>().HasOne(d => d.AssignedCar).WithOne(c => c.Driver)
				.HasForeignKey<Driver>(d => d.AssignedCarId).OnDelete(DeleteBehavior.SetNull);
		}
	}
}
