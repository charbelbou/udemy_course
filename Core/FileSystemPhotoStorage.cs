using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace udemy_course1.Core
{
    public class FileSystemPhotoStorage : IPhotoStorage
    {
        public async Task<string> StorePhoto(string uploadFolderPath, IFormFile file)
        {
            // If directory doesn't exist, create foler
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            // for security purposes, to prevent user from changing filename and accessing other files
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            // get full filePath with uploadsFolderPath
            var filePath = Path.Combine(uploadFolderPath, fileName);

            // copying file into stream
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // return filename to use.
            return fileName;
        }
    }
}