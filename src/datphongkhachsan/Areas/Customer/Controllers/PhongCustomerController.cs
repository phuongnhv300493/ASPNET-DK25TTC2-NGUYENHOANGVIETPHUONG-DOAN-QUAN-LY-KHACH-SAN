using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Data;
using datphongkhachsan.Data.DataModel.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace datphongkhachsan.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PhongCustomerController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _iwebhostenvironment;

        [BindProperty]
        public PhongViewModel PhongVM { get; set; }
        public PhongCustomerController(ApplicationDbContext db, IWebHostEnvironment iwebHostEnvironment)
        {
            _db = db;
            _iwebhostenvironment = iwebHostEnvironment;
            PhongVM = new PhongViewModel()
            {
                loaiphongs = _db.LoaiPhongs.ToList(),
                chuongtrinhs = _db.ChuongTrinhs.ToList(),
                phongs = new Data.DataModel.Phong()
            };
        }
        public async Task<IActionResult> IndexRoom()
        {
            var phongs = _db.Phongs.Include(m => m.LoaiPhong).Include(m => m.ChuongTrinh);
            return View(await phongs.ToListAsync());
        }
        public async Task<IActionResult> DetailsRoom(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PhongVM.phongs = await _db.Phongs.Include(m => m.LoaiPhong).Include(m => m.ChuongTrinh).SingleOrDefaultAsync(m => m.Id == id);

            if (PhongVM.phongs == null)
            {
                return NotFound();
            }

            return View(PhongVM);
        }

    }
}