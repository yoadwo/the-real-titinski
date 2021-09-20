using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titinski.WebAPI.EFCore
{
    public class ImagesDbContext : DbContext
    {
        public ImagesDbContext(DbContextOptions<ImagesDbContext> options)
            :base(options)
        {
        }

        public DbSet<Models.Rant> Rants { get; set; }
    }
}
