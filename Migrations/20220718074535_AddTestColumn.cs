using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CovidReportsApi.Migrations
{
    public partial class AddTestColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestColumn",
                table: "CovidDataSets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestColumn",
                table: "CovidDataSets");
        }
    }
}
