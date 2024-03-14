using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPad.Pay.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class rename_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transactions_DateTime",
                schema: "pay",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "DateTime",
                schema: "pay",
                table: "transactions");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                schema: "pay",
                table: "transactions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                schema: "pay",
                table: "transactions",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                schema: "pay",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "pay",
                table: "transactions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                schema: "pay",
                table: "transactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_transactions_DateTime",
                schema: "pay",
                table: "transactions",
                column: "DateTime",
                unique: true);
        }
    }
}
