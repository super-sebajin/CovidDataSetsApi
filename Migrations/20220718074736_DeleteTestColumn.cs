using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CovidReportsApi.Migrations
{
    public partial class DeleteTestColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestColumn",
                table: "CovidDataSets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestColumn",
                table: "CovidDataSets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
