﻿using AutoMapper;
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
            var matchdays = this.matchdaysRepository.All();

            var models = matchdays.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public T GetMatchday<T>(Guid matchdayId)
        {
            var matchday = this.matchdaysRepository
                .GetByIdAsync(matchdayId)
                .GetAwaiter()
                .GetResult();

            var model = this.mapper.Map<T>(matchday);

            return model;
        }

        public T GetCurrentMatchday<T>()
        {
            var currentMatchday = this.matchdaysRepository.All()
                .First(x => x.MatchdayStatus == MatchdayStatus.Current);

            var model = this.mapper.Map<T>(currentMatchday);

            return model;
        }

        public async Task<Matchday> SetCurrentMatchday(int week, TransferWindowStatus transferWindowStatus)
        {
            var matchdays = this.matchdaysRepository.All();

            var currentMatchday = matchdays.First(x => x.Week == week);

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
