using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyUrler_DotNetVersion.Migrations
{
    /// <inheritdoc />
    public partial class UserModelAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LinkType",
                table: "Links",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkType",
                table: "Links");
        }
    }
}
