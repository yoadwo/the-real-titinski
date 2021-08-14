using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titinski.WebAPI.AppSettings
{
    public class FtpConfig
    {
        // secret
        public string Address { get; set; }
        //secret
        public string Username { get; set; }
        //secret
        public string Password { get; set; }
        
        //config
        public string RootPath { get; set; }
        //config
        public string ImagesPath { get; set; }
    }
}
