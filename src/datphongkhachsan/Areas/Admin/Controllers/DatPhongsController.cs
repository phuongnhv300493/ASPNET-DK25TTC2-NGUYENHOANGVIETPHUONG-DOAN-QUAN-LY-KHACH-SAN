using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using datphongkhachsan.Data;
using datphongkhachsan.Data.DataModel;
using datphongkhachsan.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace datphongkhachsan.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    [Area("Admin")]
    public class DatPhongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DatPhongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/DatPhongs
        public  IActionResult Index(DatPhongIndexVM reservation, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateNhanSortParm"] = sortOrder == "DateNhan" ? "datenhan_desc" : "DateNhan";
            ViewData["DateTraSortParm"] = sortOrder == "DateTra" ? "datetra_desc" : "DateTra";
            ViewData["TongTienSortParm"] = sortOrder == "TongTien" ? "TongTien_desc" : "TongTien";
            // index chua search
            //  var listdatphong = _datphongsv.GetAll();
            var listdatphong = from m in _context.DatPhongs select m;
            switch (sortOrder)
            {
                case "name_desc":
                    listdatphong = listdatphong.OrderByDescending(s => s.TenNguoiDat);
                    break;
                case "DateNhan":
                    listdatphong = listdatphong.OrderBy(s => s.ThoiGianNhanPhongDuKien);
                    break;
                case "datenhan_desc":
                    listdatphong = listdatphong.OrderByDescending(s => s.ThoiGianNhanPhongDuKien);
                    break;
                case "DateTra":
                    listdatphong = listdatphong.OrderBy(s => s.ThoiGianTraPhongDuKien);
                    break;
                case "datetra_desc":
                    listdatphong = listdatphong.OrderByDescending(s => s.ThoiGianTraPhongDuKien);
                    break;
                case "TongTien_desc":
                    listdatphong = listdatphong.OrderByDescending(s => s.TongTien);
                    break;
                case "TongTien":
                    listdatphong = listdatphong.OrderBy(s => s.TongTien);
                    break;
                default:
                    listdatphong = listdatphong.OrderBy(s => s.TenNguoiDat);
                    break;
            }

            // tìm kiếm theo cmnd
            if (!string.IsNullOrEmpty(reservation.CMND))
            {
                listdatphong = listdatphong.Where(s => s.Cmnd.Contains(reservation.CMND));
            }
            // tìm kiếm theo thời gian nhận
            if (reservation.ThoiGianNhan != default)
            {
                listdatphong = from m in listdatphong
                    where 
                          (m.ThoiGianNhanPhongDuKien.Date <= reservation.ThoiGianNhan.Date) &&
                          (reservation.ThoiGianNhan.Date <= m.ThoiGianTraPhongDuKien.Date)
                               select m;
            }


            // tìm kiếm theo ???

            // tạo đối tượng trả về cho view.
            var GetDataforReservation2 = new DatPhongIndexVM
            {
                LisDatPhongs = listdatphong.ToList()

            };
            return View(GetDataforReservation2);
        }

        // GET: Admin/DatPhongs/Details/5
        public DatPhongDetailsMD OnetDatPhongDetialsMd { get; set; }
        private int? idphieudatphong { get; set; }
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OnetDatPhongDetialsMd = new DatPhongDetailsMD
            {
                DatPhong = _context.DatPhongs.Find(id),
                ChiTietDatPhongs = _context.ChiTietDatPhongs.Where(a=>a.DatPhongId==id).Include(b=>b.DatPhong).Include(c=>c.Phong).Include(d=>d.TrangThai).OrderBy(f=>f.ThoiGian.Date).ToList(),
                ChiTietDichVuDatPhongs = _context.ChiTietDichVuDatPhongs.Where(i=>i.DatPhongId==id).Include(i=>i.DichVu).ToList(),

            };
            if (OnetDatPhongDetialsMd.DatPhong == null)
            {
                return NotFound();
            }
            //  _dbContext.trangThais.Where(i=>i.Name)

            //public DatPhong DatPhong { get; set; }

            return View(OnetDatPhongDetialsMd);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id,DatPhongDetailsMD abfromView)
        {// id này lưu phongid vì nhận từng phòng.
            if (ModelState.IsValid)
            {
                try
                {
                    var listChiTietDatPhongFromDb =  _context.ChiTietDatPhongs
                        .Where(i=>i.DatPhongId== abfromView.DatPhong.Id && i.PhongId==id
                                  && i.ThoiGian >=DateTime.Today.Date)
                        .ToList();
                    foreach (var i in listChiTietDatPhongFromDb)
                    {
                        switch (i.TrangThaiId)
                        {
                            case 1: i.TrangThaiId = 2;
                                break;
                            case 2: i.TrangThaiId = 3;
                                break;
                            case 3:
                                break;
                            default:
                                break;
                        }
                        _context.Update(i);
                        await _context.SaveChangesAsync();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatPhongExists(abfromView.DatPhong.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // làm thanh toán: nếu thanh toán 
                //if(ab.ChiTietDatPhongs.Any(i=>i.TrangThaiId==2))
                //    return RedirectToAction(nameof(Details));
                //else
                //{// chạy thẳng qua doccument luôn
                //   // return View("HoaDon",obj: hoadon);
                //}
                return RedirectToAction(nameof(Index));
            }
         
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/DatPhongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/DatPhongs/Create
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenNguoiDat,Address,Cmnd,Sdt,TienDatCoc,ThoiGianNhanPhongDuKien,ThoiGianTraPhongDuKien,TongTien,AccoutId")] DatPhong datPhong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datPhong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(datPhong);
        }

        // GET: Admin/DatPhongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datPhong = await _context.DatPhongs.FindAsync(id);
            if (datPhong == null)
            {
                return NotFound();
            }
            return View(datPhong);
        }

        // POST: Admin/DatPhongs/Edit/5
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenNguoiDat,Address,Cmnd,Sdt,TienDatCoc,ThoiGianNhanPhongDuKien,ThoiGianTraPhongDuKien,TongTien,AccoutId")] DatPhong datPhong)
        {
            if (id != datPhong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datPhong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatPhongExists(datPhong.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(datPhong);
        }

        // GET: Admin/DatPhongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datPhong = await _context.DatPhongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datPhong == null)
            {
                return NotFound();
            }

            return View(datPhong);
        }

        // POST: Admin/DatPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datPhong = await _context.DatPhongs.FindAsync(id);
            _context.DatPhongs.Remove(datPhong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatPhongExists(int id)
        {
            return _context.DatPhongs.Any(e => e.Id == id);
        }
    }
}
