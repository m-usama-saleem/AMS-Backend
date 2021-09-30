using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointment_Management_System.Migrations
{
    public partial class mig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentInfo",
                table: "AppointmentInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentInfo",
                table: "AppointmentInfo",
                column: "AppointmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentInfo",
                table: "AppointmentInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                columns: new[] { "Id", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentInfo",
                table: "AppointmentInfo",
                columns: new[] { "Id", "AppointmentId" });
        }
    }
}
