using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiringChallange.Persistence.Migrations
{
    public partial class seedcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5f9e774f-2d63-448f-9043-dc7a8e3daf39");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c158ca71-032f-453b-a6d6-9f0ea92071e5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a770c182-c8d1-4532-a021-c45307bb3f16", new DateTime(2022, 7, 7, 3, 8, 20, 375, DateTimeKind.Local).AddTicks(2041), "AQAAAAEAACcQAAAAEJfpA5i5plF3d06AsRxV8OQgVIqMY0EIVKgsfFzehobg64+LpHk6jX1ZxM/1PGB3Og==", "8daa9511-99ad-4d7e-83bd-8ce8da78f1e8" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateDate", "Name" },
                values: new object[,]
                {
                    { "1", new DateTime(2022, 7, 7, 3, 8, 20, 376, DateTimeKind.Local).AddTicks(5421), "Market Alışverişi" },
                    { "2", new DateTime(2022, 7, 7, 3, 8, 20, 376, DateTimeKind.Local).AddTicks(5422), "Pazar Alışverişi" },
                    { "3", new DateTime(2022, 7, 7, 3, 8, 20, 376, DateTimeKind.Local).AddTicks(5423), "Teknoloji Alışverişi" },
                    { "4", new DateTime(2022, 7, 7, 3, 8, 20, 376, DateTimeKind.Local).AddTicks(5424), "Okul Alışverişi" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "026fd50c-119f-42be-a237-aa5cb4caad84");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ae07a105-3eea-42a3-825b-097e08f1ee7a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf3f8aee-6181-4ade-a70c-f4a4fa786ecc", new DateTime(2022, 7, 6, 19, 44, 9, 39, DateTimeKind.Local).AddTicks(6503), "AQAAAAEAACcQAAAAEByC4+1c/dz4L4E4H0J2GwKe0c666AyB6rg5YE3PKJ5YVTtYsAOjiGWx7fjcEVUpMg==", "e0572278-ea8b-4a12-b0b8-91e9574a4781" });
        }
    }
}
