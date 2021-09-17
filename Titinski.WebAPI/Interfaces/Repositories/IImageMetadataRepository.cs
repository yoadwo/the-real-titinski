using System.Threading.Tasks;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository
{
    public interface IImageMetadataRepository : IGenericRepository<Rant>
    {
        public Rant AddRant(RantPost rant, string fileRelativePath);
        //public Task<Rant> GetRantAsync(string id);
    }
}
