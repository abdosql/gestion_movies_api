namespace mastering_.NET_API.Services.Interfaces
{
    public interface IFileUpload
    {
        void UploadImage(IFormFile file, string path);
        bool DeleteImage(string photoName, string path);
    }
}
