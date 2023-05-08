using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mindr.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectorEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConnectorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectorColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectorEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Connectors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HttpBodyOptionRaw",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpBodyOptionRaw", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HttpRequestUrl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Raw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Protocol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpRequestUrl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectorEventParameter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectorEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectorEventParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectorEventParameter_ConnectorEvents_ConnectorEventId",
                        column: x => x.ConnectorEventId,
                        principalTable: "ConnectorEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConnectorVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectorEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConnectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectorVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectorVariables_ConnectorEvents_ConnectorEventId",
                        column: x => x.ConnectorEventId,
                        principalTable: "ConnectorEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConnectorVariables_Connectors_ConnectorId",
                        column: x => x.ConnectorId,
                        principalTable: "Connectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HttpBodyOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RawId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpBodyOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpBodyOption_HttpBodyOptionRaw_RawId",
                        column: x => x.RawId,
                        principalTable: "HttpBodyOptionRaw",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HttpRequestUrlQuery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HttpRequestUrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpRequestUrlQuery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpRequestUrlQuery_HttpRequestUrl_HttpRequestUrlId",
                        column: x => x.HttpRequestUrlId,
                        principalTable: "HttpRequestUrl",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HttpBody",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Raw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpBody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpBody_HttpBodyOption_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "HttpBodyOption",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HttpRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpRequest_HttpBody_BodyId",
                        column: x => x.BodyId,
                        principalTable: "HttpBody",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HttpRequest_HttpRequestUrl_UrlId",
                        column: x => x.UrlId,
                        principalTable: "HttpRequestUrl",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HttpItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsLoading = table.Column<bool>(type: "bit", nullable: false),
                    ConnectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpItems_Connectors_ConnectorId",
                        column: x => x.ConnectorId,
                        principalTable: "Connectors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HttpItems_HttpRequest_RequestId",
                        column: x => x.RequestId,
                        principalTable: "HttpRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HttpResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HttpItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    PostmanPreviewLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpResponse_HttpItems_HttpItemId",
                        column: x => x.HttpItemId,
                        principalTable: "HttpItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HttpResponse_HttpRequest_OriginalRequestId",
                        column: x => x.OriginalRequestId,
                        principalTable: "HttpRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HttpCookie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expires = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpCookie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpCookie_HttpResponse_HttpResponseId",
                        column: x => x.HttpResponseId,
                        principalTable: "HttpResponse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HttpHeader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HttpRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HttpResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpHeader_HttpRequest_HttpRequestId",
                        column: x => x.HttpRequestId,
                        principalTable: "HttpRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HttpHeader_HttpResponse_HttpResponseId",
                        column: x => x.HttpResponseId,
                        principalTable: "HttpResponse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HttpVariable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    HttpRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HttpResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpVariable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpVariable_HttpRequest_HttpRequestId",
                        column: x => x.HttpRequestId,
                        principalTable: "HttpRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HttpVariable_HttpResponse_HttpResponseId",
                        column: x => x.HttpResponseId,
                        principalTable: "HttpResponse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectorEventParameter_ConnectorEventId",
                table: "ConnectorEventParameter",
                column: "ConnectorEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectorVariables_ConnectorEventId",
                table: "ConnectorVariables",
                column: "ConnectorEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectorVariables_ConnectorId",
                table: "ConnectorVariables",
                column: "ConnectorId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpBody_OptionsId",
                table: "HttpBody",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpBodyOption_RawId",
                table: "HttpBodyOption",
                column: "RawId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpCookie_HttpResponseId",
                table: "HttpCookie",
                column: "HttpResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpHeader_HttpRequestId",
                table: "HttpHeader",
                column: "HttpRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpHeader_HttpResponseId",
                table: "HttpHeader",
                column: "HttpResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpItems_ConnectorId",
                table: "HttpItems",
                column: "ConnectorId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpItems_RequestId",
                table: "HttpItems",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpRequest_BodyId",
                table: "HttpRequest",
                column: "BodyId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpRequest_UrlId",
                table: "HttpRequest",
                column: "UrlId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpRequestUrlQuery_HttpRequestUrlId",
                table: "HttpRequestUrlQuery",
                column: "HttpRequestUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpResponse_HttpItemId",
                table: "HttpResponse",
                column: "HttpItemId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpResponse_OriginalRequestId",
                table: "HttpResponse",
                column: "OriginalRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpVariable_HttpRequestId",
                table: "HttpVariable",
                column: "HttpRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpVariable_HttpResponseId",
                table: "HttpVariable",
                column: "HttpResponseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectorEventParameter");

            migrationBuilder.DropTable(
                name: "ConnectorVariables");

            migrationBuilder.DropTable(
                name: "HttpCookie");

            migrationBuilder.DropTable(
                name: "HttpHeader");

            migrationBuilder.DropTable(
                name: "HttpRequestUrlQuery");

            migrationBuilder.DropTable(
                name: "HttpVariable");

            migrationBuilder.DropTable(
                name: "ConnectorEvents");

            migrationBuilder.DropTable(
                name: "HttpResponse");

            migrationBuilder.DropTable(
                name: "HttpItems");

            migrationBuilder.DropTable(
                name: "Connectors");

            migrationBuilder.DropTable(
                name: "HttpRequest");

            migrationBuilder.DropTable(
                name: "HttpBody");

            migrationBuilder.DropTable(
                name: "HttpRequestUrl");

            migrationBuilder.DropTable(
                name: "HttpBodyOption");

            migrationBuilder.DropTable(
                name: "HttpBodyOptionRaw");
        }
    }
}
