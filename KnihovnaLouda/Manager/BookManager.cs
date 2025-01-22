using KnihovnaLouda.Interfaces.IManager;
using KnihovnaLouda.Interfaces.IRepository;
using KnihovnaLouda.Models;
using Microsoft.EntityFrameworkCore;

namespace KnihovnaLouda.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateBookAsync(Book book)
        {
            if (book == null || book.AuthorId <= 0)
            {
                return false;
            }

            await _bookRepository.AddAsync(book);     
            return true;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            if (book == null || book.Id <= 0)
            {
                return false;
            }

            await _bookRepository.UpdateAsync(book);
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            if (!await _bookRepository.ExistsAsync(id))
            {
                return false;
            }

            await _bookRepository.DeleteAsync(id);
            return true;
        }
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _bookRepository.GetAllAuthorsAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _bookRepository.ExistsAsync(id);
        }

    }

}
