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
                .First(x => x.MatchdayStatus == MatchdayStatus.Current);

            var model = this.mapper.Map<T>(currentMatchday);

            return model;
        }

        public async Task<IServiceResult> SetCurrentMatchday(int week)
        {
            var result = new ServiceResult { Success = false };

            var matchdays = this.matchdayRepository.All();

            var currentMatchday = matchdays.First(x => x.Week == week);

            if (currentMatchday == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.MatchdayName);
                return result;
            }

            currentMatchday.MatchdayStatus = MatchdayStatus.Current;

            foreach (var m in matchdays)
            {
                if (m.Week < week)
                {
                    m.MatchdayStatus = MatchdayStatus.Past;
                }
                else if (m.Week > week)
                {
                    m.MatchdayStatus = MatchdayStatus.Upcoming;
                }
            }

            await this.matchdayRepository.SaveChangesAsync();

            result.Success = true;

            return result;
        }

        public async Task<IServiceResult> SetTransferWindowStatus(string transferWindowStatus)
        {
            var result = new ServiceResult { Success = false };

            bool isValid = Enum.TryParse(transferWindowStatus, out TransferWindowStatus status);

            if (!isValid)
            {
                result.Error = string.Format(
                    ExceptionConstants.InvalidEnumException,
                    GlobalConstants.TransferWindowStatusName);
                return result;
            }

            var currentMatchday = this.matchdayRepository.All()
                .First(x => x.MatchdayStatus == MatchdayStatus.Current);

            if (currentMatchday == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.MatchdayName);
                return result;
            }

            currentMatchday.TransferWindowStatus = status;

            await this.matchdayRepository.SaveChangesAsync();

            result.Success = true;
            return result;
        }
    }
}
