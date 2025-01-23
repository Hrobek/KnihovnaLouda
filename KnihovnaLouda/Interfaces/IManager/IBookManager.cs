using KnihovnaLouda.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KnihovnaLouda.Interfaces.IManager
{
    public interface IBookManager
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<bool> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book, IFormFile? Photo);
        Task<bool> DeleteBookAsync(int id);
        Task<List<Author>> GetAllAuthorsAsync();
        Task<bool> ExistsAsync(int id);
    }
}
