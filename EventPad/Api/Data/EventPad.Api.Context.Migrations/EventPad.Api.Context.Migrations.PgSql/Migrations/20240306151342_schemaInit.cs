using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPad.Api.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class schemaInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.RenameTable(
                name: "specific_events",
                newName: "specific_events",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "events",
                newName: "events",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "event_tickets",
                newName: "event_tickets",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "event_photos",
                newName: "event_photos",
                newSchema: "main");

            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                schema: "main",
                table: "events",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "specific_events",
                schema: "main",
                newName: "specific_events");

            migrationBuilder.RenameTable(
                name: "events",
                schema: "main",
                newName: "events");

            migrationBuilder.RenameTable(
                name: "event_tickets",
                schema: "main",
                newName: "event_tickets");

            migrationBuilder.RenameTable(
                name: "event_photos",
                schema: "main",
                newName: "event_photos");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "events",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
