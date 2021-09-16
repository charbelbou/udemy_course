using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using udemy.Controllers.Resources;
using udemy.Models;
using udemy.Persistence;
using udemy_course.Controllers.Resources;
using udemy_course.Persistence;
using udemy_course1.Controllers.Resources;
using udemy_course1.Core.Models;

namespace udemy.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        // Injecting UdemyDbContext and Imapper into the constructor, and initializing the fields.
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IMapper mapper, IVehicleRepository repository,IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // HttpPost attribute
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVehicleAsync([FromBody] SaveVehicleResource VehicleResource)
        {
            // Checks if the model from the Body is consistent with VehicleResource
            // If the Model state isn't valid, return BadRequest
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map VehicleResource to Vehicle, then update time
            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(VehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            // Add vehicle to database and save changes
            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);

            // Map the Vehicle back to VehicleResource and return
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        // HttpPut attribute with id passed as parameter
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource VehicleResource)
        {
            // Checks if the model from the Body is consistent with VehicleResource
            // If the Model state isn't valid, return BadRequest
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Retrieve vehicle resource from database, including it's features
            // Vehicle is found through it's ID.
            var vehicle = await repository.GetVehicle(id);

            // If the vehicle is null, return NotFound()
            if (vehicle == null)
            {
                return NotFound();
            }

            // Map VehicleResource to Vehicle, essentially overriding the vehicle
            // object's properties Using the given VehicleResource

            // LastUpdate is also updated
            mapper.Map<SaveVehicleResource, Vehicle>(VehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            // Save Changes
            await unitOfWork.CompleteAsync();

            // Map the vehicle back to VehicleResource, and return it as a response
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        // HttpDelete attribute with id passed as parameter
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            // Retrieve vehicle through it's id
            var vehicle = await repository.GetVehicle(id, includeRelated: false);

            // if vehicle is null, return NotFound
            if (vehicle == null)
            {
                return NotFound();
            }
            // Remove Vehicle and save changes.
            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();

            //Return it's ID as a response
            return Ok(id);
        }

        // HttpGet attribute with id passed as parameter
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            // Retrieve Vehicle with it's features using the given ID.
            var vehicle = await repository.GetVehicle(id);

            // If vehicle is null,return NotFound()
            if (vehicle == null)
            {
                return NotFound();
            }

            // Map the vehicle back to VehicleResource and return it.
            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
        // Get vehicles, has VehicleQueryResource parameter
        [HttpGet]
        public async Task<QueryResultResource<VehicleResource>> GetVehicles(VehicleQueryResource filterResource){
            // Maps from VehicleQueryResource to VehicleQuery
            var filter = mapper.Map<VehicleQueryResource,VehicleQuery>(filterResource);
            // Gets vehicles with filter, returns QueryResult<Vehicle>
            var queryResult = await repository.GetVehicles(filter);
            // Maps result back to QueryResultResource with VehicleResource
            return mapper.Map<QueryResult<Vehicle>,QueryResultResource<VehicleResource>>(queryResult);
        }
    }
}