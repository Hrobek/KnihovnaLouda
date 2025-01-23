using KnihovnaLouda.Models;

namespace KnihovnaLouda.Interfaces.IRepository
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetUserReservationsAsync(string userId);
        Task<List<Book>> GetAllBooksAsync();
        Task<List<ApplicationsUser>> GetAllUsersAsync();
        Task<Reservation?> GetByIdAsync(int id);
        Task AddAsync(Reservation reservation);
        Task<Book?> GetBookByIdAsync(int id);
    }

}
