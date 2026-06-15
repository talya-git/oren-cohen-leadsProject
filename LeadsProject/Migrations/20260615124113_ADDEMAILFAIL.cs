using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadsProject.Migrations
{
    /// <inheritdoc />
    public partial class ADDEMAILFAIL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Source" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Source" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Source" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "Source" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Source" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$8sFUlGri4aNtVsq19hUUkuJ9.c657tfashGWFwN5gJyW1buWEPN5e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Leads");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$dbmf3NkHS1vlCeQsItm/N.vH0/V/3djqlaTlY8Ju/2m6dWT6W8QCu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$uNZfTiZUOcvitu0XVrwV3OrrE/NHXQAKyaZNrBwjbF5XCBlP5pkDu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$rQlA.QIpOLIyq4V9KNPV0.GGV.AhlLcJ7AJR98ws65Axfpm7vgtF2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$inJ3SalgBlZlCbfSvQdlVeWDkFuVNa..RUau3iRkaNiHGZrY3l6bu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$11$DHln4/xpzT.frwOBpj9Cmey2Cl2n0HOTBsbcNeu3hSf07fJhFGsre");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$11$zwWqrHSExHrLVynIKASTeuCAfUV1v6FCdEjtXtw4NjGLHnq/rtXM6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$11$p3l0YLvuCMhRy2bz1qhT0OumFpniQQH/gGIAMXwdqB7rf2vDvwGqO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "$2a$11$K6hgNYxIbsuNXr8teZdzUuNh5lbogkxR8lUmrqqQPDHJ7B3UH7Gby");
        }
    }
}
