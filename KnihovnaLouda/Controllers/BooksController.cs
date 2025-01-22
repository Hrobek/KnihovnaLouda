using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using KnihovnaLouda.Data;
using KnihovnaLouda.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using KnihovnaLouda.Interfaces.IManager;
using KnihovnaLouda.Manager;

namespace KnihovnaLouda.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IPhotoManager _photoManager;

        public BooksController(IBookManager bookManager, IPhotoManager photoManager)
        {
            _bookManager = bookManager;
            _photoManager = photoManager;
        }

        public async Task<IActionResult> Index()
        {
            var booksList = await _bookManager.GetAllBooksAsync();
            return View(booksList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookManager.GetBookByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = new SelectList(await _bookManager.GetAllAuthorsAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile photo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = new SelectList(await _bookManager.GetAllAuthorsAsync(), "Id", "Name");
                return View(book);
            }

            if (photo != null)
            {
                string photoPath = await _photoManager.SavePhotoAsync(photo, "BooksPhotos");
                book.PhotoPath = photoPath;
            }

            bool success = await _bookManager.CreateBookAsync(book);
            if (!success)
            {
                ViewBag.Authors = new SelectList(await _bookManager.GetAllAuthorsAsync(), "Id", "Name");
                return View(book);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookManager.GetBookByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.Authors = new SelectList(await _bookManager.GetAllAuthorsAsync(), "Id", "Name", book.AuthorId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, IFormFile photo)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = new SelectList(await _bookManager.GetAllAuthorsAsync(), "Id", "Name", book.AuthorId);
                return View(book);
            }

            var existingBook = await _bookManager.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            // Pokud je nový obrázek, smažeme starý a přidáme nový
            if (photo != null)
            {
                _photoManager.DeletePhoto(existingBook.PhotoPath);

                string photoPath = await _photoManager.SavePhotoAsync(photo, "BooksPhotos");
                book.PhotoPath = photoPath;
            }
            else
            {
                // Pokud není nový obrázek, zachováme ten původní
                book.PhotoPath = existingBook.PhotoPath;
            }

            // Aktualizace existující knihy
            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.Published = book.Published;
            existingBook.AuthorId = book.AuthorId;
            existingBook.PhotoPath = book.PhotoPath; // Použijeme novou hodnotu fotky

            // Aktualizace knihy v databázi
            try
            {
                bool success = await _bookManager.UpdateBookAsync(existingBook);
                if (!success)
                {
                    ViewBag.Authors = await _bookManager.GetAllAuthorsAsync();

                    return View(book);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bookManager.ExistsAsync(book.Id))
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


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookManager.GetBookByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookManager.GetBookByIdAsync(id);
            if (book != null)
            {
                _photoManager.DeletePhoto(book.PhotoPath);
            }

            bool success = await _bookManager.DeleteBookAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
