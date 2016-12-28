using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using EmulationCoordination.Scrapers.DataContracts.GiantBomb;
using EmulationCoordination.Utilities;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public class GiantBombScraper : BaseScraper
    {
        private string apiKey = "db7d9ec6b04d7d2fcfcfc5f9b45085f7cd86b138";
        private string rootUrl = "http://www.giantbomb.com/api/";
        private string getAllDataUrl = "game/{0}/?api_key={1}&format=json&field_list=deck,developers,image,images,publishers,reviews,releases";
        private string searchUrl = "search/?api_key={0}&query={1}&resources=game&field_list=id,name,platforms,original_release_date&limit=10&format=json";
        private Dictionary<string, string> headers = new Dictionary<string, string>()
        {
            {"User-Agent","EmulationCoordinationScraper"}
        };

        private GiantBombConsoleConverter consoleConverter = new GiantBombConsoleConverter();
        private GiantBombReleaseDateConverter dateConverter = new GiantBombReleaseDateConverter();

        public override string FriendlyName => "www.giantbomb.com";

        protected override RomData ScraperSpecificGetAllData(RomData dataToFillOut)
        {
            string dataTerm = string.Format(getAllDataUrl, dataToFillOut.ScraperUniqueKey, apiKey);
            string finalUrl = string.Format("{0}{1}", rootUrl, dataTerm);
            string results = MakeTextRequest(finalUrl, headers);

            if(results != string.Empty)
            {
                GiantBombGameResults resultData = SerializationUtilities.DeserializeString<GiantBombGameResults>(results, DataFormat.JSON);
                if(resultData.results != null)
                {
                    var extractedResults = resultData.results;
                    dataToFillOut.Description = extractedResults.deck;
                    if (extractedResults.publishers?.Length > 0)
                    {
                        dataToFillOut.Publisher = extractedResults.publishers[0].name;
                    }
                    if (extractedResults.developers?.Length > 0)
                    {
                        dataToFillOut.Developer = extractedResults.developers[0].name;
                    }
                    dataToFillOut.Rating = GetRating(extractedResults.reviews);
                    dataToFillOut.NumPlayers = GetNumPlayers(extractedResults.releases);
                    GetImages(dataToFillOut, extractedResults);
                }
            }

            return dataToFillOut;
        }

        private void GetImages(RomData dataToFillOut, GiantBombGameData extractedResults)
        {
            if(extractedResults.image != null)
            {
                dataToFillOut.Background = MakeImageRequest(extractedResults.image.super_url,headers);
            }
            if(extractedResults.images?.Length > 0)
            {
                var boxArtImages = extractedResults.images.Where(f => f.tags.Contains("Box Art")).ToList();
                var conceptArtImages = extractedResults.images.Where(f => f.tags.Contains("Concept Art")).ToList();
                var everythingElse = extractedResults.images.Where(f => !(f.tags.Contains("Box Art") && !(f.tags.Contains("Concept Art")))).ToList();
                
                if(boxArtImages.Count > 0)
                {
                    dataToFillOut.BoxArt = MakeImageRequest(boxArtImages[0].thumb_url, headers);
                }
                else
                {
                    dataToFillOut.BoxArt = MakeImageRequest(everythingElse[0].thumb_url, headers);
                }

                if(conceptArtImages.Count > 0)
                {
                    dataToFillOut.Banner = MakeImageRequest(conceptArtImages[0].screen_url, headers);
                    dataToFillOut.Logo = MakeImageRequest(conceptArtImages[0].icon_url, headers);
                }
                else
                {
                    dataToFillOut.Banner = MakeImageRequest(everythingElse[0].screen_url, headers);
                    dataToFillOut.Logo = MakeImageRequest(everythingElse[0].icon_url, headers);
                }
                
            }
            else
            {
                dataToFillOut.Banner = MakeImageRequest(extractedResults.image.screen_url, headers);
                dataToFillOut.BoxArt = MakeImageRequest(extractedResults.image.small_url, headers);
                dataToFillOut.Logo = MakeImageRequest(extractedResults.image.icon_url, headers);
            }
        }

        private string GetNumPlayers(GiantBombAdditionalDetail[] releases)
        {
            if(releases?.Length > 0)
            {
                string releaseUrl = string.Format("?api_key={0}&format=json&field_list=maximum_players,minimum_players", apiKey);
                string finalUrl = string.Format("{0}{1}", releases[0].api_detail_url, releaseUrl);
                String results = MakeTextRequest(finalUrl, headers);
                if(results != String.Empty)
                {
                    GiantBombGameResults resultData = SerializationUtilities.DeserializeString<GiantBombGameResults>(results, DataFormat.JSON);
                    return DetermineNumPlayers(resultData);
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

        private string DetermineNumPlayers(GiantBombGameResults resultData)
        {
            if(resultData.results.maximum_players != null)
            {
                if(resultData.results.maximum_players > 4)
                {
                    return "4+";
                }
                return resultData.results.maximum_players.ToString();
            }
            else if (resultData.results.minimum_players != null)
            {
                if (resultData.results.minimum_players > 4)
                {
                    return "4+";
                }
                return resultData.results.minimum_players.ToString();
            }
            else
            {
                return "Unknown";
            }
        }

        private float GetRating(GiantBombAdditionalDetail[] reviews)
        {
            if(reviews?.Length > 0)
            {
                string reviewUrl = string.Format("?api_key={0}&format=json&field_list=score",apiKey);
                string finalUrl = string.Format("{0}{1}", reviews[0].api_detail_url, reviewUrl);
                String results = MakeTextRequest(finalUrl, headers);
                if(results != String.Empty)
                {
                    GiantBombGameResults resultData = SerializationUtilities.DeserializeString<GiantBombGameResults>(results, DataFormat.JSON);
                    return resultData.results.score * 2;
                }
                else
                {
                    return 0.0f;
                }
            }
            else
            {
                return 0.0f;
            }
        }

        protected override List<RomData> ScraperSpecificSearch(RomData dataToSearchFor)
        {
            List<RomData> returnList = new List<RomData>();
            string nameTerm = GenerateSearchableName(dataToSearchFor);
            string searchTerm = String.Format(searchUrl, apiKey, nameTerm);
            string finalUrl = String.Format("{0}{1}", rootUrl, searchTerm);

            string results = MakeTextRequest(finalUrl,headers);
            if(results != String.Empty)
            {
                GiantBombSearchResults resultData = SerializationUtilities.DeserializeString<GiantBombSearchResults>(results, DataFormat.JSON,consoleConverter,dateConverter);
                if(resultData.results != null)
                {
                    foreach(var res in resultData.results)
                    {
                        RomData convertedData = dataToSearchFor.Clone();
                        convertedData.FriendlyName = res.name;
                        convertedData.ScraperUniqueKey = res.id.ToString();
                        convertedData.ReleaseDate = res.original_release_date;
                        convertedData.Console = EmulatorConsoles.UNKNOWN;
                        if(res.platforms?.Length > 0)
                        {
                            convertedData.Console = res.platforms[0].platform;
                        }
                        returnList.Add(convertedData);
                    }
                }
            }

            return returnList;
        }
    }
}
