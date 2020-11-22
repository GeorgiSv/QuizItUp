using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizItUp.Data.Migrations
{
    public partial class PublishAndTrophies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Quizes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Trophies",
                table: "Quizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Trophies",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "Trophies",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "Trophies",
                table: "AspNetUsers");
        }
    }
}
