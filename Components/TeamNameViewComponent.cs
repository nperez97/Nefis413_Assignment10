using Microsoft.AspNetCore.Mvc;
using Nefis413_Assignment10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nefis413_Assignment10.Components
{
    public class TeamNameViewComponent : ViewComponent

    {
        private BowlingLeagueContext context;

        //Constructor
        public TeamNameViewComponent(BowlingLeagueContext ctx)
        {
            context = ctx;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["teamname"];

            //sets up data for our view component
            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
