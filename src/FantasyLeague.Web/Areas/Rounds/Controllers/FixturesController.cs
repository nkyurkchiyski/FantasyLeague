using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyLeague.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FantasyLeague.Web.Areas.Rounds.Controllers
{
    [Area(ControllerConstants.RoundsAreaName)]
    public class FixturesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}