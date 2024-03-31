using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApp.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BooksModels
        public async Task<IActionResult> All()
        {
            var books = await _context.kitaplar.ToListAsync();
            return View(books);
        }

        // GET: BooksModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksModel = await _context.kitaplar
                .FirstOrDefaultAsync(m => m.KitapID == id);
            if (booksModel == null)
            {
                return NotFound();
            }

            return View(booksModel);
        }

        // GET: BooksModels/Create
        public IActionResult Create()
        {
            List<string> genres = ["", "Roman", "Biyografi", "Çocuk Kitabı", "Şiir", "Felsefe", "Ansiklopedi"];
            List<string> shelfs = ["", "A1", "B2", "C3", "D4", "F6", "G7", "H8", "I9", "J10", "K11", "L12", "M13", "N14", "O15", "P16", "Q17", "R18", "S19", "T20", "U21", "V22", "W23", "X24", "Y25", "Z26"];
            List<string> states = ["", "Mevcut", "Kiralanmış", "Sipariş Bekleniyor", "Yasaklı", "Kayıp"];
            ViewBag.genres = new SelectList(genres);
            ViewBag.shelfs = new SelectList(shelfs);
            ViewBag.states = new SelectList(states);
            return View();
        }

        // POST: BooksModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitapID,Baslik,Yazar,YayinYili,Tur,ISBN,RafNumarasi,Durum")] BooksModel booksModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booksModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(All));
            }
            return View(booksModel);
        }

        // GET: BooksModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<string> genres = ["", "Roman", "Biyografi", "Çocuk Kitabı", "Şiir", "Felsefe", "Ansiklopedi"];
            List<string> shelfs = ["", "A1", "B2", "C3", "D4", "F6", "G7", "H8", "I9", "J10", "K11", "L12", "M13", "N14", "O15", "P16", "Q17", "R18", "S19", "T20", "U21", "V22", "W23", "X24", "Y25", "Z26"];
            List<string> states = ["", "Mevcut", "Kiralanmış", "Sipariş Bekleniyor", "Yasaklı", "Kayıp"];
            ViewBag.genres = new SelectList(genres);
            ViewBag.shelfs = new SelectList(shelfs);
            ViewBag.states = new SelectList(states);
            var booksModel = await _context.kitaplar.FindAsync(id);
            if (booksModel == null)
            {
                return NotFound();
            }
            return View(booksModel);
        }

        // POST: BooksModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KitapID,Baslik,Yazar,YayinYili,Tur,ISBN,RafNumarasi,Durum")] BooksModel booksModel)
        {
            if (id != booksModel.KitapID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booksModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksModelExists(booksModel.KitapID))
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
            return View(booksModel);
        }

        // GET: BooksModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksModel = await _context.kitaplar
                .FirstOrDefaultAsync(m => m.KitapID == id);
            if (booksModel == null)
            {
                return NotFound();
            }

            return View(booksModel);
        }

        // POST: BooksModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booksModel = await _context.kitaplar.FindAsync(id);
            if (booksModel != null)
            {
                _context.kitaplar.Remove(booksModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }

        private bool BooksModelExists(int id)
        {
            return _context.kitaplar.Any(e => e.KitapID == id);
        }
    }
}
