using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTourLogDateFormatToDDMMYYYY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "TourLogs",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "TourDtoId",
                table: "TourLogs",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourLogs_TourDtoId",
                table: "TourLogs",
                column: "TourDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourLogs_Tours_TourDtoId",
                table: "TourLogs",
                column: "TourDtoId",
                principalTable: "Tours",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourLogs_Tours_TourDtoId",
                table: "TourLogs");

            migrationBuilder.DropIndex(
                name: "IX_TourLogs_TourDtoId",
                table: "TourLogs");

            migrationBuilder.DropColumn(
                name: "TourDtoId",
                table: "TourLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "TourLogs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
