using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventPad.Api.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    SecondName = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Ratings = table.Column<double>(type: "double precision", nullable: false),
                    Account = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_role_claims",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_role_claims_user_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "main",
                        principalTable: "user_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Account = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_users_AdminId",
                        column: x => x.AdminId,
                        principalSchema: "main",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_claims_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                schema: "main",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_user_logins_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role_owners",
                schema: "main",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_owners", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_user_role_owners_user_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "main",
                        principalTable: "user_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_owners_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                schema: "main",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_user_tokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_photos",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_event_photos_events_EventId",
                        column: x => x.EventId,
                        principalSchema: "main",
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "specific_events",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TicketCount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: true),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Private = table.Column<bool>(type: "boolean", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specific_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_specific_events_events_EventId",
                        column: x => x.EventId,
                        principalSchema: "main",
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_tickets",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecificEventId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_event_tickets_specific_events_SpecificEventId",
                        column: x => x.SpecificEventId,
                        principalSchema: "main",
                        principalTable: "specific_events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_tickets_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_photos_EventId",
                schema: "main",
                table: "event_photos",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_event_photos_Uid",
                schema: "main",
                table: "event_photos",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_event_tickets_SpecificEventId",
                schema: "main",
                table: "event_tickets",
                column: "SpecificEventId");

            migrationBuilder.CreateIndex(
                name: "IX_event_tickets_Uid",
                schema: "main",
                table: "event_tickets",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_event_tickets_UserId",
                schema: "main",
                table: "event_tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_events_AdminId",
                schema: "main",
                table: "events",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_events_Uid",
                schema: "main",
                table: "events",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_specific_events_EventId",
                schema: "main",
                table: "specific_events",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_specific_events_Uid",
                schema: "main",
                table: "specific_events",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_claims_UserId",
                schema: "main",
                table: "user_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_logins_UserId",
                schema: "main",
                table: "user_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_claims_RoleId",
                schema: "main",
                table: "user_role_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_owners_RoleId",
                schema: "main",
                table: "user_role_owners",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "main",
                table: "user_roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "main",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "main",
                table: "users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_photos",
                schema: "main");

            migrationBuilder.DropTable(
                name: "event_tickets",
                schema: "main");

            migrationBuilder.DropTable(
                name: "user_claims",
                schema: "main");

            migrationBuilder.DropTable(
                name: "user_logins",
                schema: "main");

            migrationBuilder.DropTable(
                name: "user_role_claims",
                schema: "main");

            migrationBuilder.DropTable(
                name: "user_role_owners",
                schema: "main");

            migrationBuilder.DropTable(
                name: "user_tokens",
                schema: "main");

            migrationBuilder.DropTable(
                name: "specific_events",
                schema: "main");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "main");

            migrationBuilder.DropTable(
                name: "events",
                schema: "main");

            migrationBuilder.DropTable(
                name: "users",
                schema: "main");
        }
    }
}
