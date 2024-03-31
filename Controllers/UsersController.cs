using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;
using BCrypt.Net;
using BCrypt;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApp.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> All()
        {
            return View(await _context.kullanicilar.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersModel = await _context.kullanicilar
                .FirstOrDefaultAsync(m => m.KullaniciID == id);
            if (usersModel == null)
            {
                return NotFound();
            }

            return View(usersModel);
        }

        public IActionResult Create()
        {
            List<string> roles = ["","Yönetici", "Kullanıcı", "Misafir", "Test"];
            ViewBag.roles = new SelectList(roles);

            return View();
        }

        public bool isUsernameTaken(string Adi)
        {
            return _context.kullanicilar.Any(k => k.KullaniciAdi == Adi);
        }

        public bool isEmailTaken(string Eposta)
        {
            return _context.kullanicilar.Any(k => k.Eposta == Eposta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KullaniciID,Adi,Soyadi,Eposta,KullaniciAdi,Sifre,Rol")] UsersModel usersModel)
        {
            if (ModelState.IsValid)
            {
                if (isUsernameTaken(usersModel.KullaniciAdi) && isEmailTaken(usersModel.Eposta))
                {
                    ModelState.AddModelError(string.Empty, "Girilen kullanıcı adı ve eposta kullanılıyor!");
                }
                else if(isUsernameTaken(usersModel.KullaniciAdi))
                {
                    ModelState.AddModelError(string.Empty, "Girilen kullanıcı adı kullanılıyor!");
                }
                else if (isEmailTaken(usersModel.Eposta))
                {
                    ModelState.AddModelError(string.Empty, "Girilen eposta kullanılıyor!");
                }
                else
                {
                    var salt = BCrypt.Net.BCrypt.GenerateSalt();
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(usersModel.Sifre, salt);
                    usersModel.Sifre = hashedPassword;
                    _context.Add(usersModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(All));
                }
                    
            }
            return View(usersModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<string> roles = ["", "Yönetici", "Kullanıcı", "Misafir", "Test"];
            ViewBag.roles = new SelectList(roles);
            var usersModel = await _context.kullanicilar.FindAsync(id);
            if (usersModel == null)
            {
                return NotFound();
            }
            return View(usersModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KullaniciID,Adi,Soyadi,Eposta,KullaniciAdi,Sifre,Rol")] UsersModel usersModel)
        {
            if (id != usersModel.KullaniciID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var salt = BCrypt.Net.BCrypt.GenerateSalt();
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(usersModel.Sifre, salt);
                    usersModel.Sifre = hashedPassword;
                    _context.Update(usersModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersModelExists(usersModel.KullaniciID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(All));
            }
            return View(usersModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersModel = await _context.kullanicilar
                .FirstOrDefaultAsync(m => m.KullaniciID == id);
            if (usersModel == null)
            {
                return NotFound();
            }

            return View(usersModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersModel = await _context.kullanicilar.FindAsync(id);
            try
            {
                if (usersModel != null)
                {
                    _context.kullanicilar.Remove(usersModel);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                ViewBag.UserDeleteFailed= "Bu kullanıcı kaydı silinemez.";
                return View(usersModel);
            }
        }

        private bool UsersModelExists(int id)
        {
            return _context.kullanicilar.Any(e => e.KullaniciID == id);
        }
    }
}
