using System.Collections.Generic;
using System.Threading.Tasks;
using udemy_course1.Core.Models;

namespace udemy_course1.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}