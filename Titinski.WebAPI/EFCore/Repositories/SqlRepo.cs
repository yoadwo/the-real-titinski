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
                ImageRepoDbContext imageRepoDbContext,
                ILogger<SqlRepo> logger
            ) :base(imageRepoDbContext)
        {
            _logger = logger;
        }
        public Task<Rant> AddRantAsync(RantPost rant, string fileRelativePath)
        {
            var r = new Rant()
            {
                ID = rant.GetHashCode().ToString(),
                Description = rant.Description,
                Path = fileRelativePath
            };

            return AddAsync(r);
        }
    }
}
