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
                name: "PopulationCount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    year = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<int>(type: "int", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reliabilty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopulationCount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopulationCount_countryItems_CountryItemId",
                        column: x => x.CountryItemId,
                        principalTable: "countryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopulationCount_CountryItemId",
                table: "PopulationCount",
                column: "CountryItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopulationCount");

            migrationBuilder.DropTable(
                name: "countryItems");
        }
    }
}
