using static DotnetShop.WebApp.Areas.Admin.Helpers.Helpers;

namespace DotnetShop.WebApp.Areas.Admin.Helpers
{
    public interface IHelpers
    {
        public string UploadFile(IFormFile file, UploadFileType fileType, string fileName);
    }
}