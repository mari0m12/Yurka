using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mr.magdy.Migrations
{
    /// <inheritdoc />
    public partial class AddYurkaProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Governorate",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Markaz",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentPhone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Governorate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Markaz",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParentPhone",
                table: "AspNetUsers");
        }
    }
}
