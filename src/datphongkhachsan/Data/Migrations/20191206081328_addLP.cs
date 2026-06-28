using Microsoft.EntityFrameworkCore.Migrations;

namespace datphongkhachsan.Data.Migrations
{
    public partial class addLP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HinhUrl",
                table: "LoaiPhongs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "LoaiPhongs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhUrl",
                table: "LoaiPhongs");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "LoaiPhongs");
        }
    }
}
