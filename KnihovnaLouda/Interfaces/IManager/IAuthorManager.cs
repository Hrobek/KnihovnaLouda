using KnihovnaLouda.Models;

namespace KnihovnaLouda.Interfaces.IManager
{
    public interface IAuthorManager
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<bool> CreateAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author, IFormFile? photo);
        Task<bool> DeleteAuthorAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

}
