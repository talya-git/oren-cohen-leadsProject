using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadsProject.Migrations
{
    /// <inheritdoc />
    public partial class AddOptionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmenityOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NearByOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NearByOptions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$WpIX5gVVSSOfadKSfTOdGeglh6bpcRuKiN.AmhdWxmCnMQ/sdFPpq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityOptions");

            migrationBuilder.DropTable(
                name: "NearByOptions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$O4T8utENGmxpI12ZaaEKRu72d02t069dNACHRyRsZYYc.WdoTkFDq");
        }
    }
}
