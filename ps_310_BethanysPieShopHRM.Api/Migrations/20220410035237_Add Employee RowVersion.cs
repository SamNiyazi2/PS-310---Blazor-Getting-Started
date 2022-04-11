using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BethanysPieShopHRM.Api.Migrations
{
    public partial class AddEmployeeRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "PS_310",
                table: "Employees",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "PS_310",
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Gender",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "PS_310",
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "Gender",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "PS_310",
                table: "Employees");

            migrationBuilder.UpdateData(
                schema: "PS_310",
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Gender",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "PS_310",
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "Gender",
                value: 0);
        }
    }
}
