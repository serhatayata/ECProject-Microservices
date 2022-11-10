using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EC.Services.Order.Infrastructure.Migrations
{
    public partial class mig_v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentNo",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentNo",
                table: "Orders");
        }
    }
}
