using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VehicleRental.Migrations
{
    public partial class AddedCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Bookings_BookingDate_VehicleID",
                table: "Bookings",
                columns: new[] { "BookingDate", "VehicleID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Bookings_BookingDate_VehicleID",
                table: "Bookings");
        }
    }
}
