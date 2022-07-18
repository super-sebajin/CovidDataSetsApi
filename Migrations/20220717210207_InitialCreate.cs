using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CovidReportsApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CovidDataSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataSetName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DataSetPublicUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSetPublicUrlHttpMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CovidDataSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CovidCasesOverTimeUsa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountConfirmed = table.Column<int>(type: "int", nullable: false),
                    CountDeath = table.Column<int>(type: "int", nullable: false),
                    CountRecovered = table.Column<int>(type: "int", nullable: false),
                    CovidDataSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CovidCasesOverTimeUsa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CovidCasesOverTimeUsa_CovidDataSets_CovidDataSetId",
                        column: x => x.CovidDataSetId,
                        principalTable: "CovidDataSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CovidCasesOverTimeUsa_CovidDataSetId",
                table: "CovidCasesOverTimeUsa",
                column: "CovidDataSetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CovidCasesOverTimeUsa");

            migrationBuilder.DropTable(
                name: "CovidDataSets");
        }
    }
}
