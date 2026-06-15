using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadsProject.Migrations
{
    /// <inheritdoc />
    public partial class AddAmenitiesFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirDirections",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Amenities",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NearBy",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "AirDirections", "Amenities", "Area", "AssignedToId", "Budget", "ContactName", "CreatedAt", "Financing", "Floor", "Intent", "NearBy", "Notes", "Phone", "PropertyType", "Rating", "Rooms", "Status", "Timeline", "Transcript", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, null, "תל אביב - פלורנטין", 2, "2,500,000", "דוד לוי", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "הון עצמי + משכנתא", "3", "קניה", null, "לקוח רציני מאוד, יש לו אישור עקרוני מהבנק. לחזור אליו עד יום ג'", "054-7654321", "דירה", "hot", "4", "assigned", "3 חודשים", "[\n  {\"speaker\":\"agent\",\"text\":\"שלום, אני מאריה כהן גרופ, איך אוכל לעזור?\"},\n  {\"speaker\":\"client\",\"text\":\"שלום, ראיתי מודעה על דירת 4 חדרים בתל אביב\"},\n  {\"speaker\":\"agent\",\"text\":\"כן בטח, באיזה אזור בתל אביב אתה מחפש?\"},\n  {\"speaker\":\"client\",\"text\":\"רצוי פלורנטין או לב תל אביב\"},\n  {\"speaker\":\"agent\",\"text\":\"מה התקציב שלך?\"},\n  {\"speaker\":\"client\",\"text\":\"בערך שניים וחצי מיליון, יש לי הון עצמי של 800 אלף\"},\n  {\"speaker\":\"agent\",\"text\":\"ומתי אתה מתכנן לקנות?\"},\n  {\"speaker\":\"client\",\"text\":\"תוך 3 חודשים, אני כבר רציני בעניין\"},\n  {\"speaker\":\"agent\",\"text\":\"מעולה, אשלח לך כמה נכסים רלוונטיים. מה הטלפון שלך?\"},\n  {\"speaker\":\"client\",\"text\":\"054-7654321\"}\n]", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, null, null, "השרון - נתניה / חדרה", 3, "1,500,000", "יעקב לוי", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "הון עצמי", "2", "השקעה", null, "מחפש תשואה מעל 4%, פנוי לפגישה בסוף שבוע", "052-1112233", "דירה", "warm", "3", "assigned", "גמיש", "[\n  {\"speaker\":\"agent\",\"text\":\"בוקר טוב, כאן משרד אורן כהן גרופ\"},\n  {\"speaker\":\"client\",\"text\":\"בוקר טוב, חיפשתי דירה להשקעה באזור השרון\"},\n  {\"speaker\":\"agent\",\"text\":\"בשמחה, מה התקציב שעומד לרשותך?\"},\n  {\"speaker\":\"client\",\"text\":\"עד מיליון וחצי, להשכרה אחר כך\"},\n  {\"speaker\":\"agent\",\"text\":\"כמה חדרים אתה מחפש?\"},\n  {\"speaker\":\"client\",\"text\":\"3 חדרים מספיק, הכי חשוב לי תשואה טובה\"},\n  {\"speaker\":\"agent\",\"text\":\"יש לנו כמה נכסים מעניינים בנתניה וחדרה. תרצה שנקבע פגישה?\"},\n  {\"speaker\":\"client\",\"text\":\"כן בהחלט, אני פנוי בסוף השבוע\"},\n  {\"speaker\":\"agent\",\"text\":\"מצוין, מה שמך?\"},\n  {\"speaker\":\"client\",\"text\":\"יעקב לוי, 052-1112233\"}\n]", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, null, null, "ירושלים - בקעה / רחביה", null, "7,000 לחודש", "שרה כץ", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "שכירות", null, "זוג + ילד, צריכים לעבור בעוד חודשיים", "050-9876543", "דירה", "warm", "3", "new", "חודשיים", "[\n  {\"speaker\":\"agent\",\"text\":\"שלום, במה אוכל לסייע?\"},\n  {\"speaker\":\"client\",\"text\":\"שלום, אני מחפשת דירת שכירות לזוג בירושלים\"},\n  {\"speaker\":\"agent\",\"text\":\"באיזה אזור בירושלים?\"},\n  {\"speaker\":\"client\",\"text\":\"קרמי עדה, בקעה, או רחביה\"},\n  {\"speaker\":\"agent\",\"text\":\"מה התקציב לשכירות לחודש?\"},\n  {\"speaker\":\"client\",\"text\":\"עד 7,000 שקל בחודש\"},\n  {\"speaker\":\"agent\",\"text\":\"כמה חדרים?\"},\n  {\"speaker\":\"client\",\"text\":\"3 חדרים, אנחנו זוג ועוד ילד אחד\"},\n  {\"speaker\":\"agent\",\"text\":\"מתי אתם צריכים להיכנס?\"},\n  {\"speaker\":\"client\",\"text\":\"בעוד חודשיים, הסכם קיים נגמר\"},\n  {\"speaker\":\"agent\",\"text\":\"בסדר גמור, נשלח לך אפשרויות. מה הטלפון?\"},\n  {\"speaker\":\"client\",\"text\":\"050-9876543, שרה כץ\"}\n]", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, null, null, "הרצליה פיתוח", null, "3,200,000", "משה אברהם", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "הון עצמי", "8", "קניה", null, "עדיין בשלב בדיקה, לא בטוח בתקציב", "053-4445566", "פנטהאוז", "cold", "5", "new", "6 חודשים", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, null, null, "ראשון לציון", 4, "1,800,000", "רחל גולן", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "משכנתא", "1", "קניה", null, "דחוף! פגישה נקבעה ליום חמישי. מחפשת קרקע קרובה לבית ספר", "058-7778899", "דירה", "hot", "4", "assigned", "מיידי", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$Yc7AhlX5GdAB5Uo3pwwq5.x3JNU0b6IS.BFytSlypFI6HhJIVK6hS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$J4gGQeJRltGn9q594unCbOMw471hfmhVrbNF5Nua4R92aptRjhpNq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$lRFYZdTWBi7iycJhKwl58efQYeybY7nJ6h5jKHovGrEcsqL/JdAhG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$dK83xnSMQ6O3cavxh1zkzeUDNZRlrUKsOVDRbc61DeFssvPEeSG8q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$8EnqgmIefRsLVdQAcWjEouOIDnfeiIoVIdC8TtRE2cmpKR6N6Id8e" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$8s9EDd/z37dfHZUep1Buo.He8arhqme/q2YofEKm2TqmrsjweKyou" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$Qj2E5n3cQrKoCkPokufIGeCX6L5dPuofmEL0bRWfdmAeENfcq9fpC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BV6R16KzKqWy9N0YpUDCvOsY4wC4gTF801GqpGzh2x0zNLDICXuJi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "AirDirections",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Amenities",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "NearBy",
                table: "Leads");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8534), "$2a$11$GaudFGzH2pr2sKekyAFkheoO3s8s40N/ymHliPm7tjQBlBy./i9GC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8554), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8557), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8559), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8560), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8590), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8592), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 14, 11, 13, 26, 869, DateTimeKind.Utc).AddTicks(8593), null });
        }
    }
}
