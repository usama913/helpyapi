using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpyWebApi.Migrations
{
    public partial class addUserStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UserStatus",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "AspNetUsers");
        }
    }
}
