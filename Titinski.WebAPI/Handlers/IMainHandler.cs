using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titinski.WebAPI.Handlers
{
    public interface IMainHandler
    {
        public Task<IActionResult> GetRantAsync(string id);
        public Task<IActionResult> OnPostUploadAsync(Models.RantPost newPost);
    }
}
