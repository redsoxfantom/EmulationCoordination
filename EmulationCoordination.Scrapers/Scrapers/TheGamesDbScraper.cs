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
            String results = MakeTextRequest(finalUrl);

            if(results != String.Empty)
            {
                Data resultsData = SerializationUtilities.DeserializeString<Data>(results, DataFormat.XML);
                DataGame game = resultsData.Game[0];
                dataToFillOut.Rating = String.IsNullOrEmpty(game.Rating) ? 0.0f : float.Parse(game.Rating);
                dataToFillOut.Publisher = game.Publisher;
                dataToFillOut.Developer = game.Developer;
                dataToFillOut.Description = game.Overview;
                dataToFillOut.NumPlayers = game.Players;

                GetImages(dataToFillOut, resultsData, game);
            }
            return dataToFillOut;
        }

        private void GetImages(RomData dataToFillOut, Data resultsData, DataGame game)
        {
            String baseImgUrl = resultsData.baseImgUrl;
            if (game.Images?.Count() > 0)
            {
                var images = game.Images[0];
                GetBoxart(dataToFillOut, baseImgUrl, images);
                GetBackground(dataToFillOut, baseImgUrl, images);
                GetBanner(dataToFillOut, baseImgUrl, images);
                GetLogo(dataToFillOut, baseImgUrl, images);
            }
        }

        private void GetLogo(RomData dataToFillOut, string baseImgUrl, DataGameImages images)
        {
            if (images.clearlogo?.Count() > 0)
            {
                var logoUrl = images.clearlogo.First().Value;
                logoUrl = String.Format("{0}{1}", baseImgUrl, logoUrl);
                dataToFillOut.Logo = MakeImageRequest(logoUrl);
            }
            else
            {
                dataToFillOut.Logo = dataToFillOut.BoxArt;
            }
        }

        private void GetBanner(RomData dataToFillOut, string baseImgUrl, DataGameImages images)
        {
            if (images.banner?.Count() > 0)
            {
                var bannerUrl = images.banner.First().Value;
                bannerUrl = String.Format("{0}{1}", baseImgUrl, bannerUrl);
                dataToFillOut.Banner = MakeImageRequest(bannerUrl);
            }
        }

        private void GetBackground(RomData dataToFillOut, string baseImgUrl, DataGameImages images)
        {
            String backgroundUrl = String.Empty;
            if (images.fanart?.Count() > 0)
            {
                backgroundUrl = images.fanart.First().original[0].Value;
            }
            else if (images.screenshot?.Count() > 0)
            {
                backgroundUrl = images.screenshot.First().original[0].Value;
            }
            if (backgroundUrl != null)
            {
                backgroundUrl = String.Format("{0}{1}", baseImgUrl, backgroundUrl);
                dataToFillOut.Background = MakeImageRequest(backgroundUrl);
            }
        }

        private void GetBoxart(RomData dataToFillOut, string baseImgUrl, DataGameImages images)
        {
            if (images.boxart?.Count() > 0)
            {
                var boxartList = images.boxart.Where(f => f.side == "front");
                if (boxartList.Count() > 0)
                {
                    var boxartThumb = boxartList.First().thumb;
                    String boxartUrl = string.Format("{0}{1}", baseImgUrl, boxartThumb);
                    dataToFillOut.BoxArt = MakeImageRequest(boxartUrl);
                }
            }
        }

        protected override List<RomData> ScraperSpecificSearch(RomData dataToSearchFor)
        {
            List<RomData> data = new List<RomData>();

            String nameTerm = GenerateSearchableName(dataToSearchFor);
            String platformTerm = ConvertConsole(dataToSearchFor.Console);
            String searchPage = String.Format(searchPageformat, nameTerm, platformTerm);
            String finalUrl = String.Format("{0}{1}", rootUrl, searchPage);

            String results = MakeTextRequest(finalUrl);
            if(results != String.Empty)
            {
                Data resultsData = SerializationUtilities.DeserializeString<Data>(results, DataFormat.XML);
                foreach(var resultData in resultsData.Game)
                {
                    RomData convertedResultData = dataToSearchFor.Clone();
                    convertedResultData.FriendlyName = resultData.GameTitle;
                    convertedResultData.ScraperUniqueKey = resultData.id;
                    convertedResultData.ReleaseDate = String.IsNullOrEmpty(resultData.ReleaseDate) ? DateTime.MaxValue : DateTime.Parse(resultData.ReleaseDate);
                    convertedResultData.Console = ConvertConsole(resultData.Platform);

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

        private EmulatorConsoles ConvertConsole(String consoleName)
        {
            switch (consoleName)
            {
                case "Nintendo Game Boy":
                    return EmulatorConsoles.GAME_BOY;
                case "Nintendo 64":
                    return EmulatorConsoles.NINTENDO_64;
                case "Nintendo Game Boy Advance":
                    return EmulatorConsoles.GAME_BOY_ADVANCE;
                case "Nintendo Game Boy Color":
                    return EmulatorConsoles.GAME_BOY_COLOR;
                default:
                    throw new Exception("Could not parse " + consoleName);
            }
        }
    }
}
