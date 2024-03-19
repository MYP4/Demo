using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPad.Api.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class rename_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ratings",
                schema: "main",
                table: "users");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                schema: "main",
                table: "users",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                schema: "main",
                table: "users");

            migrationBuilder.AddColumn<double>(
                name: "Ratings",
                schema: "main",
                table: "users",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
