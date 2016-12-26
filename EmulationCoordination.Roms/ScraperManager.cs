using EmulationCoordination.Roms.Scrapers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Roms
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

            ManualScraper manual = new ManualScraper();
            availableScrapers.Add(manual.FriendlyName, manual);
        }

        public List<String> GetAllScrapers()
        {
            return availableScrapers.Keys.ToList();
        }
    }
}
