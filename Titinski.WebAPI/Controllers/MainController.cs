using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Titinski.WebAPI.Handlers;

namespace Titinski.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;
        private readonly IMainHandler _mainHandler;

        public MainController(
            IMainHandler mainHandler,
            ILogger<MainController> logger
            )
        {
            _mainHandler = mainHandler;
            _logger = logger;
        }

        [HttpGet]
        [Route("rant/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetRantAsync(string id)
        {
            return await _mainHandler.GetRantAsync(id);
        }

        [HttpGet]
        [Route("rant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllRantsAsync()
        {
            return await _mainHandler.GetAllRantsAsync();
        }

        [HttpPost("rant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SubmitForm([FromForm] Models.RantPost newPost)
        {
            try
            {
                return await _mainHandler.OnPostUploadAsync(newPost);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return Problem(e.Message);
            }
        }
    }
}
