using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyLeague.Common.Constants;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Player;
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

        public RostersController(
            IPlayersService playersService,
            IRostersService rostersService)
        {
            this.playersService = playersService;
            this.rostersService = rostersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var players = this.playersService
                .All<PlayerStatsViewModel>().ToList();

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