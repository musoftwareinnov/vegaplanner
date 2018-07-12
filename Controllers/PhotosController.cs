using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vega.Controllers.Resources;
using vega.Core;
using vega.Core.Models;

namespace vega.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        private readonly IVehicleRepository repository;

        public PhotosController(IHostingEnvironment host, IVehicleRepository repository,
                                IMapper mapper, 
                                //IPhotoRepository repository, 
                                IUnitOfWork unitOfWork)
        {
            this.host = host;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IVehicleRepository Repository { get; }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {   
            var vehicle = await repository.GetVehicle(vehicleId, includeRelated:false);
            if(vehicle == null)
                return NotFound();

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if(!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //Generate thumbnails - System.Drawing.Namespace!
            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
        
    }
}