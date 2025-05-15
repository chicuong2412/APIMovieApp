using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class LogoutJWTTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_logoutJWTs",
                table: "logoutJWTs");

            migrationBuilder.RenameTable(
                name: "refreshTokens",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "logoutJWTs",
                newName: "LogoutJWTs");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "LogoutJWTs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogoutJWTs",
                table: "LogoutJWTs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryMovie",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryMovie", x => new { x.CountriesId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CountryMovie_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenereMovie",
                columns: table => new
                {
                    GeneresId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenereMovie", x => new { x.GeneresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenereMovie_Generes_GeneresId",
                        column: x => x.GeneresId,
                        principalTable: "Generes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenereMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieProductionCompany",
                columns: table => new
                {
                    MoviesId = table.Column<int>(type: "int", nullable: false),
                    ProductionCompaniesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieProductionCompany", x => new { x.MoviesId, x.ProductionCompaniesId });
                    table.ForeignKey(
                        name: "FK_MovieProductionCompany_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieProductionCompany_ProductionCompanies_ProductionCompaniesId",
                        column: x => x.ProductionCompaniesId,
                        principalTable: "ProductionCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryMovie_MoviesId",
                table: "CountryMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_GenereMovie_MoviesId",
                table: "GenereMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieProductionCompany_ProductionCompaniesId",
                table: "MovieProductionCompany",
                column: "ProductionCompaniesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryMovie");

            migrationBuilder.DropTable(
                name: "GenereMovie");

            migrationBuilder.DropTable(
                name: "MovieProductionCompany");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Generes");

            migrationBuilder.DropTable(
                name: "ProductionCompanies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogoutJWTs",
                table: "LogoutJWTs");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "LogoutJWTs");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "refreshTokens");

            migrationBuilder.RenameTable(
                name: "LogoutJWTs",
                newName: "logoutJWTs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_logoutJWTs",
                table: "logoutJWTs",
                column: "Id");
        }
    }
}
