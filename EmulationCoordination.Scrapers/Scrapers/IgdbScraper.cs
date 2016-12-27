using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public class IgdbScraper : BaseScraper
    {
        private string searchRootUrl = "https://igdbcom-internet-game-database-v1.p.mashape.com/games/";
        private string searchFormat = "?fields=name%2Cid%2Crelease_dates&limit=10&offset=0&search={0}";
        private string apiKey = "WAV35CMfrrmshHzymVUYTbw3Sz7sp1AjcD0jsnjooQzM79mbIk";

        public override string FriendlyName => "IGDB.com";

        protected override RomData ScraperSpecificGetAllData(RomData dataToFillOut)
        {
            return dataToFillOut;
        }

        protected override List<RomData> ScraperSpecificSearch(RomData dataToSearchFor)
        {
            return null;
        }
    }
}
