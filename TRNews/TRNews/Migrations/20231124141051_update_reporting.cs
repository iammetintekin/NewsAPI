using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRNews.Migrations
{
    /// <inheritdoc />
    public partial class update_reporting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportings_Categories_CategoryId",
                table: "Reportings");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Reportings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8982d17f-e713-4eb4-9aa8-6bc8e665fcee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "29b79936-91ff-457b-8d74-1b474b528c81");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c36b8e0c-6a74-4a15-bccc-2f1065951fce", "AQAAAAIAAYagAAAAEAzg6QAdoPF/2Q2qP277LBGF7OUmWLBXY9pW1BlPqmu5QCEdZrRgKp6VA6bs87E6vQ==", "8cb07cee-00f5-4f9b-9284-b5f120b9532f" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reportings_Categories_CategoryId",
                table: "Reportings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportings_Categories_CategoryId",
                table: "Reportings");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Reportings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a08b49ea-086e-469e-be8a-10c12e4c3c23");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "49c530a8-21ed-4186-aee8-45a295a670d0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "badcb693-bed0-4b06-ad75-2b9fafe3d9d1", "AQAAAAIAAYagAAAAEDEoZJ/HqoIyX8rGai8akKYal560KUpe1YdU0q2kQmT+k2Bvhrz56f1d92jsnLAlmA==", "13ff3739-4b08-4bee-86b6-2291e7f66055" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reportings_Categories_CategoryId",
                table: "Reportings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
