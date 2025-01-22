using KnihovnaLouda.Interfaces.IManager;
using KnihovnaLouda.Interfaces.IRepository;
using KnihovnaLouda.Models;
using KnihovnaLouda.Repositories;

namespace KnihovnaLouda.Manager
{
    public class AuthorManager : IAuthorManager
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateAuthorAsync(Author author)
        {
            if (string.IsNullOrEmpty(author.Name))
            {
                return false;
            }

            await _authorRepository.AddAsync(author);
            return true;
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            if (author == null || author.Id <= 0)
            {
                return false;
            }

            await _authorRepository.UpdateAsync(author);
            return true;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            if (!await _authorRepository.ExistsAsync(id))
            {
                return false;
            }

            await _authorRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _authorRepository.ExistsAsync(id);
        }
    }

}
