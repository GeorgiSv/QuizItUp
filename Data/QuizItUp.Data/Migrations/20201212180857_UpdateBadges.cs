using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizItUp.Data.Migrations
{
    public partial class UpdateBadges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badges_Quizes_Id",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "BadgeId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Badges");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BadgeId",
                table: "Quizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuizId",
                table: "Badges",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Quizes_Id",
                table: "Badges",
                column: "Id",
                principalTable: "Quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
