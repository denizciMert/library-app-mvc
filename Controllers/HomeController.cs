using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace LibraryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult HomePage()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userProfile = await _appDbContext.kullanicilar.Where(w => w.KullaniciAdi == User.Identity.Name).FirstOrDefaultAsync();
            return View(userProfile);
        }

        [Authorize]
        public async Task<IActionResult> MyBooks()
        {
            var userBooks = await _appDbContext.kiralanan
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
                                             }).Where(u => u.KullaniciAdi == User.Identity.Name).ToListAsync();
            return View(userBooks);
        }

        public async Task<IActionResult> Rentable()
        {
            var rentableBooks = await _appDbContext.kitaplar.Where(u => u.Durum =="Mevcut").ToListAsync();
            return View(rentableBooks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }

        public IActionResult AboutMe()
        {
            return View();
        }
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult Connected()
        {
            try
            {
                _appDbContext.Database.CanConnectAsync();
                ViewBag.connectionMessage = "Aktif - Baðlantý Baþarýlý.";
            }
            catch (Exception ex)
            {
                ViewBag.connectionMessage = "Deaktif - Baðlantý Baþarýsýz: " + ex.Message;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
