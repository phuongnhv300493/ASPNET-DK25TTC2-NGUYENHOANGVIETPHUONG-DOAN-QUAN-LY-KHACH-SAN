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
using datphongkhachsan.Extensions;
using datphongkhachsan.Utility;
using Microsoft.AspNetCore.Authorization;

namespace datphongkhachsan.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    [Area("Admin")]
    public class GetListDatPhongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GetListDatPhongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/GetListDatPhongs
        public IActionResult Index(GetListDatPhongIndexVM getListDatPhongIndexVm, string sortOrder)
        { // getlist phong trong theo ngay den ngay di.
          // có 2 ô ngày đến ngày đi nhập vô.
          // tìm phòng ngày đó.
          // chọn phòng
          //var applicationDbContext = _context.Phongs.Include(p => p.ChuongTrinh).Include(p => p.LoaiPhong);
            var lsPhongsDb = from m in _context.Phongs select m;
            bool isFillterDb = false;
            if (getListDatPhongIndexVm.NgayNhanPhongDuKien != default)
            {
               // tìm tất cả các phòng trong chi tiết đặt phòng theo ngày đến, ngày đi
               var ListId = from m in _context.ChiTietDatPhongs
                   where m.ThoiGian.Date >= getListDatPhongIndexVm.NgayNhanPhongDuKien.Date &&
                         m.ThoiGian.Date <= getListDatPhongIndexVm.NgayTraPhongDuKien.Date
                   select m.PhongId;

               lsPhongsDb = _context.Phongs.Where(r => !ListId.Contains(r.Id));
               isFillterDb = true;
            }
            var getListDatPhongIndexVm2 = new GetListDatPhongIndexVM
            {
                LsPhongDatPhongs = lsPhongsDb.Include(c=>c.LoaiPhong).Include(c=>c.ChuongTrinh).ToList(),
                isFillter= isFillterDb
            };
            return View(getListDatPhongIndexVm2);
        }

        public IActionResult AddPhongToCart(int id)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssPhongCart");
            if (lstShoppingCart == null)
            {
                lstShoppingCart = new List<int>();
            }

            lstShoppingCart.Add(id);
            HttpContext.Session.Set("ssPhongCart", lstShoppingCart);

            return RedirectToAction("Index", "DatPhongCart", new {area = "Admin"});

        }

       
    }
}
