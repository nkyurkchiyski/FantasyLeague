using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FantasyLeague.Services
{
    public class ImagesService : BaseService, IImagesService
    {
        private readonly IRepository<Image> imageRepository;
        private readonly Cloudinary cloudinary;

        public ImagesService(
            IRepository<Image> imageRepository,
            Cloudinary cloudinary,
            IMapper mapper) : base(mapper)
        {
            this.imageRepository = imageRepository;
            this.cloudinary = cloudinary;
        }

        public async Task<Image> CreateAsync(string type, string publicId, string url)
        {
            if (string.IsNullOrEmpty(type) ||
                string.IsNullOrEmpty(publicId) ||
                string.IsNullOrEmpty(url))
            {
                return null;
            }

            if (type != GlobalConstants.PlayerName &&
                type != GlobalConstants.TeamName)
            {
                return null;
            }

            var image = new Image
            {
                Url = url,
                ImageType = type,
                PublicId = publicId
            };

            this.imageRepository.Add(image);
            await this.imageRepository.SaveChangesAsync();

            return image;
        }

        public DelResResult Delete(Image image)
        {
            var result = this.cloudinary.DeleteResources(image.PublicId);
            this.imageRepository.Delete(image);

            return result;
        }

        public IImageUploadResult Upload(IFormFile file, string type)
        {
            if (file == null ||
                string.IsNullOrEmpty(type))
            {
                return null;
            }

            string fileName = Guid.NewGuid().ToString();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, file.OpenReadStream()),
            };

            switch (type)
            {
                case GlobalConstants.PlayerName:
                    uploadParams.Folder = GlobalConstants.PlayersFolderPath;
                    break;
                case GlobalConstants.TeamName:
                    uploadParams.Folder = GlobalConstants.TeamsFolderPath;
                    break;
                default:
                    return null;
            }

            ImageUploadResult uploadResult = new ImageUploadResult();

            var result = this.cloudinary.Upload(uploadParams);

            if (result != null)
            {
                uploadResult.PublicId = result.PublicId;
                uploadResult.Url = result.Uri.AbsoluteUri;
            }

            return uploadResult;
        }
    }
}
