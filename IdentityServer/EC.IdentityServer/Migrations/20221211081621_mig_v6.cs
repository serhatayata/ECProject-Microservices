using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EC.IdentityServer.Migrations
{
    public partial class mig_v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneCountry",
                table: "Users",
                newName: "CountryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "Users",
                newName: "PhoneCountry");
        }
    }
}
