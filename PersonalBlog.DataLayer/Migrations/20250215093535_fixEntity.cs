using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalBlog.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class fixEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FulName",
                table: "Users",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "FulName");
        }
    }
}
