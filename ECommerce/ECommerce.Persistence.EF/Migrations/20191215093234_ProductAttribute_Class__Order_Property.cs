using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Persistence.EF.Migrations
{
	public partial class ProductAttribute_Class__Order_Property : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<short>(
				name: "Order",
				table: "ProductAttribute",
				nullable: false,
				defaultValue: (short)0);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Order",
				table: "ProductAttribute");
		}
	}
}
