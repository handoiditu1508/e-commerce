using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Persistence.EF.Migrations
{
    public partial class Product_Class__AttributesStates_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AttributesStates",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributesStates",
                table: "Product");
        }
    }
}
