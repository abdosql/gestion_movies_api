using Microsoft.AspNetCore.Http;

namespace mastering_.NET_API.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<(bool IsSuccess, string FileName)> SaveFileAsync(IFormFile file, string directory);
        Task<bool> DeleteFileAsync(string fileName, string directory);
        string GetFileUrl(string fileName, string directory);
    }
} 