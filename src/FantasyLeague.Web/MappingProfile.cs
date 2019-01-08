using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.Fixture;
using FantasyLeague.ViewModels.Index;
using FantasyLeague.ViewModels.Matchday;
using FantasyLeague.ViewModels.Player;
using FantasyLeague.ViewModels.Roster;
using FantasyLeague.ViewModels.Score;
using FantasyLeague.ViewModels.Team;
using FantasyLeague.ViewModels.User;
using System.Linq;

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
            CreateMap<Roster, RosterViewModel>()
                .ForMember(
                    f => f.Points,
                    opt => opt.Ignore())
                .ForMember(
                    f => f.Formation,
                    opt => opt.MapFrom(src => src.Formation))
                .ForMember(
                    f => f.Players,
                    opt => opt.MapFrom(src => src.Players))
                .ForMember(
                    f => f.Matchday,
                    opt => opt.MapFrom(src => src.Matchday));


            CreateMap<RosterPlayer, RosterPlayerViewModel>()
                .ForMember(
                    f => f.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    f => f.PlayerId,
                    opt => opt.MapFrom(src => src.PlayerId))
                .ForMember(
                    f => f.RosterId,
                    opt => opt.MapFrom(src => src.RosterId))
                .ForMember(
                    f => f.Selected,
                    opt => opt.MapFrom(src => src.Selected))
                .ForMember(
                    f => f.Name,
                    opt => opt.MapFrom(src => src.Player.Name))
                .ForMember(
                    f => f.Image,
                    opt => opt.MapFrom(src => src.Player.Image == null ?
                                              GlobalConstants.TemplatePlayerImageUrl : src.Player.Image.Url))
                .ForMember(
                    f => f.Position,
                    opt => opt.MapFrom(src => src.Player.Position));

        }

        private void CreateUserMappings()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(
                    f => f.Roster,
                    opt => opt.MapFrom(src => src.Rosters
                    .FirstOrDefault(x => x.Matchday.MatchdayStatus == MatchdayStatus.Current)));

            CreateMap<User, UserInfoViewModel>()
                .ForMember(
                    f => f.Username,
                    opt => opt.MapFrom(src => src.UserName))
                .ForMember(
                    f => f.Supports,
                    opt => opt.MapFrom(src => src.FavouriteTeam == null ?
                    GlobalConstants.Unknown : src.FavouriteTeam.Name));
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
                   opt => opt.MapFrom(src => src.Image.Url));

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
                    opt => opt.MapFrom(src => src.Scores.Where(x =>
                    x.Fixture.Matchday.MatchdayStatus < MatchdayStatus.Upcoming)
                                                 .Sum(x => x.Goals)))
                .ForMember(
                    f => f.Assists,
                    opt => opt.MapFrom(src => src.Scores.Where(x => 
                    x.Fixture.Matchday.MatchdayStatus < MatchdayStatus.Upcoming)
                                                 .Sum(x => x.Assists)))
                .ForMember(
                    f => f.Appearances,
                    opt => opt.MapFrom(src => src.Scores.Where(x =>
                    x.Fixture.Matchday.MatchdayStatus < MatchdayStatus.Upcoming).Count()))
                .ForMember(
                    f => f.TotalPoints,
                    opt => opt.MapFrom(src => src.Scores.Where(x => 
                    x.Fixture.Matchday.MatchdayStatus < MatchdayStatus.Upcoming)
                                                 .Sum(x => x.GetScore())))
                .ForMember(
                    f => f.PlayerImage,
                    opt => opt.MapFrom(src => src.Image == null ? GlobalConstants.TemplatePlayerImageUrl : src.Image.Url))
                .ForMember(
                    f => f.TeamName,
                    opt => opt.MapFrom(src => src.Team.Name));
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
                    opt => opt.MapFrom(src => src.MatchdayStatus));

            CreateMap<Matchday, IndexViewModel>()
                .ForMember(
                    f => f.MarchdayId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    f => f.MarchdayWeek,
                    opt => opt.MapFrom(src => src.Week))
                .ForMember(
                    f => f.User,
                    opt => opt.Ignore());
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
                    opt => opt.MapFrom(src => src.Player.Image == null ?
                    GlobalConstants.TemplatePlayerImageUrl : src.Player.Image.Url))
                .ForMember(
                    f => f.TeamId,
                    opt => opt.MapFrom(src => src.Player.TeamId))
                 .ForMember(
                    f => f.Position,
                    opt => opt.MapFrom(src => src.Player.Position));
        }
    }
}
