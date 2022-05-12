using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DotaWin.Data.Migrations
{
    public partial class correct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Patch = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUpdates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TechnicalName = table.Column<string>(type: "text", nullable: true),
                    ItemType = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    Winrate = table.Column<double>(type: "double precision", nullable: false),
                    UpdateId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heroes_DailyUpdates_UpdateId",
                        column: x => x.UpdateId,
                        principalTable: "DailyUpdates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DbItemDbUpdate",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "integer", nullable: false),
                    UpdatesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbItemDbUpdate", x => new { x.ItemsId, x.UpdatesId });
                    table.ForeignKey(
                        name: "FK_DbItemDbUpdate_DailyUpdates_UpdatesId",
                        column: x => x.UpdatesId,
                        principalTable: "DailyUpdates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbItemDbUpdate_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Matches = table.Column<int>(type: "integer", nullable: false),
                    Winrate = table.Column<double>(type: "double precision", nullable: false),
                    HeroId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    UpdateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroItems_DailyUpdates_UpdateId",
                        column: x => x.UpdateId,
                        principalTable: "DailyUpdates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroItems_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbItemDbUpdate_UpdatesId",
                table: "DbItemDbUpdate",
                column: "UpdatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_UpdateId",
                table: "Heroes",
                column: "UpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroItems_HeroId",
                table: "HeroItems",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroItems_ItemId",
                table: "HeroItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroItems_UpdateId",
                table: "HeroItems",
                column: "UpdateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbItemDbUpdate");

            migrationBuilder.DropTable(
                name: "HeroItems");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "DailyUpdates");
        }
    }
}
