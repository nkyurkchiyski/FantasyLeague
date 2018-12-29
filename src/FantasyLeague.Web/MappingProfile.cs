using System;
using AutoMapper;
using FantasyLeague.Models;
using FantasyLeague.ViewModels.Fixture;
using FantasyLeague.ViewModels.Matchday;
using FantasyLeague.ViewModels.Score;

namespace FantasyLeague.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateScoreMappings();
            this.CreateMatchdayMappings();
            this.CreateFixtureMappings();
        }

        private void CreateFixtureMappings()
        {
            CreateMap<Fixture, FixtureViewModel>()
                .ForMember(
                    f => f.HomeTeam,
                    opt => opt.MapFrom(src => src.HomeTeam.Name))
                .ForMember(
                    f => f.AwayTeam,
                    opt => opt.MapFrom(src => src.AwayTeam.Name))
                .ForMember(
                    f => f.HomeTeamGoals,
                    opt => opt.MapFrom(src => src.HomeTeamGoals.GetValueOrDefault(0)))
                .ForMember(
                    f => f.AwayTeamGoals,
                    opt => opt.MapFrom(src => src.AwayTeamGoals.GetValueOrDefault(0))); 
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
            CreateMap<Score, ScoreViewModel>()
                .ReverseMap();
        }
    }
}
