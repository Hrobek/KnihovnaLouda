using KnihovnaLouda.Interfaces.IManager;
using KnihovnaLouda.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class ReservationsController : Controller
{
    private readonly IReservationManager _reservationManager;

    public ReservationsController(IReservationManager reservationManager)
    {
        _reservationManager = reservationManager;
    }

    public async Task<IActionResult> Details()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var reservations = await _reservationManager.GetUserReservationsAsync(userId);
        return View(reservations);
    }

    public async Task<IActionResult> Create()
    {
        var books = await _reservationManager.GetAllBooksAsync();
        var users = await _reservationManager.GetAllUsersAsync();

        var reservation = new Reservation
        {
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(14)
        };

        ViewBag.Users = users;
        ViewBag.Books = new SelectList(books, "Id", "Title");
        return View(reservation);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reservation reservation)
    {
        if (!ModelState.IsValid)
        {
            return View(reservation);
        }

        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        bool success = await _reservationManager.CreateReservationAsync(reservation, userId);

        if (!success)
        {
            ModelState.AddModelError("", "Kniha již neexistuje.");
            ViewBag.Books = new SelectList(await _reservationManager.GetAllBooksAsync(), "Id", "Title");
            ViewBag.Users = await _reservationManager.GetAllUsersAsync();
            return View(reservation);
        }

        return RedirectToAction(nameof(Index), "Books");
    }
}
