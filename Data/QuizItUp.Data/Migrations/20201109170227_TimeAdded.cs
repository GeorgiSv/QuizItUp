using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizItUp.Data.Migrations
{
    public partial class TimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeToCompletePerQestion",
                table: "Quizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTimeToComplete",
                table: "Quizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Pictures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeToCompletePerQestion",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "TotalTimeToComplete",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Pictures");
        }
    }
}
