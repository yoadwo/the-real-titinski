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
        private readonly ImageRepoDbContext _imageRepoDbContext;

        public SqlRepo(
            IOptions<AppSettings.SqlConfig> sqlConfig,
            ImageRepoDbContext imageRepoDbContext
            )
        {
            _sqlConfig = sqlConfig;
            _imageRepoDbContext = imageRepoDbContext;
        }
        public string AddRant(RantPost rant, string fileRelativePath)
        {
            var r = new Rant()
            {
                ID = rant.GetHashCode().ToString(),
                Description = rant.Description,
                Path = fileRelativePath
            };
            throw new NotImplementedException();
        }

        public async Task<Rant> GetRantAsync(string id)
        {
            var r = await _imageRepoDbContext.FindAsync<Models.Rant>(id);
            return r;
            throw new NotImplementedException();
        }
    }
}
