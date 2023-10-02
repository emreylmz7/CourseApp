using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teacher",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Courses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Mail = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    StartingDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registries_CourseId",
                table: "Registries",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Registries_StudentId",
                table: "Registries",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registries_Courses_CourseId",
                table: "Registries",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registries_Students_StudentId",
                table: "Registries",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Registries_Courses_CourseId",
                table: "Registries");

            migrationBuilder.DropForeignKey(
                name: "FK_Registries_Students_StudentId",
                table: "Registries");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Registries_CourseId",
                table: "Registries");

            migrationBuilder.DropIndex(
                name: "IX_Registries_StudentId",
                table: "Registries");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Teacher",
                table: "Courses",
                type: "TEXT",
                nullable: true);
        }
    }
}
