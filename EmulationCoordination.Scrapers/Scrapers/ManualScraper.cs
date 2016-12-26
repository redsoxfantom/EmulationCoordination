using EmulationCoordination.Roms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public class ManualScraper : IScraper
    {
        public string FriendlyName => "Manual Entry";

        public RomData GetAllData(RomData dataToFillOut)
        {
            dataToFillOut.ScrapedBy = FriendlyName;
            dataToFillOut.IsUpToDate = true;
            return dataToFillOut;
        }

        public List<RomData> Search(RomData dataToSearchFor)
        {
            return new List<RomData>() { dataToSearchFor };
        }
    }
}
