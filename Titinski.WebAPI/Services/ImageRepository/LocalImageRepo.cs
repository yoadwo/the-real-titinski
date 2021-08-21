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
                    Description = new System.IO.FileInfo(file).Name
                };
                Rants.Add(r);
                count++;
            }
        }

        public string AddRant(RantPost rant)
        {
            var r = new Rant
            {
                ID = Rants.Count.ToString(),
                Description = rant.Description

            };
            Rants.Add(r);
            return Rants[Rants.IndexOf(r)].ToString();
        }

        public Rant GetRant(string id)
        {
            return Rants.FirstOrDefault(r => r.ID == id);
        }
    }
}
