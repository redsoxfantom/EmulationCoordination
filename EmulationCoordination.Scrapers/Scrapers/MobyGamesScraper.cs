using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public class MobyGamesScraper : BaseScraper
    {
        public override string FriendlyName => "www.mobygames.com";

        private string apiKey = "jFoT7uaw5xGDbdqRBtT8Zg==";
        private string rootUrl = "https://api.mobygames.com/v1/games?";
        private string searchUrl = "api_key={0}&format=brief&title={1}";

        protected override RomData ScraperSpecificGetAllData(RomData dataToFillOut)
        {
            throw new NotImplementedException();
        }

        protected override List<RomData> ScraperSpecificSearch(RomData dataToSearchFor)
        {
            List<RomData> returnList = new List<RomData>();
            string nameTerm = GenerateSearchableName(dataToSearchFor);
            string searchTerm = String.Format(searchUrl, apiKey, nameTerm);
            string finalUrl = String.Format("{0}{1}", rootUrl, searchTerm);

            throw new NotImplementedException();
        }
    }
}
