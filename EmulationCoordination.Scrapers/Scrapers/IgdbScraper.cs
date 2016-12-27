using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using EmulationCoordination.Scrapers.DataContracts.Igdb;
using EmulationCoordination.Utilities;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public class IgdbScraper : BaseScraper
    {
        private string searchRootUrl = "https://igdbcom-internet-game-database-v1.p.mashape.com/games/";
        private string searchFormat = "?fields=name%2Cid%2Crelease_dates&limit=10&offset=0&search={0}";
        private string getDataFormat = "{0}?fields=storyline%2Crating%2Cdevelopers%2Cpublishers%2Cscreenshots%2Cgame_modes&limit=10&offset=0&order=release_dates.date%3Adesc";
        private static string apiKey = "WAV35CMfrrmshHzymVUYTbw3Sz7sp1AjcD0jsnjooQzM79mbIk";
        Dictionary<String, String> headers = new Dictionary<string, string>()
        {
            { "X-Mashape-Key", apiKey },
            { "Accept", "application/json" }
        };
        IgdbPlatformConverter platformConverter = new IgdbPlatformConverter();
        IgdbReleaseDateConverter dateConverter = new IgdbReleaseDateConverter();
        IgdbGameModeConverter gameModeConverter = new IgdbGameModeConverter();

        public override string FriendlyName => "IGDB.com";

        protected override RomData ScraperSpecificGetAllData(RomData dataToFillOut)
        {
            string finalUrl = String.Format(getDataFormat, dataToFillOut.ScraperUniqueKey);
            finalUrl = String.Format("{0}{1}", searchRootUrl, finalUrl);
            string Results = MakeTextRequest(finalUrl, headers);

            if(Results != String.Empty)
            {
                IgdbData resultData = SerializationUtilities.DeserializeString<List<IgdbData>>(Results, DataFormat.JSON, gameModeConverter)[0];
            }
            return dataToFillOut;
        }

        protected override List<RomData> ScraperSpecificSearch(RomData dataToSearchFor)
        {
            string nameTerm = GenerateSearchableName(dataToSearchFor);
            string searchTerm = string.Format(searchFormat, nameTerm);
            string finalUrl = String.Format("{0}{1}", searchRootUrl, searchTerm);
            

            List<RomData> returnData = new List<RomData>();
            String results = MakeTextRequest(finalUrl, headers);
            if(results != String.Empty)
            {
                List<IgdbData> resultsData = SerializationUtilities.DeserializeString<List<IgdbData>>(
                    results, DataFormat.JSON,platformConverter,dateConverter);

                foreach(var resultData in resultsData)
                {
                    RomData convertedData = dataToSearchFor.Clone();
                    convertedData.FriendlyName = resultData.name;
                    convertedData.ScraperUniqueKey = resultData.id;
                    if(resultData.release_dates != null && resultData.release_dates.Count() > 0)
                    {
                        var releaseData = resultData.release_dates[0];
                        convertedData.Console = releaseData.platform;
                        convertedData.ReleaseDate = releaseData.date;
                    }

                    returnData.Add(convertedData);
                }
            }

            return returnData;
        }
    }
}
