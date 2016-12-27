using EmulationCoordination.Roms;
using EmulationCoordination.Scrapers.Scrapers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Scrapers
{
    public class ScraperManager
    {
        private static ScraperManager mInstance = null;
        private Dictionary<string, IScraper> availableScrapers;

        public static ScraperManager Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new ScraperManager();
                }
                return mInstance;
            }
        }

        private ScraperManager()
        {
            availableScrapers = new Dictionary<string, IScraper>();

            TheGamesDbScraper gamesDb = new TheGamesDbScraper();
            availableScrapers.Add(gamesDb.FriendlyName, gamesDb);
            IgdbScraper igdb = new IgdbScraper();
            availableScrapers.Add(igdb.FriendlyName, igdb);
        }

        public List<String> GetAllScrapers()
        {
            return availableScrapers.Keys.ToList();
        }

        public List<RomData> Search(RomData dataToSearchFor, String ScraperToUse)
        {
            if(!availableScrapers.ContainsKey(ScraperToUse))
            {
                return new List<RomData>() { dataToSearchFor };
            }
            else
            {
                return availableScrapers[ScraperToUse].Search(dataToSearchFor);
            }
        }

        public RomData GetAllData(RomData dataToSearchFor, String ScraperToUse)
        {
            if(!availableScrapers.ContainsKey(ScraperToUse))
            {
                return dataToSearchFor;
            }
            else
            {
                return availableScrapers[ScraperToUse].GetAllData(dataToSearchFor);
            }
        }
    }
}
