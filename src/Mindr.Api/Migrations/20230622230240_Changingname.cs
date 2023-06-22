using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mindr.Api.Migrations
{
    public partial class Changingname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectorEventSteps");

            migrationBuilder.CreateTable(
                name: "ConnectorEventStep",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepIndex = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectorEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectorEventStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectorEventStep_ConnectorEvents_ConnectorEventId",
                        column: x => x.ConnectorEventId,
                        principalTable: "ConnectorEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectorEventStep_ConnectorEventId",
                table: "ConnectorEventStep",
                column: "ConnectorEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectorEventStep");

            migrationBuilder.CreateTable(
                name: "ConnectorEventSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConnectorEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Key = table.Column<int>(type: "int", nullable: false),
                    StepIndex = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectorEventSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectorEventSteps_ConnectorEvents_ConnectorEventId",
                        column: x => x.ConnectorEventId,
                        principalTable: "ConnectorEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectorEventSteps_ConnectorEventId",
                table: "ConnectorEventSteps",
                column: "ConnectorEventId");
        }
    }
}
