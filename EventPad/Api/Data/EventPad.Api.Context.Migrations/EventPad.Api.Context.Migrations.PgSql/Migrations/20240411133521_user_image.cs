using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPad.Api.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class user_image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                schema: "main",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                schema: "main",
                table: "users");
        }
    }
}
