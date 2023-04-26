using System.Net;

namespace StudentAdminPortal.API.Repository
{
    public interface IImageRepository
    {
       Task<string>Upload(IFormFile file, string fileName);
    }
}
