using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FribergCarRentalsRazor.Migrations
{
    /// <inheritdoc />
    public partial class update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customer",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Car",
                newName: "CarId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Booking",
                newName: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customer",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Car",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Booking",
                newName: "Id");
        }
    }
}
