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
    public class TrangThaisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrangThaisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TrangThais
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrangThais.ToListAsync());
        }

        // GET: Admin/TrangThais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TrangThais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trangThai == null)
            {
                return NotFound();
            }

            return View(trangThai);
        }

        // GET: Admin/TrangThais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TrangThais/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TrangThai trangThai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trangThai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trangThai);
        }

        // GET: Admin/TrangThais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TrangThais.FindAsync(id);
            if (trangThai == null)
            {
                return NotFound();
            }
            return View(trangThai);
        }

        // POST: Admin/TrangThais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TrangThai trangThai)
        {
            if (id != trangThai.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trangThai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrangThaiExists(trangThai.Id))
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
            return View(trangThai);
        }

        // GET: Admin/TrangThais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TrangThais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trangThai == null)
            {
                return NotFound();
            }

            return View(trangThai);
        }

        // POST: Admin/TrangThais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trangThai = await _context.TrangThais.FindAsync(id);
            _context.TrangThais.Remove(trangThai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrangThaiExists(int id)
        {
            return _context.TrangThais.Any(e => e.Id == id);
        }
    }
}
