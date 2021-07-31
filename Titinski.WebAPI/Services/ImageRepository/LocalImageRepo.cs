using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Services.ImageRepository
{
    public class LocalImageRepo : IImageRepo
    {
        private List<Rant> Rants;
        public LocalImageRepo()
        {
            Rants = new List<Rant>();
            var count = 0;
            foreach (var file in System.IO.Directory.GetFiles(".\\Assets\\Images"))
            {
                var imageBytes = System.IO.File.ReadAllBytes(file);
                var r = new Rant
                {
                    ID = count.ToString(),
                    ImageBase64 = Convert.ToBase64String(imageBytes),
                    Description = new System.IO.FileInfo(file).Name
                };
                Rants.Add(r);
                count++;
            }
        }

        public void AddRant(Rant rant)
        {
            rant.ID = Rants.Count.ToString();
            Rants.Add(rant);
        }

        public Rant GetRant(string id)
        {
            return Rants.FirstOrDefault(r => r.ID == id);
        }
    }
}
