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
                ImagesDbContext imageRepoDbContext,
                ILogger<SqlRepo> logger
            ) :base(imageRepoDbContext)
        {
            _logger = logger;
        }
        public Rant AddRant(RantPost rant, string fileRelativePath)
        {
            var r = new Rant()
            {
                ID = rant.GetHashCode().ToString(),
                Description = rant.Description,
                Path = fileRelativePath
            };
            return Add(r);
        }
    }
}
