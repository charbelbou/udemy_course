using System.Collections.Generic;
using System.Threading.Tasks;
using udemy.Models;
using udemy_course1.Core.Models;

namespace udemy_course.Persistence
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id,bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);

        // Added function to repository
        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
    }
}