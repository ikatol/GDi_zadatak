using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDi_zadatak_API.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration_hotfix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Cars",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Cars");
        }
    }
}
