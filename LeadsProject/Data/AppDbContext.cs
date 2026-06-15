using Microsoft.EntityFrameworkCore;
using LeadsProject.Models;

namespace LeadsProject.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Lead> Leads => Set<Lead>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "מנהל", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "manager", CreatedAt = seedDate },
            new User { Id = 2, Name = "אריה", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "agent", CreatedAt = seedDate },
            new User { Id = 3, Name = "דב", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "agent", CreatedAt = seedDate },
            new User { Id = 4, Name = "רבקה", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "agent", CreatedAt = seedDate },
            new User { Id = 5, Name = "מוישי", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "agent", CreatedAt = seedDate },
            new User { Id = 6, Name = "מיכאל", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "agent", CreatedAt = seedDate },
            new User { Id = 7, Name = "אהרון", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "agent", CreatedAt = seedDate },
            new User { Id = 8, Name = "ליסה", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "agent", CreatedAt = seedDate }
        );

        var transcript1 = """
            [
              {"speaker":"agent","text":"שלום, אני מאריה כהן גרופ, איך אוכל לעזור?"},
              {"speaker":"client","text":"שלום, ראיתי מודעה על דירת 4 חדרים בתל אביב"},
              {"speaker":"agent","text":"כן בטח, באיזה אזור בתל אביב אתה מחפש?"},
              {"speaker":"client","text":"רצוי פלורנטין או לב תל אביב"},
              {"speaker":"agent","text":"מה התקציב שלך?"},
              {"speaker":"client","text":"בערך שניים וחצי מיליון, יש לי הון עצמי של 800 אלף"},
              {"speaker":"agent","text":"ומתי אתה מתכנן לקנות?"},
              {"speaker":"client","text":"תוך 3 חודשים, אני כבר רציני בעניין"},
              {"speaker":"agent","text":"מעולה, אשלח לך כמה נכסים רלוונטיים. מה הטלפון שלך?"},
              {"speaker":"client","text":"054-7654321"}
            ]
            """;

        var transcript2 = """
            [
              {"speaker":"agent","text":"בוקר טוב, כאן משרד אורן כהן גרופ"},
              {"speaker":"client","text":"בוקר טוב, חיפשתי דירה להשקעה באזור השרון"},
              {"speaker":"agent","text":"בשמחה, מה התקציב שעומד לרשותך?"},
              {"speaker":"client","text":"עד מיליון וחצי, להשכרה אחר כך"},
              {"speaker":"agent","text":"כמה חדרים אתה מחפש?"},
              {"speaker":"client","text":"3 חדרים מספיק, הכי חשוב לי תשואה טובה"},
              {"speaker":"agent","text":"יש לנו כמה נכסים מעניינים בנתניה וחדרה. תרצה שנקבע פגישה?"},
              {"speaker":"client","text":"כן בהחלט, אני פנוי בסוף השבוע"},
              {"speaker":"agent","text":"מצוין, מה שמך?"},
              {"speaker":"client","text":"יעקב לוי, 052-1112233"}
            ]
            """;

        var transcript3 = """
            [
              {"speaker":"agent","text":"שלום, במה אוכל לסייע?"},
              {"speaker":"client","text":"שלום, אני מחפשת דירת שכירות לזוג בירושלים"},
              {"speaker":"agent","text":"באיזה אזור בירושלים?"},
              {"speaker":"client","text":"קרמי עדה, בקעה, או רחביה"},
              {"speaker":"agent","text":"מה התקציב לשכירות לחודש?"},
              {"speaker":"client","text":"עד 7,000 שקל בחודש"},
              {"speaker":"agent","text":"כמה חדרים?"},
              {"speaker":"client","text":"3 חדרים, אנחנו זוג ועוד ילד אחד"},
              {"speaker":"agent","text":"מתי אתם צריכים להיכנס?"},
              {"speaker":"client","text":"בעוד חודשיים, הסכם קיים נגמר"},
              {"speaker":"agent","text":"בסדר גמור, נשלח לך אפשרויות. מה הטלפון?"},
              {"speaker":"client","text":"050-9876543, שרה כץ"}
            ]
            """;

        modelBuilder.Entity<Lead>().HasData(
            new Lead
            {
                Id = 1,
                ContactName = "דוד לוי",
                Phone = "054-7654321",
                Budget = "2,500,000",
                Area = "תל אביב - פלורנטין",
                Rooms = "4",
                PropertyType = "דירה",
                Floor = "3",
                Financing = "הון עצמי + משכנתא",
                Timeline = "3 חודשים",
                Intent = "קניה",
                Rating = "hot",
                Status = "assigned",
                AssignedToId = 2,
                Notes = "לקוח רציני מאוד, יש לו אישור עקרוני מהבנק. לחזור אליו עד יום ג'",
                Transcript = transcript1,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Lead
            {
                Id = 2,
                ContactName = "יעקב לוי",
                Phone = "052-1112233",
                Budget = "1,500,000",
                Area = "השרון - נתניה / חדרה",
                Rooms = "3",
                PropertyType = "דירה",
                Floor = "2",
                Financing = "הון עצמי",
                Timeline = "גמיש",
                Intent = "השקעה",
                Rating = "warm",
                Status = "assigned",
                AssignedToId = 3,
                Notes = "מחפש תשואה מעל 4%, פנוי לפגישה בסוף שבוע",
                Transcript = transcript2,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Lead
            {
                Id = 3,
                ContactName = "שרה כץ",
                Phone = "050-9876543",
                Budget = "7,000 לחודש",
                Area = "ירושלים - בקעה / רחביה",
                Rooms = "3",
                PropertyType = "דירה",
                Floor = null,
                Financing = null,
                Timeline = "חודשיים",
                Intent = "שכירות",
                Rating = "warm",
                Status = "new",
                AssignedToId = null,
                Notes = "זוג + ילד, צריכים לעבור בעוד חודשיים",
                Transcript = transcript3,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Lead
            {
                Id = 4,
                ContactName = "משה אברהם",
                Phone = "053-4445566",
                Budget = "3,200,000",
                Area = "הרצליה פיתוח",
                Rooms = "5",
                PropertyType = "פנטהאוז",
                Floor = "8",
                Financing = "הון עצמי",
                Timeline = "6 חודשים",
                Intent = "קניה",
                Rating = "cold",
                Status = "new",
                AssignedToId = null,
                Notes = "עדיין בשלב בדיקה, לא בטוח בתקציב",
                Transcript = null,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Lead
            {
                Id = 5,
                ContactName = "רחל גולן",
                Phone = "058-7778899",
                Budget = "1,800,000",
                Area = "ראשון לציון",
                Rooms = "4",
                PropertyType = "דירה",
                Floor = "1",
                Financing = "משכנתא",
                Timeline = "מיידי",
                Intent = "קניה",
                Rating = "hot",
                Status = "assigned",
                AssignedToId = 4,
                Notes = "דחוף! פגישה נקבעה ליום חמישי. מחפשת קרקע קרובה לבית ספר",
                Transcript = null,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        );
    }
}
