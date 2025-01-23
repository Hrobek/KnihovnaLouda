using KnihovnaLouda.Models;

namespace KnihovnaLouda.Interfaces.IRepository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task <bool>UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Author>> GetAllAuthorsAsync();
    }
}
