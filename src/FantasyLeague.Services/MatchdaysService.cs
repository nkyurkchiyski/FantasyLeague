using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Services
{
    public class MatchdaysService : BaseService, IMatchdaysService
    {
        private readonly IRepository<Matchday> matchdaysRepository;

        public MatchdaysService(
            IRepository<Matchday> matchdaysRepository,
            IMapper mapper) : base(mapper)
        {
            this.matchdaysRepository = matchdaysRepository;
        }

        public ICollection<T> All<T>()
        {
            var matchdays = this.matchdaysRepository.All().ToList();

            var models = matchdays.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public T GetMatchday<T>(Guid matchdayId)
        {
            if (matchdayId == null ||
                matchdayId == Guid.Empty)
            {
                return default(T);
            }

            var matchday = this.matchdaysRepository
                .GetByIdAsync(matchdayId)
                .GetAwaiter()
                .GetResult();

            if (matchday == null)
            {
                return default(T);
            }

            var model = this.mapper.Map<T>(matchday);

            return model;
        }

        public T GetCurrentMatchday<T>()
        {
            var currentMatchday = this.matchdaysRepository.All()
                .FirstOrDefault(x => x.MatchdayStatus == MatchdayStatus.Current);

            if (currentMatchday == null)
            {
                return default(T);
            }

            var model = this.mapper.Map<T>(currentMatchday);

            return model;
        }

        public async Task<Matchday> SetCurrentMatchdayAsync(int week, TransferWindowStatus transferWindowStatus)
        {
            if (week <= 0 || week > GlobalConstants.TotalMatchdays)
            {
                return null;
            }

            var matchdays = this.matchdaysRepository.All();

            var currentMatchday = matchdays
                .FirstOrDefault(x => x.Week == week);

            if (currentMatchday == null)
            {
                return null;
            }

            currentMatchday.MatchdayStatus = MatchdayStatus.Current;
            currentMatchday.TransferWindowStatus = transferWindowStatus;

            foreach (var m in matchdays)
            {
                if (m.Week < week)
                {
                    m.MatchdayStatus = MatchdayStatus.Past;
                    m.TransferWindowStatus = TransferWindowStatus.Closed;
                }
                else if (m.Week > week)
                {
                    m.MatchdayStatus = MatchdayStatus.Upcoming;
                    m.TransferWindowStatus = TransferWindowStatus.Closed;
                }
            }

            await this.matchdaysRepository.SaveChangesAsync();

            return currentMatchday;
        }
    }
}
