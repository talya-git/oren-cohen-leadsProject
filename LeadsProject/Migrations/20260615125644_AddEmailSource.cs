using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadsProject.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$SJRbg5jXtT85yQLkRChLYuvRDd5Ot5c2TgDMG5ucG.IMQ65OA/RZq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$8sFUlGri4aNtVsq19hUUkuJ9.c657tfashGWFwN5gJyW1buWEPN5e");
        }
    }
}
