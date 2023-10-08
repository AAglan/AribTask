using Microsoft.EntityFrameworkCore.Migrations;

namespace AribTask.Data.Migrations
{
    public partial class fixrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "EmployeesDepartments",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_EmployeesDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeesDepartments_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesDepartments_DepartmentId",
                table: "EmployeesDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesDepartments_ManagerId",
                table: "EmployeesDepartments",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesDepartments");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
