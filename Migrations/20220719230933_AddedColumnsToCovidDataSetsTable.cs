using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CovidReportsApi.Migrations
{
    public partial class AddedColumnsToCovidDataSetsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataSetProviderLongName",
                table: "CovidDataSets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DataSetProviderShortName",
                table: "CovidDataSets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSetProviderLongName",
                table: "CovidDataSets");

            migrationBuilder.DropColumn(
                name: "DataSetProviderShortName",
                table: "CovidDataSets");
        }
    }
}
