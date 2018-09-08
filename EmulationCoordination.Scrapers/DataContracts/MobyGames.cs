using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Scrapers.DataContracts
{
    public class MobyGamesSearchResults
    {
        public MobyGamesSearchResult[] games { get; set; }
    }

    public class MobyGamesSearchResult
    {
        public int game_id { get; set; }
        public string title { get; set; }
    }
}
