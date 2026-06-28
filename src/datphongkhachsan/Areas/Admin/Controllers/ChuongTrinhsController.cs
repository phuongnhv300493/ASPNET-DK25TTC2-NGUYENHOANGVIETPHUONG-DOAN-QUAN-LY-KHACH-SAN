using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using datphongkhachsan.Data;
using datphongkhachsan.Data.DataModel;
using datphongkhachsan.Utility;
using Microsoft.AspNetCore.Authorization;

namespace datphongkhachsan.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class ChuongTrinhsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChuongTrinhsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ChuongTrinhs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChuongTrinhs.ToListAsync());
        }

        // GET: Admin/ChuongTrinhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuongTrinh = await _context.ChuongTrinhs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chuongTrinh == null)
            {
                return NotFound();
            }

            return View(chuongTrinh);
        }

        // GET: Admin/ChuongTrinhs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChuongTrinhs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenChuongTrinh,TiLeThayDoiGia,IsTang")] ChuongTrinh chuongTrinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chuongTrinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chuongTrinh);
        }

        // GET: Admin/ChuongTrinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuongTrinh = await _context.ChuongTrinhs.FindAsync(id);
            if (chuongTrinh == null)
            {
                return NotFound();
            }
            return View(chuongTrinh);
        }

        // POST: Admin/ChuongTrinhs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenChuongTrinh,TiLeThayDoiGia,IsTang")] ChuongTrinh chuongTrinh)
        {
            if (id != chuongTrinh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuongTrinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuongTrinhExists(chuongTrinh.Id))
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
            return View(chuongTrinh);
        }

        // GET: Admin/ChuongTrinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuongTrinh = await _context.ChuongTrinhs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chuongTrinh == null)
            {
                return NotFound();
            }

            return View(chuongTrinh);
        }

        // POST: Admin/ChuongTrinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chuongTrinh = await _context.ChuongTrinhs.FindAsync(id);
            _context.ChuongTrinhs.Remove(chuongTrinh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuongTrinhExists(int id)
        {
            return _context.ChuongTrinhs.Any(e => e.Id == id);
        }
    }
}
