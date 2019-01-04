using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyLeague.Common.Constants;
using FantasyLeague.Common.Pagination;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyLeague.Web.Areas.Competition.Controllers
{
    [Area(ControllerConstants.CompetitionAreaName)]
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        public IActionResult All(int? pageIndex)
        {
            var teams = this.teamsService
                .All<TeamImageViewModel>();

            var paginatedMatchdays = PaginatedList<TeamImageViewModel>.Create(
                teams,
                pageIndex ?? 1,
                GlobalConstants.MatchdaysPageSize);

            return View(paginatedMatchdays);
        }

        public IActionResult Details(int? pageIndex, Guid id)
        {
            var team = this.teamsService
                .GetTeamById<TeamPlayersViewModel>(id);
            
            return View(team);
        }
    }
}