using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Services.ImageMetadataRepository
{
    public class SqlRepo : IImageMetadataRepo
    {
        private readonly IOptions<AppSettings.SqlConfig> _sqlConfig;
        public SqlRepo(
            IOptions<AppSettings.SqlConfig> sqlConfig
            )
        {
            _sqlConfig = sqlConfig;   
        }
        public string AddRant(RantPost rant, string fileRelativePath)
        {
            throw new NotImplementedException();
        }

        public Rant GetRant(string id)
        {
            throw new NotImplementedException();
        }
    }
}
