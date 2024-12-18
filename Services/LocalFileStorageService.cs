using mastering_.NET_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace mastering_.NET_API.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalFileStorageService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(bool IsSuccess, string FileName)> SaveFileAsync(IFormFile file, string directory)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return (false, string.Empty);
                }

                string uploadsFolder = Path.Combine(_environment.ContentRootPath, directory);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return (true, uniqueFileName);
            }
            catch (Exception)
            {
                return (false, string.Empty);
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName, string directory)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            try
            {
                string filePath = Path.Combine(_environment.ContentRootPath, directory, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetFileUrl(string fileName, string directory)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}/files/{fileName}";
        }
    }
} 