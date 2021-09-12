using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository;
using Titinski.WebAPI.Services.ImageRepository;

namespace Titinski.WebAPI.Handlers
{
    public class MainHandler : IMainHandler
    {
        private readonly ILogger<MainHandler> _logger;
        private readonly IImageRepo _imageRepo;
        private readonly IImageMetadataRepository _imageMetadataRepo;

        public MainHandler(
            ILogger<MainHandler> logger,
            IImageRepo imageRepo,
            IImageMetadataRepository metadataRepo            
            )
        {
            _logger = logger;
            _imageRepo = imageRepo;
            _imageMetadataRepo = metadataRepo;
        }

        public async System.Threading.Tasks.Task<IActionResult> GetRantAsync(string id)
        {
            var savedRant = await _imageMetadataRepo.GetAsync(id);
            if (savedRant != null)
            {
                return new OkObjectResult(savedRant);
            }
            else
            {
                return new NoContentResult();
            }
            
        }

        public async Task<IActionResult> OnPostUploadAsync(Models.RantPost newPost)
        {
            _logger.LogInformation($"New RantPost received");

            if (newPost.ImageFile.Length > 0)
            {

                var fileRelativePath = _imageRepo.AddRant(newPost);
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
