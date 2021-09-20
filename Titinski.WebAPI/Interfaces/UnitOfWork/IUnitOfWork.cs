using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository;

namespace Titinski.WebAPI.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IImageMetadataRepository ImageMetaDataRepo { get; }
        Task CompleteAsync();

    }
}
