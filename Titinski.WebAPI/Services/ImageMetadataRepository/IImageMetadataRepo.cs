using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Services.ImageMetadataRepository
{
    public interface IImageMetadataRepo
    {
        public void AddRant(RantPost rant);
        public Rant GetRant(string id);
    }
}
