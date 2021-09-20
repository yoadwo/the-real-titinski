using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository;
using Titinski.WebAPI.Interfaces.Storage;
using Titinski.WebAPI.Interfaces.UnitOfWork;

namespace Titinski.WebAPI.Handlers
{
    public class MainHandler : IMainHandler
    {
        private readonly ILogger<MainHandler> _logger;
        private readonly IImageStorage _imageStorage;
        private readonly IUnitOfWork _unitOfWork;

        public MainHandler(
            ILogger<MainHandler> logger,
            IImageStorage imageStorage,
            IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _imageStorage = imageStorage;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> GetRantAsync(string id)
        {
            var savedRant = await _unitOfWork.ImageMetaDataRepo.GetAsync(id);
            if (savedRant != null)
            {
                return new OkObjectResult(savedRant);
            }
            else
            {
                _logger.LogInformation("No data exists for id {0}", id);
                return new NoContentResult();
            }
            
        }

        public async Task<IActionResult> GetAllRantsAsync()
        {
            var rants = await _unitOfWork.ImageMetaDataRepo.GetAllAsync();
            if (rants != null)
            {
                return new OkObjectResult(rants);
            }
            else
            {
                _logger.LogInformation("No data was found in the DB");
                return new NoContentResult();
            }

        }

        public async Task<IActionResult> OnPostUploadAsync(Models.RantPost newPost)
        {
            _logger.LogInformation($"New RantPost received");

            if (newPost.ImageFile.Length == 0)
            {
                return new BadRequestObjectResult(new ArgumentException("Empty file"));
            }

            // TODO: add rollback for FTP
            var fileRelativePath = _imageStorage.SaveRant(newPost);
            Models.Rant r = _unitOfWork.ImageMetaDataRepo.AddRant(newPost, fileRelativePath);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Rant saved to image storage and imageMetadata Repo.");
            return new OkObjectResult(r);
        }
    }
}
