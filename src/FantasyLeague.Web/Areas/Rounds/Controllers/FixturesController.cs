using System;
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
        private readonly IFixturesService fixtureService;

        public FixturesController(IFixturesService fixtureService)
        {
            this.fixtureService = fixtureService;
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var stats = this.fixtureService
                .GetFixture<FixtureStatsViewModel>(id);
            
            return View(stats);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> GenerateScores(Guid id)
        {
            var result = await this.fixtureService.GenerateScores(id);

            if (!result.Succeeded)
            {
                return RedirectToAction(
                       ActionConstants.Error,
                       PagesConstants.Home,
                       new { area = "", errorMessage = result.Error });
            }

            return RedirectToAction(
                controllerName: PagesConstants.Matchdays,
                actionName: nameof(Details),
                routeValues: new { id });
        }

    }
}