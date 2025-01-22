using KnihovnaLouda.Interfaces.IManager;
using KnihovnaLouda.Manager;
using KnihovnaLouda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AuthorsController : Controller
{
    private readonly IAuthorManager _authorManager;
    private readonly IPhotoManager _photoManager;
    public AuthorsController(IAuthorManager authorManager, IPhotoManager photoManager)
    {
        _authorManager = authorManager;
        _photoManager = photoManager;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _authorManager.GetAllAuthorsAsync();
        return View(authors);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = await _authorManager.GetAuthorByIdAsync(id.Value);
        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Surname")] Author author, IFormFile photo)
    {
        if (!ModelState.IsValid)
        {
            return View(author);
        }

        if (photo != null)
        {
            string photoPath = await _photoManager.SavePhotoAsync(photo, "AuthorPhotos");
            author.PhotoPath = photoPath;
        }

        bool success = await _authorManager.CreateAuthorAsync(author);
        if (!success)
        {
            return View(author);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = await _authorManager.GetAuthorByIdAsync(id.Value);
        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Author author)
    {
        if (id != author.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(author);
        }

        var existingAuthor = await _authorManager.GetAuthorByIdAsync(id);
        if (existingAuthor == null)
        {
            return NotFound();
        }

        // Aktualizace existujícího autora
        existingAuthor.Name = author.Name;

        // Uložení změn do databáze
        try
        {
            bool success = await _authorManager.UpdateAuthorAsync(existingAuthor);
            if (!success)
            {
                return View(author);
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _authorManager.ExistsAsync(author.Id))
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

        var author = await _authorManager.GetAuthorByIdAsync(id.Value);
        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        bool success = await _authorManager.DeleteAuthorAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}
