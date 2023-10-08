using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AribTask.Data.Migrations
{
    public partial class removecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "EmployeeTasks");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "EmployeeDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDto_EmployeeDto_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "EmployeeDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTaskDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EmployeeDtoId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTaskDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskDto_EmployeeDto_EmployeeDtoId",
                        column: x => x.EmployeeDtoId,
                        principalTable: "EmployeeDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDto_ManagerId",
                table: "EmployeeDto",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTaskDto_EmployeeDtoId",
                table: "EmployeeTaskDto",
                column: "EmployeeDtoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTaskDto");

            migrationBuilder.DropTable(
                name: "EmployeeDto");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "EmployeeTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
