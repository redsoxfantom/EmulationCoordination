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
        public override string FriendlyName => "IGDB.com";

        protected override RomData ScraperSpecificGetAllData(RomData dataToFillOut)
        {
            throw new NotImplementedException();
        }

        protected override List<RomData> ScraperSpecificSearch(RomData dataToSearchFor)
        {
            throw new NotImplementedException();
        }
    }
}
