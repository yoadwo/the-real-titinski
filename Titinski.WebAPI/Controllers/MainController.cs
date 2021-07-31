using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Titinski.WebAPI.Handlers;

namespace Titinski.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;

        private IMainHandler _mainHandler;

        public MainController(
            IMainHandler mainHandler,
            ILogger<MainController> logger
            )
        {
            _mainHandler = mainHandler;
            _logger = logger;
        }

        [HttpGet]
        [Route("rant")]
        public async Task<IActionResult> GetRantAsync()
        {
            return await _mainHandler.GetRantAsync();
        }

        [HttpPost]
        [Route("rant")]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            return await _mainHandler.OnPostUploadAsync(files);
        }

        /*//[HttpPost("{id:int}/forms")]
        [HttpPost]
        //[ProducesResponseType(typeof(FormSubmissionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormSubmissionResult>> SubmitForm(int id, [FromForm] StudentForm form)
        {
            _logger.LogInformation($"validating the form#{form.FormId} for Student ID={id}");
            _logger.LogInformation($"saving file [{form.StudentFile.FileName}]");
            await Task.Delay(1500);
            _logger.LogInformation("file saved.");
            var result = new FormSubmissionResult { FormId = form.FormId, StudentId = id };
            return CreatedAtAction(nameof(ViewForm), new { id, form.FormId }, result);
        }

        public class StudentForm
        {
            [Required] public int FormId { get; set; }
            [Required] public IFormFile StudentFile { get; set; }
        }*/

    }
}
