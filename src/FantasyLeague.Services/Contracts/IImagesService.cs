using CloudinaryDotNet.Actions;
using FantasyLeague.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IImagesService
    {
        IImageUploadResult Upload(IFormFile file, string type);

        Task<Image> Create(string type, string publicId, string url);

        DelResResult Delete(Image image);
    }
}
