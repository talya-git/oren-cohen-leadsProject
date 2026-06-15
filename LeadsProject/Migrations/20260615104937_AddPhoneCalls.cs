using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadsProject.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneCalls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneCalls",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhoneCalls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 2,
                column: "PhoneCalls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 3,
                column: "PhoneCalls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 4,
                column: "PhoneCalls",
                value: null);

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 5,
                column: "PhoneCalls",
                value: null);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneCalls",
                table: "Leads");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$Yc7AhlX5GdAB5Uo3pwwq5.x3JNU0b6IS.BFytSlypFI6HhJIVK6hS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$J4gGQeJRltGn9q594unCbOMw471hfmhVrbNF5Nua4R92aptRjhpNq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$lRFYZdTWBi7iycJhKwl58efQYeybY7nJ6h5jKHovGrEcsqL/JdAhG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$dK83xnSMQ6O3cavxh1zkzeUDNZRlrUKsOVDRbc61DeFssvPEeSG8q");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$11$8EnqgmIefRsLVdQAcWjEouOIDnfeiIoVIdC8TtRE2cmpKR6N6Id8e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$11$8s9EDd/z37dfHZUep1Buo.He8arhqme/q2YofEKm2TqmrsjweKyou");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$11$Qj2E5n3cQrKoCkPokufIGeCX6L5dPuofmEL0bRWfdmAeENfcq9fpC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "$2a$11$BV6R16KzKqWy9N0YpUDCvOsY4wC4gTF801GqpGzh2x0zNLDICXuJi");
        }
    }
}
