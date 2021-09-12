using System.Threading.Tasks;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository
{
    public interface IImageMetadataRepository : IGenericRepository<Rant>
    {
        public Task<Rant> AddRantAsync(RantPost rant, string fileRelativePath);
        //public Task<Rant> GetRantAsync(string id);
    }
}
