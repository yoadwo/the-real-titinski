using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titinski.WebAPI.Models
{
    public class ImageRepoDbContext : DbContext
    {
        public ImageRepoDbContext(DbContextOptions<ImageRepoDbContext> options)
            :base(options)
        {

        }

        public DbSet<Models.Rant> Rants { get; set; }
    }
}
