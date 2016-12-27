using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using System.IO;
using EmulationCoordination.Scrapers.DataContracts;
using EmulationCoordination.Utilities;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public class TheGamesDbScraper : BaseScraper
    {
        private String rootUrl = @"http://thegamesdb.net/api/";
        private String searchPageformat = @"GetGamesList.php?name={0}&platform={1}";

        public override string FriendlyName => "TheGamesDb.net";

        protected override RomData ScraperSpecificGetAllData(RomData dataToFillOut)
        {
            throw new NotImplementedException();
        }

        protected override List<RomData> ScraperSpecificSearch(RomData dataToSearchFor)
        {
            List<RomData> data = new List<RomData>();
            
            String nameTerm = Path.GetFileNameWithoutExtension(dataToSearchFor.Path);
            nameTerm = nameTerm.Replace('_', ' '); // Seems to have more success without underscores
            nameTerm = Uri.EscapeDataString(nameTerm);
            String platformTerm = ConvertConsole(dataToSearchFor.Console);
            String searchPage = String.Format(searchPageformat, nameTerm, platformTerm);
            String finalUrl = String.Format("{0}{1}", rootUrl, searchPage);

            String results = MakeWebRequest(finalUrl);
            if(results != String.Empty)
            {
                Data resultsData = SerializationUtilities.DeserializeString<Data>(results, DataFormat.XML);
                foreach(var resultData in resultsData.Items)
                {
                    RomData convertedResultData = new RomData()
                    {
                        FriendlyName = resultData.GameTitle,
                        ScraperUniqueKey = resultData.id,
                        ReleaseDate = String.IsNullOrEmpty(resultData.ReleaseDate)? DateTime.MaxValue : DateTime.Parse(resultData.ReleaseDate),
                        Console = EmulatorConsoles.Parse(resultData.Platform)
                    };
                    data.Add(convertedResultData);
                }
            }

            return data;
        }

        private String ConvertConsole(EmulatorConsoles console)
        {
            if(console.Equals(EmulatorConsoles.GAME_BOY))
            {
                return "Nintendo%20Game%20Boy";
            }
            if(console.Equals(EmulatorConsoles.GAME_BOY_ADVANCE))
            {
                return "Nintendo%20Game%20Boy%20Advance";
            }
            if(console.Equals(EmulatorConsoles.GAME_BOY_COLOR))
            {
                return "Nintendo%20Game%20Boy%20Color";
            }
            if(console.Equals(EmulatorConsoles.NINTENDO_64))
            {
                return "Nintendo%2064";
            }
            throw new Exception("Console not supported: " + console.FriendlyName);
        }
    }
}
