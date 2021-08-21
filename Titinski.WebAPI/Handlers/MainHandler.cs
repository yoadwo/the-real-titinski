using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titinski.WebAPI.Services.ImageRepository;
using Titinski.WebAPI.Services.ImageMetadataRepository;

namespace Titinski.WebAPI.Handlers
{
    public class MainHandler : IMainHandler
    {
        private readonly ILogger<MainHandler> _logger;
        private readonly IImageRepo _imageRepo;
        private readonly IImageMetadataRepo _imageMetadataRepo;

        public MainHandler(
            ILogger<MainHandler> logger,
            IImageRepo imageRepo,
            IImageMetadataRepo metadataRepo
            )
        {
            _logger = logger;
            _imageRepo = imageRepo;
            _imageMetadataRepo = metadataRepo;
        }

        public async System.Threading.Tasks.Task<IActionResult> GetRantAsync(string id)
        {
            //var savedRant = _imageRepo.GetRant(id);
            var savedRant = await _imageMetadataRepo.GetRantAsync(id);
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
                string id = _imageMetadataRepo.AddRant(newPost, fileRelativePath);

                Models.Rant r = new Models.Rant()
                {
                    ID = id,
                    Description = newPost.Description,
                    Path = fileRelativePath
                };

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
