using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titinski.WebAPI.Handlers
{
    public class MainHandler : IMainHandler
    {
        private readonly ILogger<MainHandler> _logger;
        private readonly Random _rnd;

        public MainHandler(
            ILogger<MainHandler> logger
            )
        {
            _logger = logger;
            _rnd = new Random();
        }

        public async System.Threading.Tasks.Task<IActionResult> GetRantAsync()
        {
            var imageBytes = await System.IO.File.ReadAllBytesAsync(".\\Assets\\Images\\ALL-Grainfee-Dog2-spot.jpg");
            var file = new FileContentResult(imageBytes, "image/jpg");
            return file;
        }

        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = System.IO.Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return new OkObjectResult(new { count = files.Count, size });
        }
    }
}
