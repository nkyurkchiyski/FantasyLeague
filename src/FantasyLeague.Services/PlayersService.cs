using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Services
{
    public class PlayersService : BaseService, IPlayersService
    {
        private readonly IImagesService imagesService;
        private readonly IRepository<Player> playerRepository;

        public PlayersService(
            IImagesService imagesService,
            IRepository<Player> playerRepository,
            IMapper mapper) : base(mapper)
        {
            this.imagesService = imagesService;
            this.playerRepository = playerRepository;
        }

        public ICollection<T> All<T>()
        {
            var players = this.playerRepository.All();

            var models = players.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public ICollection<T> AllFromTeam<T>(Guid teamId)
        {
            var players = this.playerRepository.All()
                .Where(x => x.TeamId == teamId);

            var models = players.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public async Task<IServiceResult> Create(PlayerDetailedViewModel model)
        {
            var result = new ServiceResult { Succeeded = false };

            var player = new Player
            {
                Name = model.Name,
                TeamId = model.TeamId,
                Price = model.Price,
                Nationality = model.Nationality,
                Position = model.Position
            };

            this.playerRepository.Add(player);

            if (model.Image == null)
            {
                var createResult = await this.imagesService.Create(
                      GlobalConstants.PlayerName,
                      player.Id,
                      GlobalConstants.TemplatePlayerImagePublicId,
                      GlobalConstants.TemplatePlayerImageUrl);

                if (!createResult.Succeeded)
                {
                    result.Error = createResult.Error;
                    return result;
                }

            }
            else
            {
                var uploadResult = this.imagesService.Upload(
                    model.Image,
                    GlobalConstants.PlayerName);

                if (uploadResult == null)
                {
                    result.Error = ExceptionConstants.FailedUploadException;
                    return result;
                }

                var createResult = await this.imagesService.Create(
                      GlobalConstants.PlayerName,
                      player.Id,
                      GlobalConstants.TemplatePlayerImagePublicId,
                      GlobalConstants.TemplatePlayerImageUrl);

                if (!createResult.Succeeded)
                {
                    result.Error = createResult.Error;
                    return result;
                }
            }

            await this.playerRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public async Task<IServiceResult> Delete(Guid playerId)
        {
            var result = new ServiceResult { Succeeded = false };

            var player = await this.playerRepository
                .GetByIdAsync(playerId);

            if (player == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.PlayerName);

                return result;
            }

            this.playerRepository.Delete(player);
            await this.playerRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public async Task<IServiceResult> Edit(PlayerDetailedViewModel model)
        {
            var result = new ServiceResult { Succeeded = false };

            var player = await this.playerRepository
                .GetByIdAsync(model.Id);

            if (player == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.PlayerName);

                return result;
            }

            player.Name = model.Name;
            player.Nationality = model.Nationality;
            player.TeamId = model.TeamId;
            player.Price = model.Price;
            player.Position = model.Position;

            if (model.Image != null)
            {
                var uploadResult = this.imagesService.Upload(
                    model.Image,
                    GlobalConstants.PlayerName);

                if (uploadResult == null)
                {
                    result.Error = ExceptionConstants.FailedUploadException;
                    return result;
                }

                var createResult = await this.imagesService.Create(
                      GlobalConstants.PlayerName,
                      player.Id,
                      GlobalConstants.TemplatePlayerImagePublicId,
                      GlobalConstants.TemplatePlayerImageUrl);

                if (!createResult.Succeeded)
                {
                    result.Error = createResult.Error;
                    return result;
                }
            }

            await this.playerRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public async Task<T> GetPlayer<T>(Guid playerId)
        {
            var player = await this.playerRepository
               .GetByIdAsync(playerId);

            var model = this.mapper.Map<T>(player);

            return model;
        }
    }
}
