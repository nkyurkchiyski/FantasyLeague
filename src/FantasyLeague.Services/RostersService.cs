using AutoMapper;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Roster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasyLeague.Services
{
    public class RostersService : BaseService, IRostersService
    {
        private readonly IRepository<Roster> rosterRepository;
        private readonly IRepository<Score> scoreRepository;

        public RostersService(
            IRepository<Roster> rosterRepository,
            IRepository<Score> scoreRepository,
            IMapper mapper) : base(mapper)
        {
            this.rosterRepository = rosterRepository;
            this.scoreRepository = scoreRepository;
        }

        public ICollection<RosterViewModel> GetAllUserRosters(string username)
        {
            var rosters = this.rosterRepository.All()
              .Where(x => x.User.UserName == username);

            var models = rosters.Select(x => this.mapper.Map<RosterViewModel>(x))
               .ToList();

            if (models != null)
            {
                foreach (var rosterVM in models)
                {
                    foreach (var playerVM in rosterVM.Players)
                    {
                        playerVM.Points = this.scoreRepository.All()
                            .FirstOrDefault(x => x.Fixture.MatchdayId == rosterVM.Matchday.Id &&
                                                 x.PlayerId == playerVM.PlayerId).GetScore();
                    }
                }

            }
            return models;
        }

        public RosterViewModel GetCurrentUserRoster(string username, Guid matchdayId)
        {
            var roster = this.rosterRepository.All()
                .FirstOrDefault(x => x.User.UserName == username &&
                                   x.MatchdayId == matchdayId);

            var model = this.mapper.Map<RosterViewModel>(roster);

            if (model != null)
            {
                foreach (var playerVM in model.Players)
                {
                    playerVM.Points = this.scoreRepository.All()
                        .FirstOrDefault(x => x.Fixture.MatchdayId == matchdayId &&
                                             x.PlayerId == playerVM.PlayerId).GetScore();
                }
            }
            return model;
        }
    }
}
