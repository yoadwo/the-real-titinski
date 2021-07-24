using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public IActionResult GetRantAsync()
        {
            throw new NotImplementedException();
        }
    }
}
