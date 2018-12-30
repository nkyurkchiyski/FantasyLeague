using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyLeague.Common.Constants;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Fixture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyLeague.Web.Areas.Rounds.Controllers
{
    [Area(ControllerConstants.RoundsAreaName)]
    [Authorize]
    public class FixturesController : Controller
    {
        private readonly IScoreService scoreService;
        private readonly IFixtureService fixtureService;

        public FixturesController(IScoreService scoreService, IFixtureService fixtureService)
        {
            this.scoreService = scoreService;
            this.fixtureService = fixtureService;
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> GenerateScores(Guid id)
        {
            var result = await this.fixtureService.GenerateScores(id);

            if (!result.Success)
            {
                return RedirectToAction(
                       ActionConstants.Error,
                       PagesConstants.Home,
                       new { area = "", errorMessage = result.Error });
            }

            return RedirectToAction(actionName: nameof(Details), routeValues: new { id });
        }

    }
}