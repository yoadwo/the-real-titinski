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
            foreach (var file in System.IO.Directory.GetFiles(".\\Assets\\Images"))
            {
                var imageBytes = System.IO.File.ReadAllBytes(file);
                var r = new Rant
                {
                    ImageBase64 = Convert.ToBase64String(imageBytes),
                    Description = new System.IO.FileInfo(file).Name
                };
                Rants.Add(r);
            }                        
        }

        public void AddRant(Rant rant)
        {
            Rants.Add(rant);
        }

        public Rant GetRant(int id)
        {
            Random rnd = new Random();
            return Rants[rnd.Next(Rants.Count - 1)];
        }
    }
}
