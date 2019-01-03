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

        public async Task<IServiceResult> Create(string type, Guid entityId, string publicId, string url)
        {
            var result = new ServiceResult { Succeeded = false };

            var image = new Image
            {
                Url = url,
                ImageType = type,
                PublicId = publicId
            };

            if (type == GlobalConstants.PlayerName)
            {
                image.PlayerId = entityId;
            }
            else if (type == GlobalConstants.PlayerName)
            {
                image.TeamId = entityId;
            }
            else
            {
                result.Error = string.Format(
                    ExceptionConstants.UnknownValueException,
                    type, GlobalConstants.TypeName);
                return result;
            }

            this.imageRepository.Add(image);
            await this.imageRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;

        }

        public IImageUploadResult Upload(IFormFile file, string type)
        {
            string fileName = Guid.NewGuid().ToString();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, file.OpenReadStream()),
            };

            if (type == GlobalConstants.PlayerName)
            {
                uploadParams.Folder = GlobalConstants.PlayersFolderPath;
            }
            else if (type == GlobalConstants.TeamName)
            {
                uploadParams.Folder = GlobalConstants.TeamsFolderPath;
            }
            else
            {
                return null;
            }

            ImageUploadResult uploadResult = null;

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
