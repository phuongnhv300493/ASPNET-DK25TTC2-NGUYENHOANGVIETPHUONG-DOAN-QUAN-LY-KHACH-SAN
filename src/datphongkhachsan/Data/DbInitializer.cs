using datphongkhachsan.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datphongkhachsan.Data.DataModel;
namespace datphongkhachsan.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async void Initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }

            if (!_roleManager.RoleExistsAsync(SD.AdminEndUser).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser)).GetAwaiter().GetResult();
            }

            if (!_roleManager.RoleExistsAsync(SD.SuperAdminEndUser).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminEndUser)).GetAwaiter().GetResult();
            }

            if (!_roleManager.RoleExistsAsync(SD.CustomerEndUser).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.CustomerEndUser)).GetAwaiter().GetResult();
            }

            if (_userManager.FindByEmailAsync("admin@gmail.com").GetAwaiter().GetResult() == null)
            {
                var createResult = _userManager.CreateAsync(new AccountSys
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Quản Trị Viên",
                    EmailConfirmed = true
                }, "admin123").GetAwaiter().GetResult();

                if (createResult.Succeeded)
                {
                    IdentityUser user = _userManager.FindByEmailAsync("admin@gmail.com").GetAwaiter().GetResult();
                    if (user != null)
                    {
                        _userManager.AddToRoleAsync(user, SD.SuperAdminEndUser).GetAwaiter().GetResult();
                    }
                }
            }

            if (_userManager.FindByEmailAsync("user1@gmail.com").GetAwaiter().GetResult() == null)
            {
                var createResult1 = _userManager.CreateAsync(new AccountSys
                {
                    UserName = "user1@gmail.com",
                    Email = "user1@gmail.com",
                    Name = "Khách Hàng 1",
                    EmailConfirmed = true
                }, "user123").GetAwaiter().GetResult();

                if (createResult1.Succeeded)
                {
                    IdentityUser user1 = _userManager.FindByEmailAsync("user1@gmail.com").GetAwaiter().GetResult();
                    if (user1 != null)
                    {
                        _userManager.AddToRoleAsync(user1, SD.CustomerEndUser).GetAwaiter().GetResult();
                    }
                }
            }
        }


    }

    public interface IDbInitializer
    {
        void Initialize();
    }
}
