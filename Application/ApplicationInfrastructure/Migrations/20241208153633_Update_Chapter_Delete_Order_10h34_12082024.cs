using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationInfrastructure.Migrations
{
    public partial class Update_Chapter_Delete_Order_10h34_12082024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Chapters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
