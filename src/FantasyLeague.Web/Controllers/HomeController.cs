using FantasyLeague.Common.Constants;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Index;
using FantasyLeague.ViewModels.User;
using FantasyLeague.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

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
            var indexViewModel = this.matchdaysService
                .GetCurrentMatchday<IndexViewModel>();
            
            var user = this.usersService
                .GetUser<UserViewModel>(User.Identity.Name);

            if (user != null)
            {
                var currentRoster = this.rostersService
                    .GetCurrentUserRoster(User.Identity.Name, indexViewModel.MarchdayId);

                user.Roster = currentRoster;
                indexViewModel.User = user;
            }
            return View(indexViewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.AdminRoleName)]
        public async Task<IActionResult> SetCurrentMatchday(IndexViewModel model)
        {
            var matchday = await this.matchdaysService
                .SetCurrentMatchday(model.MarchdayWeek, model.TransferWindowStatus);

            var result = await this.rostersService
                .SetCurrentRosters(matchday.Id);

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
