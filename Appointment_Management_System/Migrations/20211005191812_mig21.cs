using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointment_Management_System.Migrations
{
    public partial class mig21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstitutionName",
                table: "AppointmentInfo");

            migrationBuilder.DropColumn(
                name: "TranslatorName",
                table: "AppointmentInfo");

            migrationBuilder.AddColumn<long>(
                name: "InstitutionId",
                table: "AppointmentInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TranslatorId",
                table: "AppointmentInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "AppointmentInfo");

            migrationBuilder.DropColumn(
                name: "TranslatorId",
                table: "AppointmentInfo");

            migrationBuilder.AddColumn<string>(
                name: "InstitutionName",
                table: "AppointmentInfo",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TranslatorName",
                table: "AppointmentInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
