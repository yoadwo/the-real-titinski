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

        public async System.Threading.Tasks.Task<IActionResult> GetRantAsync()
        {
            return new OkObjectResult(_imageRepo.GetRant(0));
        }

        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            Models.Rant r;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    /*
                     * var filePath = ".AssetsSystem.IO.Path.GetRandomFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                     */
                    
                    using (var ms = new System.IO.MemoryStream())
                    {
                        formFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        r = new Models.Rant
                        {
                            Description = formFile.FileName,
                            ImageBase64 = s
                        };
                    }
                    _imageRepo.AddRant(r);
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return new OkObjectResult(new { count = files.Count, size });
        }
    }
}
