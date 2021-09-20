using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using udemy.Models;
using udemy_course.Persistence;
using udemy_course1.Core.Models;

namespace udemy_course1.Core
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPhotoStorage photoStorage;
        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            this.photoStorage = photoStorage;
            this.unitOfWork = unitOfWork;

        }
        public async Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadsFolderPath)
        {
            // Store photo using photoStorage, pass the folderpath and the file.
            // IPhotoStorage is used, as there are different implementations
            // to upload photo depending on where application is being hosted.
            var fileName = await photoStorage.StorePhoto(uploadsFolderPath,file);

            // Creating Photo and adding it to vehicle.Photos
            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            // Using unitOfWork to save changes
            await unitOfWork.CompleteAsync();
            
            return photo;
        }
    }
}