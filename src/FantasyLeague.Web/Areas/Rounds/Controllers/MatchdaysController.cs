using System.Linq;
using FantasyLeague.Common.Constants;
using FantasyLeague.Common.Pagination;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Matchday;
using Microsoft.AspNetCore.Mvc;

namespace FantasyLeague.Web.Areas.Rounds.Controllers
{
    [Area(ControllerConstants.RoundsAreaName)]
    public class MatchdaysController : Controller
    {
        private readonly IMatchdayService matchdayService;

        public MatchdaysController(IMatchdayService matchdayService)
        {
            this.matchdayService = matchdayService;
        }

        public IActionResult All(int? pageIndex)
        {
            var matchdays = this.matchdayService
                .All<MatchdayViewModel>()
                .OrderBy(x=>x.Week)
                .ToList();
            
            var paginatedMatchdays = PaginatedList<MatchdayViewModel>.Create(
                matchdays,
                pageIndex ?? 1,
                GlobalConstants.MatchdaysPageSize);

            return View(paginatedMatchdays);
        }
    }
}