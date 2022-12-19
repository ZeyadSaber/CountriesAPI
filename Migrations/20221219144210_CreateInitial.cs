using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountriesCapitalAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PopulationCounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reliabilty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopulationCounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopulationCounts_countryItems_CountryItemId",
                        column: x => x.CountryItemId,
                        principalTable: "countryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopulationCounts_CountryItemId",
                table: "PopulationCounts",
                column: "CountryItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopulationCounts");

            migrationBuilder.DropTable(
                name: "countryItems");
        }
    }
}
