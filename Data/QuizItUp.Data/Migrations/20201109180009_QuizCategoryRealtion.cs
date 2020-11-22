using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizItUp.Data.Migrations
{
    public partial class QuizCategoryRealtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Quizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_CategoryId",
                table: "Quizes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_Categories_CategoryId",
                table: "Quizes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_Categories_CategoryId",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_CategoryId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Quizes");
        }
    }
}
