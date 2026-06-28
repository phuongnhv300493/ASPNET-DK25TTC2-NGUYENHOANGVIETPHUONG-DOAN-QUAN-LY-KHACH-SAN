using System;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Data;
using datphongkhachsan.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using datphongkhachsan.Data.DataModel;
namespace datphongkhachsan.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Accounts.ToList());
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var userFromDb = await _context.Accounts.FindAsync(id);
            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }


        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, AccountSys applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                AccountSys userFromDb = _context.Accounts.Where(u => u.Id == id).FirstOrDefault();
                if (userFromDb != null)
                {
                    userFromDb.Name = applicationUser.Name;
                    userFromDb.PhoneNumber = applicationUser.PhoneNumber;
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(applicationUser);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var userFromDb = await _context.Accounts.FindAsync(id);
            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }


        //Post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(string id)
        {
            AccountSys userFromDb = _context.Accounts.Where(u => u.Id == id).FirstOrDefault();
            if (userFromDb != null) userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}