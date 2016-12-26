using EmulationCoordination.Roms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public interface IScraper
    {
        String FriendlyName { get; }
        List<RomData> Search(RomData dataToSearchFor);
        RomData GetAllData(RomData dataToFillOut);
    }
}
