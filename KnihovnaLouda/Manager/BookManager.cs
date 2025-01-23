using KnihovnaLouda.Interfaces.IManager;
using KnihovnaLouda.Interfaces.IRepository;
using KnihovnaLouda.Models;
using Microsoft.EntityFrameworkCore;

namespace KnihovnaLouda.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPhotoManager _photoManager;
        public BookManager(IBookRepository bookRepository, IPhotoManager photoManager)
        {
            _bookRepository = bookRepository;
            _photoManager = photoManager;

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

        public async Task<bool> UpdateBookAsync(Book book, IFormFile? photo)
        {
            var existingBook = await _bookRepository.GetByIdAsync(book.Id);
            if (existingBook == null) return false;

            if (photo != null)
            {
                _photoManager.DeletePhoto(existingBook.PhotoPath);
                book.PhotoPath = await _photoManager.SavePhotoAsync(photo, "BooksPhotos");
            }
            else
            {
                book.PhotoPath = existingBook.PhotoPath;
            }

       
            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.Published = book.Published;
            existingBook.AuthorId = book.AuthorId;
            existingBook.PhotoPath = book.PhotoPath;

            return await _bookRepository.UpdateAsync(existingBook);
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
