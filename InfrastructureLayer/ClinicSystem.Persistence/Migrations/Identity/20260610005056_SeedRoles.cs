using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClinicSystem.Persistence.Migrations.Identity
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "c18292cc-4e12-43e8-8245-19f4d2406dfc", "Admin", "ADMIN" },
                    { "2", "b8f9606b-ac6b-4ae1-9a82-2bc25a1a3a86", "Doctor", "DOCTOR" },
                    { "3", "787bb0da-50c3-40be-96c9-7446a28aeb03", "Receptionist", "RECEPTIONIST" },
                    { "4", "a9374842-08d3-4ecb-8de5-de5a50ee69e5", "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4");
        }
    }
}
