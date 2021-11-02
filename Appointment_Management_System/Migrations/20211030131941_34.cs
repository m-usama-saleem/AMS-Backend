using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointment_Management_System.Migrations
{
    public partial class _34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApprovalBy",
                table: "Finance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "Finance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CompletionBy",
                table: "Finance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Finance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "ApprovalBy",
                table: "AppointmentInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "AppointmentInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CompletionBy",
                table: "AppointmentInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "AppointmentInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalBy",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "CompletionBy",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "ApprovalBy",
                table: "AppointmentInfo");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "AppointmentInfo");

            migrationBuilder.DropColumn(
                name: "CompletionBy",
                table: "AppointmentInfo");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "AppointmentInfo");
        }
    }
}
