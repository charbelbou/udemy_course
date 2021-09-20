using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace udemy_course1.Core
{
    //IPhotoStorage interface
    public interface IPhotoStorage
    {
        Task<string> StorePhoto(string uploadFolderPath,IFormFile file);
    }
}