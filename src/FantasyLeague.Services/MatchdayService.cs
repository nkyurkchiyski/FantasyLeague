using AutoMapper;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyLeague.Services
{
    public class MatchdayService : BaseService, IMatchdayService
    {
        private readonly IRepository<Matchday> matchdayRepository;
        
        public MatchdayService(
            IRepository<Matchday> matchdayRepository,
            IMapper mapper) : base(mapper)
        {
            this.matchdayRepository = matchdayRepository;
        }

        public ICollection<T> All<T>()
        {
            var matchdays = this.matchdayRepository.All();

            var models = matchdays.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public int Count()
        {
            int count = this.matchdayRepository.All().Count();

            return count;
        }

        public T Details<T>(Guid matchdayId)
        {
            var matchday = this.matchdayRepository.GetByIdAsync(matchdayId);

            var model = this.mapper.Map<T>(matchday);

            return model;
        }

        public T GetCurrentMatchday<T>()
        {
            var currentMatchday = this.matchdayRepository.All()
                .OrderBy(x => x.StartDate)
                .First(x => x.StartDate > DateTime.UtcNow);

            var model = this.mapper.Map<T>(currentMatchday);

            return model;
        }
    }
}
