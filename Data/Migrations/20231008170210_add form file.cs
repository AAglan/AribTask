using Microsoft.EntityFrameworkCore.Migrations;

namespace AribTask.Data.Migrations
{
    public partial class addformfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Employees",
                newName: "ImageFileName");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "EmployeeDto",
                newName: "ImageFileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                table: "Employees",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                table: "EmployeeDto",
                newName: "ImagePath");
        }
    }
}
