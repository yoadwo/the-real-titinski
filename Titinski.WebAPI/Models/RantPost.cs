using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Titinski.WebAPI.Models
{
    public class RantPost
    {
        [Required] public string Description { get; set; }
        [Required] public IFormFile ImageFile { get; set; }
    }
}
