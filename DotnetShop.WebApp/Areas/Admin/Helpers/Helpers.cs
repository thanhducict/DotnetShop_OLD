namespace DotnetShop.WebApp.Areas.Admin.Helpers
{
    public class Helpers : IHelpers
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Helpers(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public enum UploadFileType
        {
            ProductImage
        }

        /// <summary>
        /// Executes file uploading.
        /// </summary>
        /// <param name="file">File uploaded.</param>
        /// <param name="fileName">File name without its extension.</param>
        /// <returns>File url.</returns>
        public string UploadFile(IFormFile file, UploadFileType fileType, string fileName = "")
        {
            string fileUrl = string.Empty;
            string uploadLocation;

            switch (fileType)
            {
                case UploadFileType.ProductImage:
                    uploadLocation = "images/Products";
                    break;
                default:
                    uploadLocation = string.Empty;
                    break;
            }

            if (file != null)
            {
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, uploadLocation);
                    fileName = string.IsNullOrEmpty(fileName) ? file.FileName : $"{fileName}{Path.GetExtension(file.FileName)}";
                    string filePath = Path.Combine(uploadFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    fileUrl = $"/{uploadLocation}/{fileName}";
                }
            }

            return fileUrl;
        }
    }
}
