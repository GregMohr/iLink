using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace exam4.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    created_at = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    first = table.Column<string>(nullable: false),
                    last = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    connectionId = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    id = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.connectionId);
                    table.ForeignKey(
                        name: "FK_Connections_Users_connectionId",
                        column: x => x.connectionId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invites",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    created_at = table.Column<DateTime>(nullable: false),
                    invitedId = table.Column<int>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invites", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invites_Users_invitedId",
                        column: x => x.invitedId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invites_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_connectionId",
                table: "Connections",
                column: "connectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_userId",
                table: "Connections",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_invitedId",
                table: "Invites",
                column: "invitedId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_userId",
                table: "Invites",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Invites");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
