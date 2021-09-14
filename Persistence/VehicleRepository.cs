using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using udemy.Models;
using udemy.Persistence;
using udemy_course1.Core.Models;
using udemy_course1.Extensions;

namespace udemy_course.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly UdemyDbContext context;
        public VehicleRepository(UdemyDbContext context)
        {
            this.context = context;

        }
        // GetVehicles
        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObject){
            var result = new QueryResult<Vehicle>();

            // Get vehicles with model, makes and features
            var query = context.Vehicles
            .Include(v=>v.Model)
                .ThenInclude(m=>m.Make)
            .Include(v=>v.Features)
                .ThenInclude(vf=>vf.Feature).AsQueryable();

            //  If MakeId has value, update query to get vehicles with specific make
            if(queryObject.MakeId.HasValue){
                query = query.Where(v => v.Model.MakeId == queryObject.MakeId.Value);
            }

            //  If ModelId has value, update query to get vehicles with specific ModelID
            if(queryObject.ModelId.HasValue){
                query = query.Where(v => v.ModelId == queryObject.ModelId.Value);
            }

            // Mapping specific strings to lamda functions.
            var columnMaps = new Dictionary<string,Expression<Func<Vehicle,object>>>(){
                ["make"] = v=>v.Model.Make.Name,
                ["model"] = v=>v.Model.Name,
                ["contactName"] = v=>v.ContactName,
                ["id"] = v=>v.Id,
            };

            // Apply ordering (sorting) with queryobject and the map
            query = query.ApplyOrdering(queryObject,columnMaps);

            // Assign totalItem count to result (For paging)
            result.TotalItems = await query.CountAsync();

            // Apply pagining to query
            query = query.ApplyPaging(queryObject);

            // Assign Items to result
            result.Items = await query.ToListAsync();

            return result;
        }

        // Get Vehicle with id
        // includeRelated determines whether the object will include model and feature objects.
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if(!includeRelated){
                return await context.Vehicles.FindAsync(id);
            }
            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle){
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle){
            context.Vehicles.Remove(vehicle);
        }
    }
}