using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coursework.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Places");

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "Places",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Places_DistrictId",
                table: "Places",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_District_DistrictId",
                table: "Places",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_District_DistrictId",
                table: "Places");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropIndex(
                name: "IX_Places_DistrictId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Places");

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Places",
                type: "text",
                nullable: true);
        }
    }
}
