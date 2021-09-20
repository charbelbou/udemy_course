using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using udemy.Models;
using udemy_course1.Core.Models;

namespace udemy_course1.Core
{
    // IPhotoService interface
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadsFolderPath);
    }
}