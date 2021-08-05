using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Services.ImageRepository
{
    public interface IImageRepo
    {
        public void AddRant(RantPost rant);
        public Rant GetRant(string id);
    }
}
