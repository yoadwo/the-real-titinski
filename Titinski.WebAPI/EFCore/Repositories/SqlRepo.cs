using System.Threading.Tasks;
using Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.EFCore.Repositories
{
    public class SqlRepo : GenericRepository<Rant>, IImageMetadataRepository
    {
        public SqlRepo(
            ImageRepoDbContext imageRepoDbContext
            ):base(imageRepoDbContext)
        {
            //_imageRepoDbContext = imageRepoDbContext;
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

        public async Task<Rant> GetRantAsync(string id)
        {
            var r = await _context.FindAsync<Models.Rant>(id);
            return r;
        }
    }
}
