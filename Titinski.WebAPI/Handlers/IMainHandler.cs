using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titinski.WebAPI.Handlers
{
    public interface IMainHandler
    {
        public IActionResult GetRantAsync();
    }
}
