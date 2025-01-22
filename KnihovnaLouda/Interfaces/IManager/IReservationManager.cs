using KnihovnaLouda.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KnihovnaLouda.Interfaces.IManager
{
    public interface IReservationManager
    {
        Task<List<Reservation>> GetUserReservationsAsync(string userId);
        Task<List<Book>> GetAllBooksAsync();
        Task<List<SelectListItem>> GetAllUsersAsync();
        Task<bool> CreateReservationAsync(Reservation reservation, string? userId);
    }

}
