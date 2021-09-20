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
        private readonly IWebHostEnvironment host;
        private readonly IVehicleRepository repository;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        private readonly IPhotoRepository photoRepository;
        private readonly IPhotoService photoService;
        public PhotosController(IPhotoService photoService, IWebHostEnvironment host, IVehicleRepository repository, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoRepository photoRepository)
        {
            this.photoService = photoService;
            this.photoRepository = photoRepository;
            this.photoSettings = options.Value;
            this.mapper = mapper;
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

            // Upload photo using photoService, pass vehicle, file and folderpath
            var photo = await photoService.UploadPhoto(vehicle, file, uploadsFolderPath);

            // Mapping Photo to PhotoResource, and returning it.
            return Ok(mapper.Map<Photo, PhotoResource>(photo));

        }
    }
}