using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pictureAPI.Migrations
{
    public partial class initforallcontrollers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pictures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Albums",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_UserId",
                table: "Pictures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UserId",
                table: "Albums",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_UserId",
                table: "Albums",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_AspNetUsers_UserId",
                table: "Pictures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_UserId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_AspNetUsers_UserId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_UserId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Albums_UserId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Albums");
        }
    }
}
