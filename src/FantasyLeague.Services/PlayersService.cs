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
        private readonly IRepository<Team> teamRepository;

        public PlayersService(
            IImagesService imagesService,
            IRepository<Player> playerRepository,
            IRepository<Team> teamRepository,
            IMapper mapper) : base(mapper)
        {
            this.imagesService = imagesService;
            this.playerRepository = playerRepository;
            this.teamRepository = teamRepository;
        }

        private bool ValidPlayerDetailedViewModel(PlayerDetailedViewModel model)
        {
            return model.TeamId != Guid.Empty &&
                   model.Price > 0 &&
                   model.Price <= GlobalConstants.MaxPlayerPrice &&
                   !string.IsNullOrEmpty(model.Name) &&
                   !string.IsNullOrEmpty(model.Nationality) &&
                   !string.IsNullOrWhiteSpace(model.Name) &&
                   !string.IsNullOrWhiteSpace(model.Nationality);
        }

        private Player CreatePlayer(PlayerDetailedViewModel model)
        {
            return new Player
            {
                Name = model.Name,
                TeamId = model.TeamId,
                Price = model.Price,
                Nationality = model.Nationality,
                Position = model.Position
            };
        }

        public ICollection<T> All<T>()
        {
            var players = this.playerRepository.All().ToList();

            var models = players.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public async Task<IServiceResult> ArchiveAsync(Guid playerId)
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

            player.Active = false;
            await this.playerRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public async Task<IServiceResult> CreateAsync(PlayerDetailedViewModel model)
        {
            var result = new ServiceResult { Succeeded = false };

            if (model == null)
            {
                result.Error = string.Format(ExceptionConstants.InvalidInputException);
                return result;
            }

            if (!this.ValidPlayerDetailedViewModel(model))
            {
                result.Error = string.Format(ExceptionConstants.InvalidInputException);
                return result;
            }

            var team = await this.teamRepository
                .GetByIdAsync(model.TeamId);

            if (team == null)
            {
                result.Error = string.Format(ExceptionConstants.InvalidInputException);
                return result;
            }

            Player player = this.CreatePlayer(model);

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

                var image = await this.imagesService.CreateAsync(
                      GlobalConstants.PlayerName,
                      uploadResult.PublicId,
                      uploadResult.Url);

                player.Image = image;
            }

            this.playerRepository.Add(player);
            await this.playerRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public async Task<IServiceResult> EditAsync(PlayerDetailedViewModel model)
        {
            var result = new ServiceResult { Succeeded = false };

            if (model == null)
            {
                result.Error = string.Format(ExceptionConstants.InvalidInputException);
                return result;
            }

            if (model.Id == Guid.Empty || !ValidPlayerDetailedViewModel(model))
            {
                result.Error = string.Format(ExceptionConstants.InvalidInputException);
                return result;
            }

            var team = await this.teamRepository
               .GetByIdAsync(model.TeamId);

            if (team == null)
            {
                result.Error = string.Format(ExceptionConstants.InvalidInputException);
                return result;
            }

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

            if (model.Image != null)
            {
                if (player.Image != null)
                {
                    this.imagesService.Delete(player.Image);
                }

                var uploadResult = this.imagesService.Upload(
                    model.Image,
                    GlobalConstants.PlayerName);

                if (uploadResult == null)
                {
                    result.Error = ExceptionConstants.FailedUploadException;
                    return result;
                }

                var image = await this.imagesService.CreateAsync(
                      GlobalConstants.PlayerName,
                      uploadResult.PublicId,
                      uploadResult.Url);

                player.Image = image;
            }

            await this.playerRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public async Task<T> GetPlayerAsync<T>(Guid playerId)
        {
            var player = await this.playerRepository
               .GetByIdAsync(playerId);

            var model = this.mapper.Map<T>(player);

            return model;
        }

        public async Task<IServiceResult> RestoreAsync(Guid playerId)
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

            player.Active = true;
            await this.playerRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }
    }
}
