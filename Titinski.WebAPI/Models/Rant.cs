using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titinski.WebAPI.Models
{
    public class Rant
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string ImageBase64 { get; set; }
    }
}
