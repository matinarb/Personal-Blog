using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalBlog.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addisSpecial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpecial",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpecial",
                table: "Posts");
        }
    }
}
