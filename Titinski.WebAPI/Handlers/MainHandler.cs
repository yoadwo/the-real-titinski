using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titinski.WebAPI.Services.ImageRepository;

namespace Titinski.WebAPI.Handlers
{
    public class MainHandler : IMainHandler
    {
        private readonly ILogger<MainHandler> _logger;
        private readonly IImageRepo _imageRepo;
        private readonly Random _rnd;

        public MainHandler(
            ILogger<MainHandler> logger,
            IImageRepo imageRepo
            )
        {
            _logger = logger;
            _imageRepo = imageRepo;

            _rnd = new Random();
        }

        public async System.Threading.Tasks.Task<IActionResult> GetRantAsync(string id)
        {
            var savedRant = _imageRepo.GetRant(id);
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
            _logger.LogInformation($"descrption: {newPost.Description}");
            _logger.LogInformation($"saving file :{newPost.ImageFile.FileName}");
            Models.Rant r;

            if (newPost.ImageFile.Length > 0)
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    await newPost.ImageFile.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    r = new Models.Rant
                    {
                        Description = newPost.Description,
                        ImageBase64 = s
                    };
                }
                _imageRepo.AddRant(r);
                _logger.LogInformation("file saved.");
                return new OkObjectResult(r);
            }
            else
            {
                return new BadRequestObjectResult(new ArgumentException("Empty file"));
            }

            
        }
    }
}
