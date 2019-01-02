using AutoMapper;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyLeague.Services
{
    public class TeamsService : BaseService, ITeamsService
    {
        private readonly IRepository<Team> teamsRepository;

        public TeamsService(
            IRepository<Team> teamsRepository,
            IMapper mapper) : base(mapper)
        {
            this.teamsRepository = teamsRepository;
        }

        public ICollection<T> All<T>()
        {
            var teams = this.teamsRepository.All();

            var models = teams.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public T GetTeamById<T>(Guid teamId)
        {
            var team = this.teamsRepository
               .GetByIdAsync(teamId)
               .GetAwaiter()
               .GetResult();

            var model = this.mapper.Map<T>(team);

            return model;
        }

        public T GetTeamByName<T>(string teamName)
        {
            var team = this.teamsRepository.All()
               .FirstOrDefault(x => x.Name == teamName);

            var model = this.mapper.Map<T>(team);

            return model;
        }
    }
}
