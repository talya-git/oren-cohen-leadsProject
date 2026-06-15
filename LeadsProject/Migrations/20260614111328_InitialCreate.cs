using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadsProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Budget = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rooms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Financing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timeline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedToId = table.Column<int>(type: "int", nullable: true),
                    Transcript = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leads_Users_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8534), "מנהל", "$2a$11$GaudFGzH2pr2sKekyAFkheoO3s8s40N/ymHliPm7tjQBlBy./i9GC", "manager" },
                    { 2, new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8554), "אריה", null, "agent" },
                    { 3, new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8557), "דב", null, "agent" },
                    { 4, new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8559), "רבקה", null, "agent" },
                    { 5, new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8560), "מוישי", null, "agent" },
                    { 6, new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8590), "מיכאל", null, "agent" },
                    { 7, new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8592), "אהרון", null, "agent" },
                    { 8, new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8593), "ליסה", null, "agent" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leads_AssignedToId",
                table: "Leads",
                column: "AssignedToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
