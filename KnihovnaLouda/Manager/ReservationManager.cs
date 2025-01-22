using KnihovnaLouda.Interfaces.IManager;
using KnihovnaLouda.Interfaces.IRepository;
using KnihovnaLouda.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KnihovnaLouda.Manager
{
    public class ReservationManager : IReservationManager
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationManager(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<List<Reservation>> GetUserReservationsAsync(string userId)
        {
            return await _reservationRepository.GetUserReservationsAsync(userId);
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _reservationRepository.GetAllBooksAsync();
        }

        public async Task<List<SelectListItem>> GetAllUsersAsync()
        {
            var users = await _reservationRepository.GetAllUsersAsync();
            return users.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = $"{u.FirstName} {u.LastName}"
            }).ToList();
        }

        public async Task<bool> CreateReservationAsync(Reservation reservation, string? userId)
        {
            if (reservation.BookId <= 0)
            {
                return false;
            }

            reservation.UserId = reservation.UserId ?? userId;
            var book = await _reservationRepository.GetByIdAsync(reservation.BookId);

            if (book == null)
            {
                return false;
            }

            await _reservationRepository.AddAsync(reservation);
            return true;
        }
    }

}
