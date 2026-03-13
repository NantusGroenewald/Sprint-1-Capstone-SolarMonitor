using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SolarMonitor.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialPanels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Panels",
                columns: new[] { "Id", "Brand", "InstallationDate", "Model", "Type" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "SunPower", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maxeon 3", "Monocrystalline" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "LG", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "NeON 2", "Monocrystalline" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Canadian Solar", new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "HiKu", "Polycrystalline" }
                });

            migrationBuilder.InsertData(
                table: "Readings",
                columns: new[] { "Id", "PanelId", "Timestamp", "Voltage", "Watts" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 3, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), 48.200000000000003, 350.5 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 3, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), 48.5, 375.0 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 3, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), 47.799999999999997, 320.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Panels",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Readings",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Readings",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Readings",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Panels",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Panels",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}
