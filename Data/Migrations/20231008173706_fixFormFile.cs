using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AribTask.Data.Migrations
{
    public partial class fixFormFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTaskDto");

            migrationBuilder.DropTable(
                name: "EmployeeDto");

            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "EmployeeDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationUserId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationUserId = table.Column<int>(type: "int", nullable: true),
                    EmployeeDtoId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
    }
}
