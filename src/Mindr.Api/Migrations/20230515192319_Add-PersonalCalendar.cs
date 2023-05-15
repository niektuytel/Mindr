using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mindr.Api.Migrations
{
    public partial class AddPersonalCalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "PersonalCredentials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PersonalCalendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalendarId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalCalendars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalCalendars");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "PersonalCredentials");
        }
    }
}
