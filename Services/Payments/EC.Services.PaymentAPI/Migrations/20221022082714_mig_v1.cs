using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EC.Services.PaymentAPI.Migrations
{
    public partial class mig_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentNo = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    CDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)2),
                    PhoneCountry = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(60)", nullable: true),
                    CardName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CountyName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AddressDetail = table.Column<string>(type: "nvarchar(240)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentNo",
                table: "Payments",
                column: "PaymentNo",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
