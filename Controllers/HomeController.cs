using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nefis413_Assignment10.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Nefis413_Assignment10.Models.ViewModels;

namespace Nefis413_Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        //making variable to indicate how many books to display for pagination
        public int PageSize = 5;

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        //could add in Index(string bowlerSearch)
        //[HttpGet]
        public IActionResult Index(long? teamnameid, string teamname, int pageNum = 1)
        {
            //var passedItem = "%David%";

            ViewData["Category"] = "All";

            if(teamname != null)
            {
                ViewData["Category"] = teamname;
            }

            return View(new BowlingTeamListViewModel
            {   
                bowlers = context.Bowlers
                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE BowlerFirstName LIKE {passedItem}") //this filtering occurs on set itself.
                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamnameid} OR {teamnameid} IS NULL")
                .Where(p => teamnameid == null || p.TeamId == teamnameid)
                .OrderBy(p => p.BowlerLastName) // orderby is written in language called linq, which helps you query the database
                .Skip((pageNum - 1) * PageSize)
                .Take(PageSize)
                .ToList()
                ,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = PageSize,
                    //if teamnameid is null then select total count, else only count where team name id matches if selected
                    TotalNumItems = teamnameid == null ? context.Bowlers.Count() : 
                    context.Bowlers.Where(x => x.TeamId == teamnameid).Count()
                    
                },
                TeamName = teamname //this is passed in from the default page in our tag helpers which is receieved in the controller
        });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
