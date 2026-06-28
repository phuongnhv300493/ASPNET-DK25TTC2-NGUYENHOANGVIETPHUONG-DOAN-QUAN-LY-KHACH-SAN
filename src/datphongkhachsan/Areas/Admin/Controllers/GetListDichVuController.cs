using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class GetListDichVuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GetListDichVuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/GetListDichVu
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DichVus.Include(d => d.LoaiDv);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult AddDichVuToCart(int id)
        {
            List<int> lsDichvuCart = HttpContext.Session.Get<List<int>>("ssDichVuCart");
            if (lsDichvuCart == null)
            {
                lsDichvuCart = new List<int>();
            }

            lsDichvuCart.Add(id);
            HttpContext.Session.Set("ssDichVuCart", lsDichvuCart);
            return RedirectToAction("Index", "DatPhongCart", new { area = "Admin" });
        }
    }
}
