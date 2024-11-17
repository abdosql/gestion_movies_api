using mastering_.NET_API.Services.Interfaces;

namespace mastering_.NET_API.Services
{
    public class FileUpload : IFileUpload
    {
        private IWebHostEnvironment _env;
        private readonly string[] allowedExtensions;
        public UploadState uploadState { get; set; }
        private const long MaxFileSize = 5 * 1024 * 1024;



        public FileUpload(IWebHostEnvironment env)
        {
            _env = env;
            allowedExtensions = new string[]{ ".jpg", ".png", ".jpeg" };
            uploadState = new UploadState();
        }


        private bool VerifyExtension(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);

            if (allowedExtensions.Contains(fileExtension.ToLower()))
            {
                return true;
            }
            this.uploadState.Message = "Invalid file extension.";
            this.uploadState.State = false;
            return false;
        }


        private bool VerifyLength(IFormFile file)
        {
            if (file.Length < MaxFileSize)
            {
                return true;
            }
            this.uploadState.Message = "Image size must be less than 5 MB.";
            this.uploadState.State = false;
            return false;
        }


        public void UploadImage(IFormFile file, string path)
        {
            if (VerifyExtension(file))
            {
                if (VerifyLength(file))
                {
                    this.uploadState.PhotoName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string pathFile = Path.Combine(_env.ContentRootPath, path, uploadState.PhotoName);
                    Directory.CreateDirectory(Path.GetDirectoryName(pathFile));

                    using (FileStream stream = System.IO.File.Create(pathFile))
                    {
                        file.CopyTo(stream);
                    }

                    this.uploadState.Message = "Image uploaded successfully.";
                    this.uploadState.State = true;
                }
            }
        }

        public bool DeleteImage(string photoName, string path)
        {
            string pathFile = Path.Combine(_env.ContentRootPath, path, photoName);

            if (System.IO.File.Exists(pathFile))
            {
                try
                {
                    System.IO.File.Delete(pathFile);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }


    }
}
