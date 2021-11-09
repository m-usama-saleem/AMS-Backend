using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointment_Management_System.Migrations
{
    public partial class mig36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "AppointmentInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AppointmentInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedReason",
                table: "AppointmentInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AppointmentInfo");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AppointmentInfo");

            migrationBuilder.DropColumn(
                name: "DeletedReason",
                table: "AppointmentInfo");
        }
    }
}
