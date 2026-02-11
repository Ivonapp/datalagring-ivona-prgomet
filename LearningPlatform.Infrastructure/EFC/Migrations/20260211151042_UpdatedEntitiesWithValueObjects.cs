using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntitiesWithValueObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Teacher",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Teacher",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Participant",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Participant",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Enrollment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseSession",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CourseSession",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseCode",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Course",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Course",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CourseSession");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CourseSession");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Course");
        }
    }
}
