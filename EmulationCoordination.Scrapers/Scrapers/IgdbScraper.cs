using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public class IgdbScraper : IScraper
    {
        public string FriendlyName => "IGDB.com";

        public RomData GetAllData(RomData dataToFillOut)
        {
            dataToFillOut.ScrapedBy = FriendlyName;
            return dataToFillOut;
        }

        public List<RomData> Search(RomData dataToSearchFor)
        {
            return new List<RomData>() { dataToSearchFor };
        }
    }
}
