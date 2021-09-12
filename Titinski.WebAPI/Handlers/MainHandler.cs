using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository;
using Titinski.WebAPI.Interfaces.Storage;

namespace Titinski.WebAPI.Handlers
{
    public class MainHandler : IMainHandler
    {
        private readonly ILogger<MainHandler> _logger;
        private readonly IImageStorage _imageStorage;
        private readonly IImageMetadataRepository _imageMetadataRepo;

        public MainHandler(
            ILogger<MainHandler> logger,
            IImageStorage imageStorage,
            IImageMetadataRepository metadataRepo            
            )
        {
            _logger = logger;
            _imageStorage = imageStorage;
            _imageMetadataRepo = metadataRepo;
        }

        public async Task<IActionResult> GetRantAsync(string id)
        {
            var savedRant = await _imageMetadataRepo.GetAsync(id);
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
            var rants = await _imageMetadataRepo.GetAllAsync();
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

            if (newPost.ImageFile.Length > 0)
            {

                var fileRelativePath = _imageStorage.AddRant(newPost);
                Models.Rant r = await _imageMetadataRepo.AddRantAsync(newPost, fileRelativePath);

                _logger.LogInformation("Rant saved to image Repo and imageMetadata Repo.");
                return new OkObjectResult(r);
            }
            else
            {
                return new BadRequestObjectResult(new ArgumentException("Empty file"));
            }

            
        }
    }
}
