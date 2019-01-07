using FantasyLeague.Common.Constants;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Player;
using FantasyLeague.ViewModels.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Web.Areas.Competition.Controllers
{
    [Area(ControllerConstants.CompetitionAreaName)]
    [Authorize]
    public class PlayersController : Controller
    {
        private readonly IPlayersService playersService;
        private readonly ITeamsService teamsService;

        public PlayersController(
            IPlayersService playersService,
            ITeamsService teamsService)
        {
            this.playersService = playersService;
            this.teamsService = teamsService;
        }

        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> Archive(Guid playerId, Guid teamId)
        {
            var result = await this.playersService
                .ArchiveAsync(playerId);

            if (!result.Succeeded)
            {
                return RedirectToAction(
                       ActionConstants.Error,
                       PagesConstants.Home,
                       new { area = "", errorMessage = result.Error });
            }

            return RedirectToAction(
                ActionConstants.Details,
                PagesConstants.Teams,
                new { id = teamId });
        }

        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> Restore(Guid playerId, Guid teamId)
        {
            var result = await this.playersService
                .RestoreAsync(playerId);

            if (!result.Succeeded)
            {
                return RedirectToAction(
                       ActionConstants.Error,
                       PagesConstants.Home,
                       new { area = "", errorMessage = result.Error });
            }

            return RedirectToAction(
                ActionConstants.Details,
                PagesConstants.Teams,
                new { id = teamId });
        }

        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public IActionResult Create(Guid teamId)
        {
            var model = new PlayerDetailedViewModel { TeamId = teamId };
            var allTeams = this.teamsService.All<TeamViewModel>();

            List<SelectListItem> allTeamsList = new List<SelectListItem>();
            foreach (var team in allTeams)
            {
                allTeamsList.Add(new SelectListItem { Value = team.Id.ToString(), Text = team.Name });
            }

            this.ViewData[PagesConstants.Teams] = allTeamsList;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> Create(PlayerDetailedViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.playersService.CreateAsync(model);

                if (!result.Succeeded)
                {
                    return RedirectToAction(
                           ActionConstants.Error,
                           PagesConstants.Home,
                           new { area = "", errorMessage = result.Error });
                }

                return RedirectToAction(
                    ActionConstants.Details,
                    PagesConstants.Teams,
                    new { id = model.TeamId });
            }

            var allTeams = this.teamsService.All<TeamViewModel>();

            List<SelectListItem> allTeamsList = new List<SelectListItem>();
            foreach (var team in allTeams)
            {
                allTeamsList.Add(new SelectListItem { Value = team.Id.ToString(), Text = team.Name });
            }

            this.ViewData[PagesConstants.Teams] = allTeamsList;

            return View(model);
        }

        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await this.playersService
                .GetPlayerAsync<PlayerDetailedViewModel>(id);

            var allTeams = this.teamsService
                .All<TeamViewModel>();
            
            List<SelectListItem> allTeamsList = new List<SelectListItem>();
            foreach (var team in allTeams)
            {
                allTeamsList.Add(new SelectListItem { Value = team.Id.ToString(), Text = team.Name });
            }

            this.ViewData[PagesConstants.Teams] = allTeamsList;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> Edit(PlayerDetailedViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.playersService.EditAsync(model);

                if (!result.Succeeded)
                {
                    return RedirectToAction(
                           ActionConstants.Error,
                           PagesConstants.Home,
                           new { area = "", errorMessage = result.Error });
                }

                return RedirectToAction(
                    ActionConstants.Details,
                    PagesConstants.Teams,
                    new { id = model.TeamId });
            }

            var allTeams = this.teamsService.All<TeamViewModel>();

            List<SelectListItem> allTeamsList = new List<SelectListItem>();
            foreach (var team in allTeams)
            {
                allTeamsList.Add(new SelectListItem { Value = team.Id.ToString(), Text = team.Name });
            }

            this.ViewData[PagesConstants.Teams] = allTeamsList;

            return View(model);
        }
    }
}