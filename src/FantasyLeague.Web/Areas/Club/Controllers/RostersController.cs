using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyLeague.Common.Constants;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Matchday;
using FantasyLeague.ViewModels.Player;
using FantasyLeague.ViewModels.Roster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyLeague.Web.Areas.Club.Controllers
{
    [Area(ControllerConstants.ClubAreaName)]
    [Authorize]
    public class RostersController : Controller
    {
        private readonly IPlayersService playersService;
        private readonly IRostersService rostersService;
        private readonly IMatchdaysService matchdaysService;
        private readonly IUsersService usersService;

        public RostersController(
            IPlayersService playersService,
            IRostersService rostersService,
            IMatchdaysService matchdaysService,
            IUsersService usersService)
        {
            this.playersService = playersService;
            this.rostersService = rostersService;
            this.matchdaysService = matchdaysService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var currentMatchday = this.matchdaysService
               .GetCurrentMatchday<MatchdayViewModel>();

            var currentRoster = this.rostersService
               .GetCurrentUserRoster(User.Identity.Name, currentMatchday.Id);

            var rostersWeeks = this.rostersService
              .GetAllUserRosters(User.Identity.Name)
              .Where(x => x.Matchday.Week <= currentMatchday.Week)
              .Select(x => x.Matchday.Week)
              .OrderBy(x => x)
              .ToArray();

            var user = this.usersService
                .GetUser<User>(User.Identity.Name);

            var indexRosterViewModel = new UserRosterViewModel
            {
                MarchdayWeek = currentMatchday.Week,
                RostersWeeks = rostersWeeks,
                TransferWindowStatus = currentMatchday.TransferWindowStatus,
                ClubName = user.ClubName,
                Roster = currentRoster
            };

            return View(indexRosterViewModel);
        }

        public IActionResult Details(UserRosterViewModel model)
        {
            var rosters = this.rostersService
                      .GetAllUserRosters(User.Identity.Name);

            var rostersWeeks = rosters.Select(x => x.Matchday.Week)
                                      .OrderBy(x => x).ToArray();

            var roster = rosters.FirstOrDefault(x => x.Matchday.Week == model.MarchdayWeek);

            var user = this.usersService
                .GetUser<User>(User.Identity.Name);

            var indexRosterViewModel = new UserRosterViewModel
            {
                MarchdayWeek = model.MarchdayWeek,
                RostersWeeks = rostersWeeks,
                ClubName = user.ClubName,
                Roster = roster
            };

            return View(indexRosterViewModel);
        }

        public async Task<IActionResult> Edit(UserRosterViewModel model)
        {
            var formation = model.Roster.Formation;

            var matchday = this.matchdaysService
                .GetCurrentMatchday<Matchday>();

            var roster = this.rostersService
                .GetCurrentUserRoster(User.Identity.Name, matchday.Id);

            var result = await this.rostersService
                .SetNewFormation(formation, roster.Id);

            if (!result.Succeeded)
            {
                return RedirectToAction(
                       ActionConstants.Error,
                       PagesConstants.Home,
                       new { area = "", errorMessage = result.Error });
            }

            var editRosterModel = new EditRosterViewModel
            {
                Formation = formation,
                Matchday = roster.Matchday,
                Goalkeepers = roster.Players.Where(x => x.Position == PlayerPosition.Goalkeeper).ToList(),
                Attackers = roster.Players.Where(x => x.Position == PlayerPosition.Attacker).ToList(),
                Midfielders = roster.Players.Where(x => x.Position == PlayerPosition.Midfielder).ToList(),
                Defenders = roster.Players.Where(x => x.Position == PlayerPosition.Defender).ToList()
            };
            return View(editRosterModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRosterViewModel editRosterModel)
        {
            var formation = editRosterModel.Formation;

            var players = editRosterModel.Goalkeepers
                .Concat(editRosterModel.Defenders)
                .Concat(editRosterModel.Midfielders)
                .Concat(editRosterModel.Attackers)
                .ToList();

            var rosterId = players.First().RosterId;

            var result = await this.rostersService.Edit(players);

            if (!result.Succeeded)
            {
                return RedirectToAction(
                       ActionConstants.Error,
                       PagesConstants.Home,
                       new { area = "", errorMessage = result.Error });
            }

            return RedirectToAction(
                      ActionConstants.Index,
                      PagesConstants.Rosters);
        }

        public IActionResult Create()
        {
            var players = this.playersService
                .All<PlayerStatsViewModel>()
                .Where(x => x.Active)
                .ToList();

            return View(players);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string[] ids)
        {
            var result = await this.rostersService.Create(User.Identity.Name, ids);

            return Ok();
        }
    }
}