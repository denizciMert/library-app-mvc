using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApp.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class RentsController : Controller
    {
        private readonly AppDbContext _context;

        public RentsController(AppDbContext context)
        {
            _context = context;
        }

        /*public string WhatIsUsername(int UserID)
        {
            return _context.kullanicilar.Where(w => w.KullaniciID == UserID).Select(u=> u.KullaniciAdi).FirstOrDefault();
        }

        public string WhatIsBookname(int BookID)
        {
            return _context.kitaplar.Where(w => w.KitapID == BookID).Select(u => u.Baslik).FirstOrDefault();
        }

        public async Task<IActionResult> All()
        {
            var rentDetails = await _context.kiralanan.Include(r => r.KullaniciID).Include(r => r.KitapID)
                             .Select(r => new RentsViewModel
                             {
                                 KiralananID = r.KiralananID,
                                 KullaniciID = r.KullaniciID,
                                 KullaniciAdi = WhatIsUsername(r.KullaniciID),
                                 KitapID = r.KitapID,
                                 KitapAdi = WhatIsBookname(r.KitapID),
                                 AlisTarihi = r.AlisTarihi,
                                 IadeTarihi = r.IadeTarihi,
                                 GeriGetirmeTarihi = r.GeriGetirmeTarihi
                             }).ToListAsync();
            return View(rentDetails);
        }*/

        public async Task<IActionResult> All()
        {
            var rentDetails = await _context.kiralanan
                                 .Include(r => r.User)
                                 .Include(r => r.Book)
                                 .Select(r => new RentsViewModel
                                 {
                                     KiralananID = r.KiralananID,
                                     KullaniciID = r.KullaniciID,
                                     KullaniciAdi = r.User.KullaniciAdi,
                                     KitapID = r.KitapID,
                                     KitapAdi = r.Book.Baslik,
                                     AlisTarihi = r.AlisTarihi,
                                     IadeTarihi = r.IadeTarihi,
                                     GeriGetirmeTarihi = r.GeriGetirmeTarihi
                                 }).ToListAsync();

            return View(rentDetails);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentsModel = await _context.kiralanan
                .FirstOrDefaultAsync(m => m.KiralananID == id);
            if (rentsModel == null)
            {
                return NotFound();
            }

            return View(rentsModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        public bool IsBookIdValid(int BookID)
        {
            return _context.kitaplar.Any(k => k.KitapID == BookID);
        }

        public bool IsUserIdValid(int UserID)
        {
            return _context.kullanicilar.Any(k => k.KullaniciID== UserID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KiralananID,KitapID,KullaniciID,AlisTarihi,IadeTarihi,GeriGetirmeTarihi")] RentsModel rentsModel)
        {
            if (ModelState.IsValid)
            {
                if (IsUserIdValid(rentsModel.KullaniciID))
                {
                    if (IsBookIdValid(rentsModel.KitapID))
                    {
                        _context.Add(rentsModel);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(All));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Kitap No. hatalı.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kullanıcı No. hatalı.");
                }
            }
            return View(rentsModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentsModel = await _context.kiralanan.FindAsync(id);
            if (rentsModel == null)
            {
                return NotFound();
            }
            return View(rentsModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KiralananID,KitapID,KullaniciID,AlisTarihi,IadeTarihi,GeriGetirmeTarihi")] RentsModel rentsModel)
        {
            if (id != rentsModel.KiralananID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (IsUserIdValid(rentsModel.KullaniciID))
                {
                    if (IsBookIdValid(rentsModel.KitapID))
                    {
                        try
                        {
                            _context.Update(rentsModel);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!RentsModelExists(rentsModel.KiralananID))
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
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Kitap No. hatalı.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kullanıcı No. hatalı.");
                }
            }
            return View(rentsModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentsModel = await _context.kiralanan
                .FirstOrDefaultAsync(m => m.KiralananID == id);
            if (rentsModel == null)
            {
                return NotFound();
            }

            return View(rentsModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentsModel = await _context.kiralanan.FindAsync(id);
            if (rentsModel != null)
            {
                _context.kiralanan.Remove(rentsModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }

        private bool RentsModelExists(int id)
        {
            return _context.kiralanan.Any(e => e.KiralananID == id);
        }
    }
}