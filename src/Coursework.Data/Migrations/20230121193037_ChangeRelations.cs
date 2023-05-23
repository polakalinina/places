using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coursework.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeLogos_Types_TypeId",
                table: "TypeLogos");

            migrationBuilder.DropIndex(
                name: "IX_TypeLogos_TypeId",
                table: "TypeLogos");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "TypeLogos");

            migrationBuilder.AddColumn<Guid>(
                name: "LogoId",
                table: "Types",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Types_LogoId",
                table: "Types",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_TypeLogos_LogoId",
                table: "Types",
                column: "LogoId",
                principalTable: "TypeLogos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_TypeLogos_LogoId",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_LogoId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Types");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "TypeLogos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeLogos_TypeId",
                table: "TypeLogos",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeLogos_Types_TypeId",
                table: "TypeLogos",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id");
        }
    }
}
