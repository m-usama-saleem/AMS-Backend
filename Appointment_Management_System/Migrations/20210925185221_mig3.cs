using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointment_Management_System.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppointmentId",
                table: "AppUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AppointmentId",
                table: "AppUsers",
                column: "AppointmentId",
                unique: true,
                filter: "[AppointmentId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppUsers_AppointmentId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "AppUsers");
        }
    }
}
