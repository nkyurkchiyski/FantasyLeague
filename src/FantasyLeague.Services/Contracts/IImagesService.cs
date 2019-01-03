using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IImagesService
    {
        IImageUploadResult Upload(IFormFile file, string type);

        Task<IServiceResult> Create(string type, Guid entityId, string publicId, string url);
    }
}
