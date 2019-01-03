using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Roster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IServiceResult ClubNameTaken(string clubName)
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

            return user.ClubName;
        }
        
        public async Task<Team> GetFavouriteTeamAsync(string userId)
        {
            var user = await this.usersRepository
                .GetByIdAsync(userId);

            return user.FavouriteTeam;
        }

        public T GetUser<T>(string username)
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

            var team = this.teamsRepository.All()
                .FirstOrDefault(x => x.Name == teamName);

            if (team != null)
            {
                user.FavouriteTeamId = team.Id;
            }
            else
            {
                user.FavouriteTeamId = null;
            }

            await usersRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }
    }
}
