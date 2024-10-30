using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpyWebApi.Migrations
{
    public partial class updateimagesfolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Images");
        }
    }
}
