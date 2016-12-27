using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using System.IO;
using EmulationCoordination.Utilities;
using EmulationCoordination.Scrapers.DataContracts.GamesDb;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public class TheGamesDbScraper : BaseScraper
    {
        private String rootUrl = @"http://thegamesdb.net/api/";
        private String searchPageformat = @"GetGamesList.php?name={0}&platform={1}";
        private String getDataFormat = @"GetGame.php?id={0}";

        public override string FriendlyName => "TheGamesDb.net";

        protected override RomData ScraperSpecificGetAllData(RomData dataToFillOut)
        {
            String getDataTerm = String.Format(getDataFormat, dataToFillOut.ScraperUniqueKey);
            String finalUrl = String.Format("{0}{1}", rootUrl, getDataTerm);
            String results = MakeWebRequest(finalUrl);

            if(results != String.Empty)
            {
                Data resultsData = SerializationUtilities.DeserializeString<Data>(results, DataFormat.XML);
                DataGame game = resultsData.Game[0];
                dataToFillOut.Rating = String.IsNullOrEmpty(game.Rating) ? 0.0f : float.Parse(game.Rating);
                dataToFillOut.Publisher = game.Publisher;
                dataToFillOut.Developer = game.Developer;
                dataToFillOut.Description = game.Overview;
                dataToFillOut.NumPlayers = game.Players;
            }
            return dataToFillOut;
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
                foreach(var resultData in resultsData.Game)
                {
                    RomData convertedResultData = dataToSearchFor.Clone();
                    convertedResultData.FriendlyName = resultData.GameTitle;
                    convertedResultData.ScraperUniqueKey = resultData.id;
                    convertedResultData.ReleaseDate = String.IsNullOrEmpty(resultData.ReleaseDate) ? DateTime.MaxValue : DateTime.Parse(resultData.ReleaseDate);
                    convertedResultData.Console = EmulatorConsoles.Parse(resultData.Platform);

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
