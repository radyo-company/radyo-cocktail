using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cocktail.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueCocktailName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cocktail_Name",
                table: "Cocktail",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cocktail_Name",
                table: "Cocktail");
        }
    }
}
