using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EC.IdentityServer.Migrations
{
    public partial class mig_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cards");
        }
    }
}
