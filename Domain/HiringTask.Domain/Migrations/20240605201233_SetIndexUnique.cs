using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiringTask.Domain.Migrations
{
    /// <inheritdoc />
    public partial class SetIndexUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationalCode",
                table: "Employees",
                column: "NationalCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonalCode",
                table: "Employees",
                column: "PersonalCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_NationalCode",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PersonalCode",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
