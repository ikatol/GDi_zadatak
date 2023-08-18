﻿// <auto-generated />
using System;
using GDi_zadatak_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GDi_zadatak_API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GDi_zadatak_API.Models.Cars.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("YearOfProduction")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.HasIndex("Registration")
                        .IsUnique();

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("GDi_zadatak_API.Models.Cars.CarBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CarBrands");
                });

            modelBuilder.Entity("GDi_zadatak_API.Models.Cars.CarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("MaxLoadCapacityKg")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("GDi_zadatak_API.Models.Drivers.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AssignedCarId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssignedCarId")
                        .IsUnique()
                        .HasFilter("[AssignedCarId] IS NOT NULL");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("GDi_zadatak_API.Models.Cars.Car", b =>
                {
                    b.HasOne("GDi_zadatak_API.Models.Cars.CarModel", "Model")
                        .WithMany("Cars")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("GDi_zadatak_API.Models.Cars.CarModel", b =>
                {
                    b.HasOne("GDi_zadatak_API.Models.Cars.CarBrand", "Brand")
                        .WithMany("CarModels")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("GDi_zadatak_API.Models.Drivers.Driver", b =>
                {
                    b.HasOne("GDi_zadatak_API.Models.Cars.Car", "AssigendCar")
                        .WithOne("Driver")
                        .HasForeignKey("GDi_zadatak_API.Models.Drivers.Driver", "AssignedCarId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("AssigendCar");
                });

            modelBuilder.Entity("GDi_zadatak_API.Models.Cars.Car", b =>
                {
                    b.Navigation("Driver");
                });

            modelBuilder.Entity("GDi_zadatak_API.Models.Cars.CarBrand", b =>
                {
                    b.Navigation("CarModels");
                });

            modelBuilder.Entity("GDi_zadatak_API.Models.Cars.CarModel", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
