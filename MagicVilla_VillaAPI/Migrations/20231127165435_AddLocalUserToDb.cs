using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalUserToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 27, 18, 54, 35, 25, DateTimeKind.Local).AddTicks(126));

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 27, 18, 54, 35, 25, DateTimeKind.Local).AddTicks(174));

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 27, 18, 54, 35, 25, DateTimeKind.Local).AddTicks(185));

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 27, 18, 54, 35, 25, DateTimeKind.Local).AddTicks(195));

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 27, 18, 54, 35, 25, DateTimeKind.Local).AddTicks(206));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 24, 22, 42, 9, 490, DateTimeKind.Local).AddTicks(2403));

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 24, 22, 42, 9, 490, DateTimeKind.Local).AddTicks(2443));

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 24, 22, 42, 9, 490, DateTimeKind.Local).AddTicks(2446));

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 24, 22, 42, 9, 490, DateTimeKind.Local).AddTicks(2448));

            migrationBuilder.UpdateData(
                table: "Vllias",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 24, 22, 42, 9, 490, DateTimeKind.Local).AddTicks(2450));
        }
    }
}
