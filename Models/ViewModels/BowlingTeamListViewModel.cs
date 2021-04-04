using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nefis413_Assignment10.Models.ViewModels
{
    public class BowlingTeamListViewModel
    {
        //model contains teams and paging info
        public IEnumerable<Bowlers> bowlers { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string TeamName { get; set; }
    }
}
