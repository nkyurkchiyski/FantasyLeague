﻿using FantasyLeague.Common.Constants;
using FantasyLeague.Common.Pagination;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Matchday;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Web.Areas.Rounds.Controllers
{
    [Area(ControllerConstants.RoundsAreaName)]
    [Authorize]
    public class MatchdaysController : Controller
    {
        private readonly IMatchdaysService matchdayService;
        private readonly IFixturesService fixtureService;

        public MatchdaysController(
            IMatchdaysService matchdayService,
            IFixturesService fixtureService)
        {
            this.matchdayService = matchdayService;
            this.fixtureService = fixtureService;
        }

        public IActionResult All(int? pageIndex)
        {
            var matchdays = this.matchdayService
                .All<MatchdayViewModel>()
                .OrderBy(x => x.Week)
                .ToList();

            var paginatedMatchdays = PaginatedList<MatchdayViewModel>.Create(
                matchdays,
                pageIndex ?? 1,
                GlobalConstants.MatchdaysPageSize);

            return View(paginatedMatchdays);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var matchday = this.matchdayService
                .GetMatchday<MatchdayDetailsViewModel>(id);

            return View(matchday);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public IActionResult Edit(Guid id)
        {
            var matchday = this.matchdayService
                .GetMatchday<MatchdayEditViewModel>(id);

            return View(matchday);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> Edit(MatchdayEditViewModel matchday)
        {
            foreach (var fixture in matchday.Fixtures)
            {
                var result = await this.fixtureService.EditAsync(
                    fixture.Id,
                    fixture.Date,
                    fixture.Status,
                    fixture.HomeTeamGoals,
                    fixture.AwayTeamGoals);

                if (!result.Succeeded)
                {
                    return RedirectToAction(
                        ActionConstants.Error,
                        PagesConstants.Home,
                        new { area = "", errorMessage = result.Error });
                }
            }

            return RedirectToAction(nameof(Details), new { id = matchday.Id });
        }
    }
}