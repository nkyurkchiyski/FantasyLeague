using FantasyLeague.Common.Constants;
using FantasyLeague.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Web.Areas.Club.Controllers
{
    [Area(ControllerConstants.ClubAreaName)]
    [Authorize]
    public class TransfersController : Controller
    {
        private readonly ITransfersService transfersService;

        public TransfersController(ITransfersService transfersService)
        {
            this.transfersService = transfersService;
        }

        public IActionResult Index()
        {
            var model = this.transfersService
                .GetTransfersRosterViewModel(this.User.Identity.Name);

            return View(model);
        }

        [HttpPost]
        public async Task<string> Create([FromBody] JObject model)
        {
            var playersIn = model.GetValue("PlayersIn").ToObject<string[]>();
            var playersOut = model.GetValue("PlayersOut").ToObject<string[]>();

            string path = $"/{ControllerConstants.ClubAreaName}/{PagesConstants.Rosters}/{ActionConstants.Index}";

            if (!playersIn.Any() || !playersOut.Any())
            {
                return path;
            }

            var result = await this.transfersService
                .TransferPlayersAsync(
                User.Identity.Name,
                playersIn, playersOut);
            
            if (!result.Succeeded)
            {
                path = $"/{PagesConstants.Home}/{ActionConstants.Error}/?{ExceptionConstants.ErrorMessage}={result.Error}";
            }

            return path;
        }
    }
}