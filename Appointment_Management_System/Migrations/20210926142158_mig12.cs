using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointment_Management_System.Migrations
{
    public partial class mig12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "isDeleted",
                table: "AppUsers",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "isDeleted",
                table: "AppointmentInfo",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AppointmentInfo");
        }
    }
}
