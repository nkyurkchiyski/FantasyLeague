using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IRepository<User> usersRepository;
        private readonly IRepository<Team> teamsRepository;

        public UsersService(
            IRepository<User> usersRepository,
            IRepository<Team> teamsRepository,
            IMapper mapper) : base(mapper)
        {
            this.usersRepository = usersRepository;
            this.teamsRepository = teamsRepository;
        }

        public IServiceResult ClubNameVacant(string clubName)
        {
            var result = new ServiceResult { Succeeded = false };

            result.Succeeded = usersRepository.All()
                .FirstOrDefault(x => x.ClubName == clubName) == null ?
                true : false;

            if (!result.Succeeded)
            {
                result.Error = string.Format(
                    ExceptionConstants.AlreadyTakenClubNameException,
                    clubName);
            }

            return result;
        }

        public async Task<string> GetClubNameAsync(string userId)
        {
            var user = await this.usersRepository
                .GetByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return user.ClubName;
        }

        public async Task<Team> GetFavouriteTeamAsync(string userId)
        {
            var user = await this.usersRepository
                .GetByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return user.FavouriteTeam;
        }

        public ICollection<T> GetLeaderboard<T>()
        {
            var users = this.usersRepository.All().ToList();

            var models = users.Select(x => this.mapper.Map<T>(x)).ToList();

            return models;
        }

        public T GetUserByUsername<T>(string username)
        {
            var user = this.usersRepository.All()
                .FirstOrDefault(x => x.UserName == username);

            var model = this.mapper.Map<T>(user);

            return model;
        }

        public async Task<IServiceResult> SetFavouriteTeamAsync(string userId, string teamName)
        {
            var result = new ServiceResult { Succeeded = false };

            var user = await this.usersRepository.GetByIdAsync(userId);

            if (user == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.UserName);
                return result;
            }

            if (teamName != null)
            {

                var team = this.teamsRepository.All()
                    .FirstOrDefault(x => x.Name == teamName);

                if (team != null)
                {
                    user.FavouriteTeamId = team.Id;
                }
                else
                {
                    result.Error = string.Format(
                     ExceptionConstants.NotFoundException,
                     GlobalConstants.TeamName);
                    return result;
                }

            }

            await usersRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }
    }
}
