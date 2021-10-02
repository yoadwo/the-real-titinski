using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Interfaces.Storage
{
    public interface IImageStorage
    {
        /// <summary>
        /// Adds the image included in the request
        /// </summary>
        /// <param name="rant">The Rant object received from client</param>
        /// <returns>Image URI in the repo</returns>
        public string SaveRant(RantPost rant);
        public System.IO.Stream LoadRant(string path);
    }
}
