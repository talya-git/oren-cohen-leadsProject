using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadsProject.Migrations
{
    /// <inheritdoc />
    public partial class AddObjectionsAndReferralProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Objections",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferralProject",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Objections", "ReferralProject" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Objections", "ReferralProject" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Objections", "ReferralProject" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Objections", "ReferralProject" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Objections", "ReferralProject" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$evRrSPSnqZnwEbUQ0jHmiuM2BDWw8J/ciBtRTUlWqibe3iIAjWVI.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Objections",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "ReferralProject",
                table: "Leads");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$SJRbg5jXtT85yQLkRChLYuvRDd5Ot5c2TgDMG5ucG.IMQ65OA/RZq");
        }
    }
}
