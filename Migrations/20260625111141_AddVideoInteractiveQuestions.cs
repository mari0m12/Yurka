using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mr.magdy.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoInteractiveQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriggerSecond = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoQuestions_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    VideoQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoQuestionAnswers_VideoQuestions_VideoQuestionId",
                        column: x => x.VideoQuestionId,
                        principalTable: "VideoQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoQuestionResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    AwardedPoints = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoQuestionId = table.Column<int>(type: "int", nullable: false),
                    SelectedAnswerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoQuestionResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoQuestionResponses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VideoQuestionResponses_VideoQuestionAnswers_SelectedAnswerId",
                        column: x => x.SelectedAnswerId,
                        principalTable: "VideoQuestionAnswers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoQuestionResponses_VideoQuestions_VideoQuestionId",
                        column: x => x.VideoQuestionId,
                        principalTable: "VideoQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoQuestionAnswers_VideoQuestionId",
                table: "VideoQuestionAnswers",
                column: "VideoQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoQuestionResponses_SelectedAnswerId",
                table: "VideoQuestionResponses",
                column: "SelectedAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoQuestionResponses_UserId_VideoQuestionId",
                table: "VideoQuestionResponses",
                columns: new[] { "UserId", "VideoQuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoQuestionResponses_VideoQuestionId",
                table: "VideoQuestionResponses",
                column: "VideoQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoQuestions_LessonId_TriggerSecond",
                table: "VideoQuestions",
                columns: new[] { "LessonId", "TriggerSecond" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoQuestionResponses");

            migrationBuilder.DropTable(
                name: "VideoQuestionAnswers");

            migrationBuilder.DropTable(
                name: "VideoQuestions");
        }
    }
}
