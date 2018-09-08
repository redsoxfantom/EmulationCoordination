using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using EmulationCoordination.Scrapers.DataContracts;
using EmulationCoordination.Utilities;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public class MobyGamesScraper : BaseScraper
    {
        public override string FriendlyName => "www.mobygames.com";

        private string apiKey = "jFoT7uaw5xGDbdqRBtT8Zg==";
        private string rootUrl = "https://api.mobygames.com/v1/games?";
        private string searchUrl = "api_key={0}&format=brief&title={1}&platform={2}";

        protected override RomData ScraperSpecificGetAllData(RomData dataToFillOut)
        {
            throw new NotImplementedException();
        }

        protected override List<RomData> ScraperSpecificSearch(RomData dataToSearchFor)
        {
            List<RomData> returnList = new List<RomData>();
            string nameTerm = GenerateSearchableName(dataToSearchFor);
            string searchTerm = String.Format(searchUrl, apiKey, nameTerm,ConvertConsole(dataToSearchFor.Console));
            string finalUrl = String.Format("{0}{1}", rootUrl, searchTerm);

            string results = MakeTextRequest(finalUrl);
            if(!String.IsNullOrEmpty(results))
            {
                MobyGamesSearchResults searchResults = SerializationUtilities.DeserializeString<MobyGamesSearchResults>(results, DataFormat.JSON);
                if (searchResults != null)
                {
                    foreach (var game in searchResults.games)
                    {
                        RomData convertedData = dataToSearchFor.Clone();
                        convertedData.FriendlyName = game.title;
                        convertedData.ScraperUniqueKey = game.game_id.ToString();
                        returnList.Add(convertedData);
                    }
                }
            }

            return returnList;
        }

        private int ConvertConsole(EmulatorConsoles console)
        {
            if(console.Equals(EmulatorConsoles.GAMECUBE))
            {
                return 14;
            }
            if (console.Equals(EmulatorConsoles.GAME_BOY))
            {
                return 10;
            }
            if (console.Equals(EmulatorConsoles.GAME_BOY_ADVANCE))
            {
                return 12;
            }
            if (console.Equals(EmulatorConsoles.GAME_BOY_COLOR))
            {
                return 11;
            }
            if (console.Equals(EmulatorConsoles.MASTER_SYSTEM))
            {
                return 26;
            }
            if (console.Equals(EmulatorConsoles.NES))
            {
                return 22;
            }
            if (console.Equals(EmulatorConsoles.NINTENDO_64))
            {
                return 9;
            }
            if (console.Equals(EmulatorConsoles.NINTENDO_WII))
            {
                return 82;
            }
            if (console.Equals(EmulatorConsoles.PLAYSTATION_2))
            {
                return 7;
            }
            if (console.Equals(EmulatorConsoles.PLAYSTATION_3))
            {
                return 81;
            }
            if (console.Equals(EmulatorConsoles.GAMECUBE))
            {
                return 15;
            }
            return 204;
        }
    }
}
