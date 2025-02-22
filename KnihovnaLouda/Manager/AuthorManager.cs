﻿using KnihovnaLouda.Interfaces.IManager;
using KnihovnaLouda.Interfaces.IRepository;
using KnihovnaLouda.Models;
using KnihovnaLouda.Repositories;

namespace KnihovnaLouda.Manager
{
    public class AuthorManager : IAuthorManager
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IPhotoManager _photoManager;   

        public AuthorManager(IAuthorRepository authorRepository, IPhotoManager photoManager)
        {
            _authorRepository = authorRepository;
            _photoManager = photoManager;

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
            if (string.IsNullOrEmpty(author.Name) || string.IsNullOrEmpty(author.Surname))
            {
                return false;
            }

            await _authorRepository.AddAsync(author);
            return true;
        }

        public async Task<bool> UpdateAuthorAsync(Author author, IFormFile? photo)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(author.Id);
            if (existingAuthor == null) throw new Exception("User not found.");

            if (photo != null)
            {
                _photoManager.DeletePhoto(existingAuthor.PhotoPath);
                author.PhotoPath = await _photoManager.SavePhotoAsync(photo, "AuthorPhotos");
            }
            else
            {
                author.PhotoPath = existingAuthor.PhotoPath;
            }


            existingAuthor.Name = author.Name;
            existingAuthor.Surname = author.Surname;
            existingAuthor.PhotoPath = author.PhotoPath;

            return await _authorRepository.UpdateAsync(existingAuthor);
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
