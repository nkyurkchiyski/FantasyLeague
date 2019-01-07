using FantasyLeague.Common.Constants;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FantasyLeague.Web.Areas.Leaderboard.Controllers
{
    [Area(ControllerConstants.LeaderboardAreaName)]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var leaderboard = this.usersService
                .GetLeaderboard<UserInfoViewModel>()
                .OrderByDescending(x=>x.TotalPoints)
                .ToList();

            return View(leaderboard);
        }
    }
}