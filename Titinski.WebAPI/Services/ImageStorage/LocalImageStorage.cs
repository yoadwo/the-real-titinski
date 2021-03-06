using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Titinski.WebAPI.Interfaces.Storage;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Services.ImageStorage
{
    public class LocalImageStorage : IImageStorage
    {
        private List<Rant> Rants;
        public LocalImageStorage()
        {
            Rants = new List<Rant>();
            var count = 0;
            foreach (var file in System.IO.Directory.GetFiles(".\\Assets\\Images"))
            {
                var imageBytes = System.IO.File.ReadAllBytes(file);
                var r = new Rant
                {
                    ID = count.ToString(),
                    Description = new System.IO.FileInfo(file).Name,
                    Path = file
                };
                Rants.Add(r);
                count++;
            }
        }

        public string SaveRant(RantPost rant)
        {
            var r = new Rant
            {
                ID = Rants.Count.ToString(),
                Description = rant.Description,
                Path = rant.ImageFile.FileName

            };
            Rants.Add(r);
            return Rants[Rants.IndexOf(r)].ToString();
        }

        public async Task<Stream> LoadRantAsync(string id)
        {
            var rant = Rants.FirstOrDefault(r => r.ID == id);
            var fs = new FileStream(rant.Path, FileMode.Open, FileAccess.Read);
            var ms = new MemoryStream();
            await fs.CopyToAsync(ms);
            return ms;
        }
    }
}
