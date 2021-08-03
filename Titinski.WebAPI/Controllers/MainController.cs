using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<AppSettings.SqlConfig> _sqlConfig;

        private IMainHandler _mainHandler;

        public MainController(
            IMainHandler mainHandler,
            ILogger<MainController> logger,
            IOptions<AppSettings.SqlConfig> sqlConfig
            )
        {
            _mainHandler = mainHandler;
            _logger = logger;
            _sqlConfig = sqlConfig;
        }

        [HttpGet]
        [Route("rant/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetRantAsync(string id)
        {
            return await _mainHandler.GetRantAsync(id);
        }        

        [HttpPost("rantForm")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SubmitForm([FromForm] Models.RantPost newPost)
        {
            return await _mainHandler.OnPostUploadAsync(newPost);
        }
    }
}
