using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using System.Net;
using System.IO;
using System.Drawing;

namespace EmulationCoordination.Scrapers.Scrapers
{
    public abstract class BaseScraper : IScraper
    {
        public abstract string FriendlyName { get; }

        public RomData GetAllData(RomData dataToFillOut)
        {
            dataToFillOut.ScrapedBy = FriendlyName;
            dataToFillOut.IsUpToDate = true;

            return ScraperSpecificGetAllData(dataToFillOut);
        }

        protected abstract RomData ScraperSpecificGetAllData(RomData dataToFillOut);

        public List<RomData> Search(RomData dataToSearchFor)
        {
            List<RomData> returnList = new List<RomData>();
            returnList.AddRange(ScraperSpecificSearch(dataToSearchFor));
            return returnList;
        }

        protected abstract List<RomData> ScraperSpecificSearch(RomData dataToSearchFor);

        protected Image MakeImageRequest(String url, Dictionary<String, String> headers = null)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    AddHeaders(headers, client);
                    MemoryStream stream = new MemoryStream(client.DownloadData(url));
                    return Image.FromStream(stream);
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }

        protected String MakeWebRequest(String url, Dictionary<String,String> headers = null)
        {
            String val = String.Empty;
            using (WebClient client = new WebClient())
            {
                try
                {
                    AddHeaders(headers, client);
                    val = client.DownloadString(url);
                }
                catch (Exception)
                {

                }
            }
            return val;
        }

        private void AddHeaders(Dictionary<string, string> headers, WebClient client)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.Headers.Set(header.Key, header.Value);
                }
            }
        }
    }
}
