using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EC.Services.Order.Infrastructure.Migrations
{
    public partial class mig_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "OrderItems",
                type: "nvarchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "OrderItems",
                type: "nvarchar(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)");
        }
    }
}
