using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseSessionId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CourseSessionEntityTeacherEntity",
                columns: table => new
                {
                    CourseSessionsId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSessionEntityTeacherEntity", x => new { x.CourseSessionsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_CourseSessionEntityTeacherEntity_CourseSession_CourseSessionsId",
                        column: x => x.CourseSessionsId,
                        principalTable: "CourseSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSessionEntityTeacherEntity_Teacher_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_CourseSessionId",
                table: "Enrollment",
                column: "CourseSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_ParticipantId",
                table: "Enrollment",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSession_CourseId",
                table: "CourseSession",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSessionEntityTeacherEntity_TeachersId",
                table: "CourseSessionEntityTeacherEntity",
                column: "TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSession_Course_CourseId",
                table: "CourseSession",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_CourseSession_CourseSessionId",
                table: "Enrollment",
                column: "CourseSessionId",
                principalTable: "CourseSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Participant_ParticipantId",
                table: "Enrollment",
                column: "ParticipantId",
                principalTable: "Participant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSession_Course_CourseId",
                table: "CourseSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_CourseSession_CourseSessionId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Participant_ParticipantId",
                table: "Enrollment");

            migrationBuilder.DropTable(
                name: "CourseSessionEntityTeacherEntity");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_CourseSessionId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_ParticipantId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_CourseSession_CourseId",
                table: "CourseSession");

            migrationBuilder.DropColumn(
                name: "CourseSessionId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "Enrollment");
        }
    }
}
