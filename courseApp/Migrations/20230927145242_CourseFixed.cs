using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseApp.Migrations
{
    /// <inheritdoc />
    public partial class CourseFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Teacher",
                table: "Courses",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teacher",
                table: "Courses");
        }
    }
}
