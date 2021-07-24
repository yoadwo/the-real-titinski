using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public IActionResult GetRant()
        {
            return _mainHandler.GetRantAsync();
        }

    }
}
