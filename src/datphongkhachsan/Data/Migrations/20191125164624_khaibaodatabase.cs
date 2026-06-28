using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace datphongkhachsan.Data.Migrations
{
    public partial class khaibaodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hoaDons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNguoiTao = table.Column<string>(nullable: true),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    TongTien = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "loaiDVs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loaiDVs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "loaiPhongs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loaiPhongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trangThais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trangThais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dichVus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    InStock = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    GetLoaiDVId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dichVus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dichVus_loaiDVs_GetLoaiDVId",
                        column: x => x.GetLoaiDVId,
                        principalTable: "loaiDVs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "phongs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    TrangThai = table.Column<int>(nullable: false),
                    LoaiPhongId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phongs_loaiPhongs_LoaiPhongId",
                        column: x => x.LoaiPhongId,
                        principalTable: "loaiPhongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "datPhongs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNguoiDat = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CMND = table.Column<string>(nullable: true),
                    SDT = table.Column<string>(nullable: true),
                    TienDatCoc = table.Column<decimal>(nullable: false),
                    ThoiGianNhanPhongDuKien = table.Column<DateTime>(nullable: false),
                    ThoiGianTraPhongDuKien = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    GetTrangThaiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_datPhongs_trangThais_GetTrangThaiId",
                        column: x => x.GetTrangThaiId,
                        principalTable: "trangThais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "chiTietHoaDons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonId = table.Column<int>(nullable: false),
                    DichVud = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    GiaTien = table.Column<decimal>(nullable: false),
                    GetDichVuId = table.Column<int>(nullable: true)
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
                name: "dichVuCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DichVuId = table.Column<int>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    DichVuCartId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dichVuCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dichVuCartItems_dichVus_DichVuId",
                        column: x => x.DichVuId,
                        principalTable: "dichVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "chiTietDatPhongs",
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
                name: "TrangThaiDatPhong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatPhongId = table.Column<int>(nullable: false),
                    TrangThaiId = table.Column<int>(nullable: false),
                    ThoiGian = table.Column<DateTime>(nullable: false)
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
                name: "IX_datPhongs_GetTrangThaiId",
                table: "datPhongs",
                column: "GetTrangThaiId");

            migrationBuilder.CreateIndex(
                name: "IX_dichVuCartItems_DichVuId",
                table: "dichVuCartItems",
                column: "DichVuId");

            migrationBuilder.CreateIndex(
                name: "IX_dichVus_GetLoaiDVId",
                table: "dichVus",
                column: "GetLoaiDVId");

            migrationBuilder.CreateIndex(
                name: "IX_phongs_LoaiPhongId",
                table: "phongs",
                column: "LoaiPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_TrangThaiDatPhong_DatPhongId",
                table: "TrangThaiDatPhong",
                column: "DatPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_TrangThaiDatPhong_TrangThaiId",
                table: "TrangThaiDatPhong",
                column: "TrangThaiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chiTietDatPhongs");

            migrationBuilder.DropTable(
                name: "chiTietHoaDons");

            migrationBuilder.DropTable(
                name: "dichVuCartItems");

            migrationBuilder.DropTable(
                name: "TrangThaiDatPhong");

            migrationBuilder.DropTable(
                name: "phongs");

            migrationBuilder.DropTable(
                name: "hoaDons");

            migrationBuilder.DropTable(
                name: "dichVus");

            migrationBuilder.DropTable(
                name: "datPhongs");

            migrationBuilder.DropTable(
                name: "loaiPhongs");

            migrationBuilder.DropTable(
                name: "loaiDVs");

            migrationBuilder.DropTable(
                name: "trangThais");
        }
    }
}
