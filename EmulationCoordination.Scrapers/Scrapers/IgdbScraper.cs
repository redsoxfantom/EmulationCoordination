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
        private string searchRootUrl = "https://igdbcom-internet-game-database-v1.p.mashape.com/";
        private string searchFormat = "games/?fields=name%2Cid%2Crelease_dates&limit=10&offset=0&search={0}";
        private string getDataFormat = "games/{0}?fields=storyline%2Crating%2Cdevelopers%2Cpublishers%2Cscreenshots%2Cgame_modes&limit=10&offset=0&order=release_dates.date%3Adesc";
        private string getCompanyFormat = "companies/{0}?fields=id%2Cname";
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
                dataToFillOut.Description = String.IsNullOrEmpty(resultData.storyline) ? "No Description Available" : resultData.storyline;
                dataToFillOut.NumPlayers = resultData.game_modes?.Count() > 0 ? resultData.game_modes[0].NumPlayers : "Unknown";
                dataToFillOut.Rating = (float)resultData.rating / 100.0f;
                dataToFillOut.Publisher = RetrieveCompanyInfo(resultData.publishers);
                dataToFillOut.Developer = RetrieveCompanyInfo(resultData.developers);
            }
            return dataToFillOut;
        }

        private string RetrieveCompanyInfo(long[] ids)
        {
            if(ids.Length > 0)
            {
                string finalUrl = String.Format(getCompanyFormat, ids[0]);
                finalUrl = String.Format("{0}{1}", searchRootUrl, finalUrl);
                string Results = MakeTextRequest(finalUrl, headers);
                if (Results != String.Empty)
                {
                    IgdbData resultData = SerializationUtilities.DeserializeString<List<IgdbData>>(Results, DataFormat.JSON, gameModeConverter)[0];
                    return resultData.name;
                }
                else
                {
                    return "Unknown";
                }
            }
            else
            {
                return "Unknown";
            }
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
