using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizItUp.Data.Migrations
{
    public partial class ChangeTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_Tags_TagId",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_TagId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Quizes");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Badges",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "QuizTag",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    TagId = table.Column<int>(nullable: false),
                    QuizId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizTag_Quizes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizTag_IsDeleted",
                table: "QuizTag",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTag_QuizId",
                table: "QuizTag",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTag_TagId",
                table: "QuizTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizTag");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Badges");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Quizes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_TagId",
                table: "Quizes",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_Tags_TagId",
                table: "Quizes",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
