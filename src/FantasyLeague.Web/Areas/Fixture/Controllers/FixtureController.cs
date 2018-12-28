using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyLeague.Common.Constants;
using FantasyLeague.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FantasyLeague.Web.Areas.Fixture.Controllers
{
    [Area(GlobalConstants.FixtureName)]
    public class FixtureController : Controller
    {
        private readonly IFixtureService fixtureService;

        public FixtureController(IFixtureService fixtureService)
        {
            this.fixtureService = fixtureService;
        }

        public IActionResult Details(Guid fixtureId)
        {
            return View();
        }
    }
}