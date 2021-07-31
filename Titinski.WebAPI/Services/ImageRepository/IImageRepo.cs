using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titinski.WebAPI.Models;

namespace Titinski.WebAPI.Services.ImageRepository
{
    public interface IImageRepo
    {
        public void AddRant(Rant rant);
        public Rant GetRant(string id);
    }
}
