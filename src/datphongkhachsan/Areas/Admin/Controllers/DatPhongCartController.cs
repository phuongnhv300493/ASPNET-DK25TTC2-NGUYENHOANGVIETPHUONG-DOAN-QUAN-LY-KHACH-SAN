using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Areas.Admin.Models;
using datphongkhachsan.Data;
using datphongkhachsan.Data.DataModel;
using datphongkhachsan.Extensions;
using datphongkhachsan.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;

namespace datphongkhachsan.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    [Area("Admin")]
    public class DatPhongCartController : Controller
    {
        private readonly ApplicationDbContext _Context;
        public DatPhongCartVM _DatPhongCart { get; set; }
        public DatPhongCartController(ApplicationDbContext dbContext)
        {
            _Context = dbContext;
            _DatPhongCart = new DatPhongCartVM()
            {
                LsPhongDatPhongs = new List<Phong>(),
                LsDichVuDatPhongs = new List<DichVu>(),
            };
        }
        public IActionResult Index()
        {
            List<int> lstsPhongCart = HttpContext.Session.Get<List<int>>("ssPhongCart");
            if (lstsPhongCart == null) return View(_DatPhongCart);
            if (lstsPhongCart.Count > 0)
            {
                foreach (int cartItem in lstsPhongCart)
                {
                    Phong listPhongDat = _Context.Phongs.Find(cartItem);
                    _DatPhongCart.LsPhongDatPhongs.Add(listPhongDat);
                }
            }
            List<int> lstsDichVuCart = HttpContext.Session.Get<List<int>>("ssDichVuCart");
            if (lstsDichVuCart == null) return View(_DatPhongCart);
            if (lstsDichVuCart.Count > 0)
            {
                foreach (int cartItem in lstsDichVuCart)
                {
                    DichVu oneDichVu = _Context.DichVus.Find(cartItem);
                    _DatPhongCart.LsDichVuDatPhongs.Add(oneDichVu);
                }
            }

            return View(_DatPhongCart);
        }
        //tìm phòng trống
        public async Task<IActionResult> GetListPhong()
        {
            var lsPhongs = _Context.Phongs.Include(p => p.LoaiPhong);
            return RedirectToAction("Index", "GetListDatPhongs", new { area = "Admin" });

        }
        // dùng action xóa phòng ra khỏi giỏ phòng cần đặt
        public IActionResult RemovePhongFromCart(int id)
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssPhongCart");

            if (lstCartItems.Count > 0)
            {
                if (lstCartItems.Contains(id))
                {
                    lstCartItems.Remove(id);
                }
            }

            HttpContext.Session.Set("ssPhongCart", lstCartItems);
            return RedirectToAction("Index", "DatPhongCart", new { area = "Admin" });
        }
        // chọn dịch vụ hiện có của khách sạn
        public async Task<IActionResult> GetListDichVu()
        {
            //  var lsDichVus = _dichVusv.GetAll().Include(p => p.GetLoaiDV);
            var lsPhongs = _Context.Phongs.Include(p => p.LoaiPhong);
            return RedirectToAction("Index", "GetListDichVu", new { area = "Admin" });
        }
        // xóa dịch vụ ra khỏi dịch cart
        public IActionResult RemoveDVFromCart(int id)
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssDichVuCart");

            if (lstCartItems.Count > 0)
            {
                if (lstCartItems.Contains(id))
                {
                    lstCartItems.Remove(id);
                }
            }
            HttpContext.Session.Set("ssDichVuCart", lstCartItems);
            return RedirectToAction("Index", "DatPhongCart", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // xác nhận đặt phòng
        public async Task<IActionResult> XacNhanDatPhong(DatPhongCartVM ab)
        {
            if (ModelState.IsValid)
            {
                // lấy đc id vừa add vào db : đã lấy đc
                _Context.Add(ab.newDatPhong);
                await _Context.SaveChangesAsync();
                int newDatPhongId = ab.newDatPhong.Id;

                #region Phong

                // list Id Phong
                List<int> lstsPhongCart = HttpContext.Session.Get<List<int>>("ssPhongCart");
                // xác định số dòng cần thêm cho mỗi phòng
                var soNgay = (int)(ab.newDatPhong.ThoiGianTraPhongDuKien.Date - ab.newDatPhong.ThoiGianNhanPhongDuKien.Date).TotalDays;
                var thoiGianinChiTietDatPhong = ab.newDatPhong.ThoiGianTraPhongDuKien.Date;
                for (int j = 0; j < soNgay; j++)
                {
                    foreach (var i in lstsPhongCart)
                    {
                        var objChiTietDatPhong = new ChiTietDatPhong
                        {
                            DatPhongId = newDatPhongId,
                            PhongId = i,
                            TrangThaiId = 1,
                            ThoiGian = thoiGianinChiTietDatPhong
                        };
                        // nếu không tồn tại cùng ngày cùng tháng thì ghi vô
                        var IsTonTai = from m in _Context.ChiTietDatPhongs
                            where m == objChiTietDatPhong
                            select m;
                        if(IsTonTai.Count() != 0)
                            break;
                        _Context.ChiTietDatPhongs.Add(objChiTietDatPhong); 
                        await _Context.SaveChangesAsync();
                    }
                    thoiGianinChiTietDatPhong.AddDays(1);
                }

                #endregion

                #region dichvu

                List<int> lstsDichVuCart = HttpContext.Session.Get<List<int>>("ssDichVuCart");

                foreach (var i in lstsDichVuCart)
                {
                    var objChiTietDichVuDatPhong = new ChiTietDichVuDatPhong()
                    {
                        DatPhongId = newDatPhongId,
                        DichVuId = i,
                        SoLuong = 1 // đang test mặc định số lượng dịch vụ là 1;
                    };
                    _Context.ChiTietDichVuDatPhongs.Add(objChiTietDichVuDatPhong);
                    await _Context.SaveChangesAsync();
                }
                #endregion
            }
            return RedirectToAction("Index", "DatPhongs", new { area = "Admin" });
        }
    }

}