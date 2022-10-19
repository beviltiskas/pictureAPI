using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pictureAPI.Migrations
{
    public partial class fixedImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Pictures",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Pictures",
                newName: "ImagePath");
        }
    }
}
