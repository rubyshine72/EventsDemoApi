using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventsDemoAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class events : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MTimezones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    value = table.Column<string>(type: "VARCHAR", maxLength: 127, nullable: false),
                    abbr = table.Column<string>(type: "VARCHAR", maxLength: 127, nullable: false),
                    offset = table.Column<float>(type: "REAL", nullable: false),
                    isdst = table.Column<bool>(type: "INTEGER", nullable: false),
                    text = table.Column<string>(type: "VARCHAR", maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MTimezones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "VARCHAR", maxLength: 511, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    StartAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimezoneId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_MTimezones_TimezoneId",
                        column: x => x.TimezoneId,
                        principalTable: "MTimezones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MTimezones",
                columns: new[] { "Id", "abbr", "isdst", "offset", "text", "value" },
                values: new object[,]
                {
                    { 1, "DST", false, -12f, "(UTC-12:00) International Date Line West", "Dateline Standard Time" },
                    { 2, "U", false, -11f, "(UTC-11:00) Coordinated Universal Time-11", "UTC-11" },
                    { 3, "HST", false, -10f, "(UTC-10:00) Hawaii", "Hawaiian Standard Time" },
                    { 4, "AKDT", true, -8f, "(UTC-09:00) Alaska", "Alaskan Standard Time" },
                    { 5, "PDT", true, -7f, "(UTC-08:00) Baja California", "Pacific Standard Time (Mexico)" },
                    { 6, "PDT", true, -7f, "(UTC-07:00) Pacific Daylight Time (US & Canada)", "Pacific Daylight Time" },
                    { 7, "PST", false, -8f, "(UTC-08:00) Pacific Standard Time (US & Canada)", "Pacific Standard Time" },
                    { 8, "UMST", false, -7f, "(UTC-07:00) Arizona", "US Mountain Standard Time" },
                    { 9, "MDT", true, -6f, "(UTC-07:00) Chihuahua, La Paz, Mazatlan", "Mountain Standard Time (Mexico)" },
                    { 10, "MDT", true, -6f, "(UTC-07:00) Mountain Time (US & Canada)", "Mountain Standard Time" },
                    { 11, "CAST", false, -6f, "(UTC-06:00) Central America", "Central America Standard Time" },
                    { 12, "CDT", true, -5f, "(UTC-06:00) Central Time (US & Canada)", "Central Standard Time" },
                    { 13, "CDT", true, -5f, "(UTC-06:00) Guadalajara, Mexico City, Monterrey", "Central Standard Time (Mexico)" },
                    { 14, "CCST", false, -6f, "(UTC-06:00) Saskatchewan", "Canada Central Standard Time" },
                    { 15, "SPST", false, -5f, "(UTC-05:00) Bogota, Lima, Quito", "SA Pacific Standard Time" },
                    { 16, "EST", false, -5f, "(UTC-05:00) Eastern Time (US & Canada)", "Eastern Standard Time" },
                    { 17, "EDT", true, -4f, "(UTC-04:00) Eastern Daylight Time (US & Canada)", "Eastern Daylight Time" },
                    { 18, "UEDT", false, -5f, "(UTC-05:00) Indiana (East)", "US Eastern Standard Time" },
                    { 19, "VST", false, -4.5f, "(UTC-04:30) Caracas", "Venezuela Standard Time" },
                    { 20, "PYT", false, -4f, "(UTC-04:00) Asuncion", "Paraguay Standard Time" },
                    { 21, "ADT", true, -3f, "(UTC-04:00) Atlantic Time (Canada)", "Atlantic Standard Time" },
                    { 22, "CBST", false, -4f, "(UTC-04:00) Cuiaba", "Central Brazilian Standard Time" },
                    { 23, "SWST", false, -4f, "(UTC-04:00) Georgetown, La Paz, Manaus, San Juan", "SA Western Standard Time" },
                    { 24, "PSST", false, -4f, "(UTC-04:00) Santiago", "Pacific SA Standard Time" },
                    { 25, "NDT", true, -2.5f, "(UTC-03:30) Newfoundland", "Newfoundland Standard Time" },
                    { 26, "ESAST", false, -3f, "(UTC-03:00) Brasilia", "E. South America Standard Time" },
                    { 27, "AST", false, -3f, "(UTC-03:00) Buenos Aires", "Argentina Standard Time" },
                    { 28, "SEST", false, -3f, "(UTC-03:00) Cayenne, Fortaleza", "SA Eastern Standard Time" },
                    { 29, "GDT", true, -3f, "(UTC-03:00) Greenland", "Greenland Standard Time" },
                    { 30, "MST", false, -3f, "(UTC-03:00) Montevideo", "Montevideo Standard Time" },
                    { 31, "BST", false, -3f, "(UTC-03:00) Salvador", "Bahia Standard Time" },
                    { 32, "U", false, -2f, "(UTC-02:00) Coordinated Universal Time-02", "UTC-02" },
                    { 33, "MDT", true, -1f, "(UTC-02:00) Mid-Atlantic - Old", "Mid-Atlantic Standard Time" },
                    { 34, "ADT", true, 0f, "(UTC-01:00) Azores", "Azores Standard Time" },
                    { 35, "CVST", false, -1f, "(UTC-01:00) Cape Verde Is.", "Cape Verde Standard Time" },
                    { 36, "MDT", true, 1f, "(UTC) Casablanca", "Morocco Standard Time" },
                    { 37, "UTC", false, 0f, "(UTC) Coordinated Universal Time", "UTC" },
                    { 38, "GMT", false, 0f, "(UTC) Edinburgh, London", "GMT Standard Time" },
                    { 39, "BST", true, 1f, "(UTC+01:00) Edinburgh, London", "British Summer Time" },
                    { 40, "GDT", true, 1f, "(UTC) Dublin, Lisbon", "GMT Standard Time" },
                    { 41, "GST", false, 0f, "(UTC) Monrovia, Reykjavik", "Greenwich Standard Time" },
                    { 42, "WEDT", true, 2f, "(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna", "W. Europe Standard Time" },
                    { 43, "CEDT", true, 2f, "(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague", "Central Europe Standard Time" },
                    { 44, "RDT", true, 2f, "(UTC+01:00) Brussels, Copenhagen, Madrid, Paris", "Romance Standard Time" },
                    { 45, "CEDT", true, 2f, "(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb", "Central European Standard Time" },
                    { 46, "WCAST", false, 1f, "(UTC+01:00) West Central Africa", "W. Central Africa Standard Time" },
                    { 47, "NST", false, 1f, "(UTC+01:00) Windhoek", "Namibia Standard Time" },
                    { 48, "GDT", true, 3f, "(UTC+02:00) Athens, Bucharest", "GTB Standard Time" },
                    { 49, "MEDT", true, 3f, "(UTC+02:00) Beirut", "Middle East Standard Time" },
                    { 50, "EST", false, 2f, "(UTC+02:00) Cairo", "Egypt Standard Time" },
                    { 51, "SDT", true, 3f, "(UTC+02:00) Damascus", "Syria Standard Time" },
                    { 52, "EEDT", true, 3f, "(UTC+02:00) E. Europe", "E. Europe Standard Time" },
                    { 53, "SAST", false, 2f, "(UTC+02:00) Harare, Pretoria", "South Africa Standard Time" },
                    { 54, "FDT", true, 3f, "(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius", "FLE Standard Time" },
                    { 55, "TDT", false, 3f, "(UTC+03:00) Istanbul", "Turkey Standard Time" },
                    { 56, "JDT", true, 3f, "(UTC+02:00) Jerusalem", "Israel Standard Time" },
                    { 57, "LST", false, 2f, "(UTC+02:00) Tripoli", "Libya Standard Time" },
                    { 58, "JST", false, 3f, "(UTC+03:00) Amman", "Jordan Standard Time" },
                    { 59, "AST", false, 3f, "(UTC+03:00) Baghdad", "Arabic Standard Time" },
                    { 60, "KST", false, 3f, "(UTC+02:00) Kaliningrad", "Kaliningrad Standard Time" },
                    { 61, "AST", false, 3f, "(UTC+03:00) Kuwait, Riyadh", "Arab Standard Time" },
                    { 62, "EAST", false, 3f, "(UTC+03:00) Nairobi", "E. Africa Standard Time" },
                    { 63, "MSK", false, 3f, "(UTC+03:00) Moscow, St. Petersburg, Volgograd, Minsk", "Moscow Standard Time" },
                    { 64, "SAMT", false, 4f, "(UTC+04:00) Samara, Ulyanovsk, Saratov", "Samara Time" },
                    { 65, "IDT", true, 4.5f, "(UTC+03:30) Tehran", "Iran Standard Time" },
                    { 66, "AST", false, 4f, "(UTC+04:00) Abu Dhabi, Muscat", "Arabian Standard Time" },
                    { 67, "ADT", true, 5f, "(UTC+04:00) Baku", "Azerbaijan Standard Time" },
                    { 68, "MST", false, 4f, "(UTC+04:00) Port Louis", "Mauritius Standard Time" },
                    { 69, "GET", false, 4f, "(UTC+04:00) Tbilisi", "Georgian Standard Time" },
                    { 70, "CST", false, 4f, "(UTC+04:00) Yerevan", "Caucasus Standard Time" },
                    { 71, "AST", false, 4.5f, "(UTC+04:30) Kabul", "Afghanistan Standard Time" },
                    { 72, "WAST", false, 5f, "(UTC+05:00) Ashgabat, Tashkent", "West Asia Standard Time" },
                    { 73, "YEKT", false, 5f, "(UTC+05:00) Yekaterinburg", "Yekaterinburg Time" },
                    { 74, "PKT", false, 5f, "(UTC+05:00) Islamabad, Karachi", "Pakistan Standard Time" },
                    { 75, "IST", false, 5.5f, "(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi", "India Standard Time" },
                    { 76, "SLST", false, 5.5f, "(UTC+05:30) Sri Jayawardenepura", "Sri Lanka Standard Time" },
                    { 77, "NST", false, 5.75f, "(UTC+05:45) Kathmandu", "Nepal Standard Time" },
                    { 78, "CAST", false, 6f, "(UTC+06:00) Nur-Sultan (Astana)", "Central Asia Standard Time" },
                    { 79, "BST", false, 6f, "(UTC+06:00) Dhaka", "Bangladesh Standard Time" },
                    { 80, "MST", false, 6.5f, "(UTC+06:30) Yangon (Rangoon)", "Myanmar Standard Time" },
                    { 81, "SAST", false, 7f, "(UTC+07:00) Bangkok, Hanoi, Jakarta", "SE Asia Standard Time" },
                    { 82, "NCAST", false, 7f, "(UTC+07:00) Novosibirsk", "N. Central Asia Standard Time" },
                    { 83, "CST", false, 8f, "(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi", "China Standard Time" },
                    { 84, "NAST", false, 8f, "(UTC+08:00) Krasnoyarsk", "North Asia Standard Time" },
                    { 85, "MPST", false, 8f, "(UTC+08:00) Kuala Lumpur, Singapore", "Singapore Standard Time" },
                    { 86, "WAST", false, 8f, "(UTC+08:00) Perth", "W. Australia Standard Time" },
                    { 87, "TST", false, 8f, "(UTC+08:00) Taipei", "Taipei Standard Time" },
                    { 88, "UST", false, 8f, "(UTC+08:00) Ulaanbaatar", "Ulaanbaatar Standard Time" },
                    { 89, "NAEST", false, 8f, "(UTC+08:00) Irkutsk", "North Asia East Standard Time" },
                    { 90, "JST", false, 9f, "(UTC+09:00) Osaka, Sapporo, Tokyo", "Japan Standard Time" },
                    { 91, "KST", false, 9f, "(UTC+09:00) Seoul", "Korea Standard Time" },
                    { 92, "CAST", false, 9.5f, "(UTC+09:30) Adelaide", "Cen. Australia Standard Time" },
                    { 93, "ACST", false, 9.5f, "(UTC+09:30) Darwin", "AUS Central Standard Time" },
                    { 94, "EAST", false, 10f, "(UTC+10:00) Brisbane", "E. Australia Standard Time" },
                    { 95, "AEST", false, 10f, "(UTC+10:00) Canberra, Melbourne, Sydney", "AUS Eastern Standard Time" },
                    { 96, "WPST", false, 10f, "(UTC+10:00) Guam, Port Moresby", "West Pacific Standard Time" },
                    { 97, "TST", false, 10f, "(UTC+10:00) Hobart", "Tasmania Standard Time" },
                    { 98, "YST", false, 9f, "(UTC+09:00) Yakutsk", "Yakutsk Standard Time" },
                    { 99, "CPST", false, 11f, "(UTC+11:00) Solomon Is., New Caledonia", "Central Pacific Standard Time" },
                    { 100, "VST", false, 11f, "(UTC+11:00) Vladivostok", "Vladivostok Standard Time" },
                    { 101, "NZST", false, 12f, "(UTC+12:00) Auckland, Wellington", "New Zealand Standard Time" },
                    { 102, "U", false, 12f, "(UTC+12:00) Coordinated Universal Time+12", "UTC+12" },
                    { 103, "FST", false, 12f, "(UTC+12:00) Fiji", "Fiji Standard Time" },
                    { 104, "MST", false, 12f, "(UTC+12:00) Magadan", "Magadan Standard Time" },
                    { 105, "KDT", true, 13f, "(UTC+12:00) Petropavlovsk-Kamchatsky - Old", "Kamchatka Standard Time" },
                    { 106, "TST", false, 13f, "(UTC+13:00) Nuku'alofa", "Tonga Standard Time" },
                    { 107, "SST", false, 13f, "(UTC+13:00) Samoa", "Samoa Standard Time" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_TimezoneId",
                table: "Events",
                column: "TimezoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_EventId",
                table: "Participants",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "MTimezones");
        }
    }
}
