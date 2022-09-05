using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EC.IdentityServer.Migrations
{
    public partial class mig_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(26)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cvv = table.Column<string>(type: "nvarchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
