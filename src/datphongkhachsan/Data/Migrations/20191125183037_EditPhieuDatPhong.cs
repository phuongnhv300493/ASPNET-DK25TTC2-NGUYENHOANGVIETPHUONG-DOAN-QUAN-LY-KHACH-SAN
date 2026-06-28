using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace datphongkhachsan.Data.Migrations
{
    public partial class EditPhieuDatPhong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chiTietDatPhongs");

            migrationBuilder.DropTable(
                name: "chiTietHoaDons");

            migrationBuilder.DropTable(
                name: "TrangThaiDatPhong");

            migrationBuilder.AddColumn<int>(
                name: "DatPhongId",
                table: "hoaDons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CaTietPhongDatPhongs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatPhongId = table.Column<int>(nullable: false),
                    PhongId = table.Column<int>(nullable: false),
                    TongSoNgay = table.Column<int>(nullable: false),
                    GiaTienMotNgay = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaTietPhongDatPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaTietPhongDatPhongs_datPhongs_DatPhongId",
                        column: x => x.DatPhongId,
                        principalTable: "datPhongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaTietPhongDatPhongs_phongs_PhongId",
                        column: x => x.PhongId,
                        principalTable: "phongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDichVuDatPhongs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatPhongId = table.Column<int>(nullable: false),
                    DichVuId = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    GiaTien = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDichVuDatPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietDichVuDatPhongs_datPhongs_DatPhongId",
                        column: x => x.DatPhongId,
                        principalTable: "datPhongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDichVuDatPhongs_dichVus_DichVuId",
                        column: x => x.DichVuId,
                        principalTable: "dichVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_DatPhongId",
                table: "hoaDons",
                column: "DatPhongId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaTietPhongDatPhongs_DatPhongId",
                table: "CaTietPhongDatPhongs",
                column: "DatPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_CaTietPhongDatPhongs_PhongId",
                table: "CaTietPhongDatPhongs",
                column: "PhongId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDichVuDatPhongs_DatPhongId",
                table: "ChiTietDichVuDatPhongs",
                column: "DatPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDichVuDatPhongs_DichVuId",
                table: "ChiTietDichVuDatPhongs",
                column: "DichVuId");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDons_datPhongs_DatPhongId",
                table: "hoaDons",
                column: "DatPhongId",
                principalTable: "datPhongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDons_datPhongs_DatPhongId",
                table: "hoaDons");

            migrationBuilder.DropTable(
                name: "CaTietPhongDatPhongs");

            migrationBuilder.DropTable(
                name: "ChiTietDichVuDatPhongs");

            migrationBuilder.DropIndex(
                name: "IX_hoaDons_DatPhongId",
                table: "hoaDons");

            migrationBuilder.DropColumn(
                name: "DatPhongId",
                table: "hoaDons");

            migrationBuilder.CreateTable(
                name: "chiTietDatPhongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatPhongId = table.Column<int>(type: "int", nullable: false),
                    GiaTienMotNgay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhongId = table.Column<int>(type: "int", nullable: false),
                    TongSoNgay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietDatPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chiTietDatPhongs_datPhongs_DatPhongId",
                        column: x => x.DatPhongId,
                        principalTable: "datPhongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chiTietDatPhongs_phongs_PhongId",
                        column: x => x.PhongId,
                        principalTable: "phongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chiTietHoaDons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DichVud = table.Column<int>(type: "int", nullable: false),
                    GetDichVuId = table.Column<int>(type: "int", nullable: true),
                    GiaTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietHoaDons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chiTietHoaDons_dichVus_GetDichVuId",
                        column: x => x.GetDichVuId,
                        principalTable: "dichVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chiTietHoaDons_hoaDons_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "hoaDons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiDatPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatPhongId = table.Column<int>(type: "int", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiDatPhong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrangThaiDatPhong_datPhongs_DatPhongId",
                        column: x => x.DatPhongId,
                        principalTable: "datPhongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrangThaiDatPhong_trangThais_TrangThaiId",
                        column: x => x.TrangThaiId,
                        principalTable: "trangThais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chiTietDatPhongs_DatPhongId",
                table: "chiTietDatPhongs",
                column: "DatPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietDatPhongs_PhongId",
                table: "chiTietDatPhongs",
                column: "PhongId");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHoaDons_GetDichVuId",
                table: "chiTietHoaDons",
                column: "GetDichVuId");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHoaDons_HoaDonId",
                table: "chiTietHoaDons",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_TrangThaiDatPhong_DatPhongId",
                table: "TrangThaiDatPhong",
                column: "DatPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_TrangThaiDatPhong_TrangThaiId",
                table: "TrangThaiDatPhong",
                column: "TrangThaiId");
        }
    }
}
