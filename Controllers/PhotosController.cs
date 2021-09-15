using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using udemy_course.Persistence;
using udemy_course1.Controllers.Resources;
using udemy_course1.Core;
using udemy_course1.Core.Models;
using udemy_course1.Persistence;

namespace udemy_course1.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]

    // Photos API
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        private readonly IPhotoRepository photoRepository;
        public PhotosController(IHostingEnvironment host, IVehicleRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.host = host;
            this.repository = repository;
        }
        // Get Photos with vehicle ID
        // Map from Photo to PhotoResource
        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos = await photoRepository.GetPhotos(vehicleId);

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }

        // Upload Photo
        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            // Getting the vehicle object which we're adding a photo to
            var vehicle = await repository.GetVehicle(vehicleId, includeRelated: false);
            // if null return NotFound
            if (vehicle == null)
            {
                return NotFound();
            }

            // Checking if file input is null
            if (file == null)
            {
                return BadRequest("Null file");
            }

            // Checking if file is empty
            if (file.Length == 0)
            {
                return BadRequest("Empty file");
            }

            // Checking if file exceeds size limit
            if (file.Length > photoSettings.MaxBytes)
            {
                return BadRequest("Max file size exceeded");
            }

            // Checking if file type is accepted
            if (!photoSettings.IsSupported(file.FileName))
            {
                return BadRequest("Invalid file type.");
            }


            // host.WebRootPath = "wwwroot"
            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");

            // If directory doesn't exist, create foler
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            // for security purposes, to prevent user from changing filename and accessing other files
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            // get full filePath with uploadsFolderPath
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            // copying file into stream
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Creating Photo and adding it to vehicle.Photos
            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            // Using unitOfWork to save changes
            await unitOfWork.CompleteAsync();

            // Mapping Photo to PhotoResource, and returning it.
            return Ok(mapper.Map<Photo, PhotoResource>(photo));

        }
    }
}