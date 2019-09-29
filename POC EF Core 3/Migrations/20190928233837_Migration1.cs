using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POC_EF_Core_3.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemInstance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<short>(nullable: false),
                    BoundCharacterId = table.Column<long>(nullable: true),
                    CharacterId = table.Column<long>(nullable: false),
                    Design = table.Column<short>(nullable: false),
                    DurabilityPoint = table.Column<int>(nullable: false),
                    ItemDeleteTime = table.Column<DateTime>(nullable: true),
                    ItemVNum = table.Column<short>(nullable: false),
                    Upgrade = table.Column<byte>(nullable: false),
                    Rare = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInstance", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemInstance");
        }
    }
}
