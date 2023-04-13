using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mindr.Api.Migrations
{
    public partial class SetColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectorColor",
                table: "ConnectorEvents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectorColor",
                table: "ConnectorEvents");
        }
    }
}
