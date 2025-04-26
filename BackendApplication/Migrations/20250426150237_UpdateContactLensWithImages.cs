using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactLensWithImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "ContactLenses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "ContactLenses");
        }
    }
}
