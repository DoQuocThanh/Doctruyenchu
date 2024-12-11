using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationInfrastructure.Migrations
{
    public partial class Update_Story_View_2h09_12102024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "View",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "View",
                table: "Stories");
        }
    }
}
