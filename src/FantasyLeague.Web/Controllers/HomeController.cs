using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FantasyLeague.Web.Models;
using FantasyLeague.ViewModels.Index;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Matchday;
using FantasyLeague.ViewModels.Roster;
using FantasyLeague.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using FantasyLeague.ViewModels.User;

namespace FantasyLeague.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchdaysService matchdaysService;
        private readonly IUsersService usersService;
        private readonly IRostersService rostersService;


        public HomeController(
            IMatchdaysService matchdaysService,
            IUsersService usersService,
            IRostersService rostersService)
        {
            this.matchdaysService = matchdaysService;
            this.usersService = usersService;
            this.rostersService = rostersService;
        }

        public IActionResult Index()
        {
            var currentMatchday = this.matchdaysService
                .GetCurrentMatchday<MatchdayViewModel>();

            var indexViewModel = new IndexViewModel
            {
                MarchdayWeek = currentMatchday.Week,
                TransferWindowStatus = currentMatchday.TransferWindowStatus,
                User = new UserViewModel()
            };

            var currentRoster = this.rostersService
                .GetCurrentUserRoster(User.Identity.Name, currentMatchday.Id);

            var allRosters = this.rostersService
                .GetAllUserRosters(User.Identity.Name);

            if (currentRoster != null && allRosters != null)
            {
                indexViewModel.User.TotalPoints = allRosters.Sum(x => x.Points);
                indexViewModel.User.CurrentPoints = currentRoster.Points;
                indexViewModel.User.Roster = currentRoster;
            }

            return View(indexViewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> SetCurrentMatchday(IndexViewModel model)
        {
            var result = await this.matchdaysService
                .SetCurrentMatchday(model.MarchdayWeek, model.TransferWindowStatus);

            if (!result.Succeeded)
            {
                return RedirectToAction(
                       ActionConstants.Error,
                       PagesConstants.Home,
                       new { area = "", errorMessage = result.Error });
            }

            return RedirectToAction(controllerName: PagesConstants.Home, actionName: ActionConstants.Index);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorMessage)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = errorMessage });
        }
    }
}
