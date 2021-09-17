using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titinski.WebAPI.EFCore.Repositories;
using Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository;
using Titinski.WebAPI.Interfaces.UnitOfWork;

namespace Titinski.WebAPI.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ImagesDbContext _context;
        public IImageMetadataRepository ImageMetaDataRepo { get; private set; }

        public UnitOfWork(ImagesDbContext context, IImageMetadataRepository imageMetaDataRepo)
        {
            _context = context;
            ImageMetaDataRepo = imageMetaDataRepo;
        }

        public Task CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
