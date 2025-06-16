using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixTourForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "TourId",
                table: "TourLogs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TourLogs_TourId",
                table: "TourLogs",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourLogs_Tours_TourId",
                table: "TourLogs",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourLogs_Tours_TourId",
                table: "TourLogs");

            migrationBuilder.DropIndex(
                name: "IX_TourLogs_TourId",
                table: "TourLogs");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "TourLogs");

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
    }
}
