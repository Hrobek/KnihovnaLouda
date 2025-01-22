namespace KnihovnaLouda.Interfaces.IManager
{
    public interface IPhotoManager
    {
        Task<string> SavePhotoAsync(IFormFile file, string folder);
        void DeletePhoto(string filePath);
    }

}
