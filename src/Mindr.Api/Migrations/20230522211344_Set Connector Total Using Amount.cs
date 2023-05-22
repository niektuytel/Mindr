using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mindr.Api.Migrations
{
    public partial class SetConnectorTotalUsingAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "PersonalCalendars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PersonalCalendars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "PersonalCalendars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "PersonalCalendars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalUsing",
                table: "Connectors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "PersonalCalendars");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PersonalCalendars");

            migrationBuilder.DropColumn(
                name: "Selected",
                table: "PersonalCalendars");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "PersonalCalendars");

            migrationBuilder.DropColumn(
                name: "TotalUsing",
                table: "Connectors");
        }
    }
}
