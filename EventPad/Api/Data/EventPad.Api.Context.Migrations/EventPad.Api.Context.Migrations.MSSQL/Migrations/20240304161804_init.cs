using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPad.Api.Context.Migrations.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Ratings = table.Column<double>(type: "float", nullable: false),
                    Account = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Account = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_users_AdminId",
                        column: x => x.AdminId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_event_photos_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "specific_events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketCount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specific_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_specific_events_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SpecificEventId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_event_tickets_specific_events_SpecificEventId",
                        column: x => x.SpecificEventId,
                        principalTable: "specific_events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_tickets_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_photos_EventId",
                table: "event_photos",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_event_photos_Uid",
                table: "event_photos",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_event_tickets_SpecificEventId",
                table: "event_tickets",
                column: "SpecificEventId");

            migrationBuilder.CreateIndex(
                name: "IX_event_tickets_Uid",
                table: "event_tickets",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_event_tickets_UserId",
                table: "event_tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_events_AdminId",
                table: "events",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_events_Uid",
                table: "events",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_specific_events_EventId",
                table: "specific_events",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_specific_events_Uid",
                table: "specific_events",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Uid",
                table: "users",
                column: "Uid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_photos");

            migrationBuilder.DropTable(
                name: "event_tickets");

            migrationBuilder.DropTable(
                name: "specific_events");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
