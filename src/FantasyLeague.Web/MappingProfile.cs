using System;
using System.Linq;
using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Models;
using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.Fixture;
using FantasyLeague.ViewModels.Matchday;
using FantasyLeague.ViewModels.Player;
using FantasyLeague.ViewModels.Roster;
using FantasyLeague.ViewModels.Score;
using FantasyLeague.ViewModels.Team;
using FantasyLeague.ViewModels.User;

namespace FantasyLeague.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateRosterMappings();
            this.CreateScoreMappings();
            this.CreateMatchdayMappings();
            this.CreateFixtureMappings();
            this.CreatePlayerMappings();
            this.CreateTeamMappings();
            this.CreateUserMappings();
        }

        private void CreateRosterMappings()
        {
            CreateMap<Roster, RosterViewModel>();

            CreateMap<RosterPlayer, RosterPlayerViewModel>()
                .ForMember(
                    f => f.PlayerName,
                    opt => opt.MapFrom(src => src.Player.Name))
                .ForMember(
                    f => f.PlayerImage,
                    opt => opt.MapFrom(src => src.Player.PlayerImage.Url));

        }

        private void CreateUserMappings()
        {
            CreateMap<User, UserViewModel>();
        }

        private void CreateTeamMappings()
        {
            CreateMap<Team, TeamViewModel>()
                .ForMember(
                    f => f.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    f => f.Id,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<Team, TeamImageViewModel>()
               .ForPath(
                   f => f.TeamImage,
                   opt => opt.MapFrom(src => src.TeamImage.Url));

            CreateMap<Team, TeamPlayersViewModel>();
        }

        private void CreateFixtureMappings()
        {
            CreateMap<Fixture, FixtureViewModel>()
                .ForMember(
                    f => f.HomeTeamName,
                    opt => opt.MapFrom(src => src.HomeTeam.Name))
                .ForMember(
                    f => f.AwayTeamName,
                    opt => opt.MapFrom(src => src.AwayTeam.Name))
                .ForMember(
                    f => f.HomeTeamGoals,
                    opt => opt.MapFrom(src => src.HomeTeamGoals.GetValueOrDefault(0)))
                .ForMember(
                    f => f.AwayTeamGoals,
                    opt => opt.MapFrom(src => src.AwayTeamGoals.GetValueOrDefault(0)))
                .ForMember(
                    f => f.ScoresAdded,
                    opt => opt.MapFrom(src => src.Scores.Any()));

            CreateMap<Fixture, FixtureStatsViewModel>()
                .ForMember(
                    f => f.Scores,
                    opt => opt.MapFrom(src => src.Scores))
                .ForMember(
                    f => f.MatchdayId,
                    opt => opt.MapFrom(src => src.MatchdayId));

        }

        private void CreatePlayerMappings()
        {
            CreateMap<Player, PlayerViewModel>();

            CreateMap<Player, PlayerDetailedViewModel>()
                .ForPath(e => e.Image, opt => opt.Ignore());

            CreateMap<Player, PlayerStatsViewModel>()
                .ForMember(
                    f => f.Goals,
                    opt => opt.MapFrom(src => src.Scores
                                                 .Where(x => x.Fixture.Matchday.MatchdayStatus < MatchdayStatus.Upcoming)
                                                 .Sum(x => x.Goals)))
                .ForMember(
                    f => f.Assists,
                    opt => opt.MapFrom(src => src.Scores
                                                 .Where(x => x.Fixture.Matchday.MatchdayStatus < MatchdayStatus.Upcoming)
                                                 .Sum(x => x.Assists)))
                .ForMember(
                    f => f.TotalPoints,
                    opt => opt.MapFrom(src => src.Scores
                                                 .Where(x => x.Fixture.Matchday.MatchdayStatus < MatchdayStatus.Upcoming)
                                                 .Sum(x => x.GetScore())))
                .ForMember(
                    f => f.PlayerImage,
                    opt => opt.MapFrom(src => src.PlayerImage == null ? GlobalConstants.TemplatePlayerImageUrl : src.PlayerImage.Url));
        }

        private void CreateMatchdayMappings()
        {
            CreateMap<Matchday, MatchdayViewModel>()
                .ForMember(
                    f => f.Status,
                    opt => opt.MapFrom(src => src.MatchdayStatus.ToString()));

            CreateMap<Matchday, MatchdayDetailsViewModel>()
                .ForMember(
                    f => f.Status,
                    opt => opt.MapFrom(src => src.MatchdayStatus.ToString()));

            CreateMap<Matchday, MatchdayEditViewModel>()
                .ForMember(
                    f => f.Status,
                    opt => opt.MapFrom(src => src.MatchdayStatus.ToString()));
        }

        private void CreateScoreMappings()
        {
            CreateMap<Score, ScoreViewModel>();

            CreateMap<Score, ScorePlayerViewModel>()
                .ForMember(
                    f => f.PlayerName,
                    opt => opt.MapFrom(src => src.Player.Name))
                .ForMember(
                    f => f.PlayerImage,
                    opt => opt.MapFrom(src => src.Player.PlayerImage == null ? 
                    GlobalConstants.TemplatePlayerImageUrl : src.Player.PlayerImage.Url))
                .ForMember(
                    f => f.TeamId,
                    opt => opt.MapFrom(src => src.Player.TeamId))
                 .ForMember(
                    f => f.Position,
                    opt => opt.MapFrom(src => src.Player.Position));
        }
    }
}
