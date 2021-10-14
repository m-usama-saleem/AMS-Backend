using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointment_Management_System.Migrations
{
    public partial class mig27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Translators",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Translators",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Translators",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Translators",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Translators",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Translators",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Translators",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Translators",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Institutions",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Institutions",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppointmentStart",
                table: "Finance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndOfTheAppointment",
                table: "Finance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndOfTheTrip",
                table: "Finance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartOfTheTrip",
                table: "Finance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalHours",
                table: "Finance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "AppointmentInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Institutions");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Institutions");

            migrationBuilder.DropColumn(
                name: "AppointmentStart",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "EndOfTheAppointment",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "EndOfTheTrip",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "StartOfTheTrip",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "AppointmentInfo");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Translators",
                newName: "Name");
        }
    }
}
