using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class RemovingNotUsedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salario",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SalarioAnual",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Salario",
                table: "Employees",
                type: "REAL",
                maxLength: 20,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalarioAnual",
                table: "Employees",
                type: "REAL",
                maxLength: 30,
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
