using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship_2022.Infrastructure.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorites_ListingId",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Messages",
                newName: "Content");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Listings",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ListingId",
                table: "Favorites",
                column: "ListingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorites_ListingId",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Messages",
                newName: "content");

            migrationBuilder.AlterColumn<bool>(
                name: "Category",
                table: "Listings",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ListingId",
                table: "Favorites",
                column: "ListingId",
                unique: true);
        }
    }
}
