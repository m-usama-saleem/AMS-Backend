using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointment_Management_System.Migrations
{
    public partial class mig15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Translators",
                table: "Translators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentInfo",
                table: "AppointmentInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translators",
                table: "Translators",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentInfo",
                table: "AppointmentInfo",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Translators",
                table: "Translators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentInfo",
                table: "AppointmentInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translators",
                table: "Translators",
                columns: new[] { "Name", "Language" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentInfo",
                table: "AppointmentInfo",
                column: "AppointmentId");
        }
    }
}
