using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Services.ImageMetadataRepository
{
    public interface IImageMetadataRepo
    {
        public string AddRant(RantPost rant, string fileRelativePath);
        public Rant GetRant(string id);
    }
}
