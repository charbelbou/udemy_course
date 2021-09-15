using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using udemy.Persistence;
using udemy_course1.Core;
using udemy_course1.Core.Models;

namespace udemy_course1.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly UdemyDbContext context;
        public PhotoRepository(UdemyDbContext context)
        {
            this.context = context;

        }
        // Photo Repository
        // Get photos which belong to specific vehicle ID
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await context.Photos
            .Where(p => p.VehicleId == vehicleId)
            .ToListAsync();
        }
    }
}