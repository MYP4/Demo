using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventPad.Pay.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pay");

            migrationBuilder.CreateTable(
                name: "event_accounts",
                schema: "pay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNumber = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Balance = table.Column<float>(type: "real", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_accounts",
                schema: "pay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNumber = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Balance = table.Column<float>(type: "real", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                schema: "pay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    EventAccountId = table.Column<int>(type: "integer", nullable: false),
                    UserAccountId = table.Column<int>(type: "integer", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: true),
                    Ticket = table.Column<Guid>(type: "uuid", maxLength: 16, nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactions_event_accounts_EventAccountId",
                        column: x => x.EventAccountId,
                        principalSchema: "pay",
                        principalTable: "event_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transactions_user_accounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalSchema: "pay",
                        principalTable: "user_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_accounts_AccountNumber",
                schema: "pay",
                table: "event_accounts",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_event_accounts_Uid",
                schema: "pay",
                table: "event_accounts",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transactions_DateTime",
                schema: "pay",
                table: "transactions",
                column: "DateTime",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transactions_EventAccountId",
                schema: "pay",
                table: "transactions",
                column: "EventAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_Uid",
                schema: "pay",
                table: "transactions",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transactions_UserAccountId",
                schema: "pay",
                table: "transactions",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_user_accounts_AccountNumber",
                schema: "pay",
                table: "user_accounts",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_accounts_Uid",
                schema: "pay",
                table: "user_accounts",
                column: "Uid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions",
                schema: "pay");

            migrationBuilder.DropTable(
                name: "event_accounts",
                schema: "pay");

            migrationBuilder.DropTable(
                name: "user_accounts",
                schema: "pay");
        }
    }
}
