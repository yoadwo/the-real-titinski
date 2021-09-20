using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.EFCore.Repositories
{
    public class SqlRepo : GenericRepository<Rant>, IImageMetadataRepository
    {
        private readonly ILogger<SqlRepo> _logger;

        public SqlRepo(
                ImagesDbContext imagesDbContext,
                ILogger<SqlRepo> logger
            ) :base(imagesDbContext)
        {
            _logger = logger;
        }

        public Rant AddRant(RantPost rant, string fileRelativePath)
        {
            var r = new Rant()
            {
                // file relative path includes a timestamp so that should make ID unique enough
                ID = fileRelativePath.GetHashCode().ToString(),
                Description = rant.Description,
                Path = fileRelativePath
            };
            return Add(r);
        }
    }
}
