using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OcharHasfarim.Data;
using OcharHasfarim.Models;

namespace OcharHasfarim.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Book.Include(b => b.Shelf);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Id");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Height,Width,Genre,ShelfId")] Book book)
        {
            if (ModelState.IsValid)
            {
               
                var shelfHeight = await _context.Shelf
                    .Where(s => s.Id == book.ShelfId)
                    .Select(s => s.Height)
                    .FirstOrDefaultAsync();

                if (book.Height > shelfHeight)
                {
                    TempData["Message"] = "הספר יותר גבוה מהמדף שבחרת אנא בחר מדף אחר גבוה יותר.";
                    TempData["MessageType"] = "error";

                    ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Id", book.ShelfId);
                    return View(book);
                }

                
                var totalWidth = await _context.Book
                    .Where(b => b.ShelfId == book.ShelfId)
                    .SumAsync(b => b.Width);

                
                var shelfWidth = await _context.Shelf
                    .Where(s => s.Id == book.ShelfId)
                    .Select(s => s.Width)
                    .FirstOrDefaultAsync();

              
                if (totalWidth + book.Width > shelfWidth)
                {
                    TempData["Message"] = "אין מספיק מקום במדף עבור הספר החדש. אנא בחר מדף אחר.";
                    TempData["MessageType"] = "error";

                    ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Id", book.ShelfId);
                    return View(book);
                }

                
                if (book.Height + 10 <= shelfHeight)
                {
                    TempData["Message"] = "שים לב הספר נמוך משמעותית מהמדף הנבחר כך שיש לך עודף מקום.";
                    TempData["MessageType"] = "warning";
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Id", book.ShelfId);
            return View(book);
        }

        public async Task<IActionResult> CalculateTotalWidth(int shelfId)
        {
            var totalWidth = await _context.Book
                .Where(b => b.ShelfId == shelfId)
                .SumAsync(b => b.Width);

            ViewData["TotalWidth"] = totalWidth;
            return View();
        }
        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Id", book.ShelfId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Height,Width,Genre,ShelfId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Id", book.ShelfId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
