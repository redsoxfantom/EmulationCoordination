using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Roms
{
    public class RomData
    {
        public Image Background { get; set; }

        public Image Banner { get; set; }

        public Image BoxArt { get; set; }

        public string Description { get; set; }

        public string Developer { get; set; }

        public string FriendlyName { get; set; }

        public Image Logo { get; set; }

        public string NumPlayers { get; set; }

        public string Path { get; set; }

        public string Publisher { get; set; }

        public float Rating { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TimeSpan TimePlayed { get; set; }

        public bool IsUpToDate { get; set; }

        public EmulatorConsoles Console { get; set; }

        public String ScrapedBy { get; set; }

        public String ScraperUniqueKey { get; set; }

        public RomData Clone()
        {
            RomData data = new RomData()
            {
                Background = this.Background,
                Banner = this.Banner,
                BoxArt = this.BoxArt,
                Description = this.Description,
                Developer = this.Developer,
                FriendlyName = this.FriendlyName,
                Logo = this.Logo,
                NumPlayers = this.NumPlayers,
                Path = this.Path,
                Publisher = this.Publisher,
                Rating = this.Rating,
                ReleaseDate = this.ReleaseDate,
                TimePlayed = this.TimePlayed,
                IsUpToDate = this.IsUpToDate,
                Console = this.Console,
                ScrapedBy = this.ScrapedBy,
                ScraperUniqueKey = this.ScraperUniqueKey
            };

            return data;
        }
        public string PrettyPrintPlayTime()
        {
            if (TimePlayed.TotalDays >= 2)
            {
                return String.Format("{0} Days", (int)TimePlayed.TotalDays);
            }
            else if (TimePlayed.TotalHours >= 2)
            {
                return String.Format("{0} Hours", (int)TimePlayed.TotalHours);
            }
            else if (TimePlayed.TotalMinutes >= 2)
            {
                return String.Format("{0} Minutes", (int)TimePlayed.TotalMinutes);
            }
            else if (TimePlayed.TotalSeconds >= 2)
            {
                return String.Format("{0} Seconds", (int)TimePlayed.TotalSeconds);
            }
            else
            {
                return "Not Played";
            }
        }

        public string PrettyPrintReleaseDate()
        {
            if (ReleaseDate == DateTime.MinValue)
            {
                return "Unknown";
            }
            return ReleaseDate.ToShortDateString();
        }

        public string PrettyPrintRating()
        {
            return String.Format("{0}/10", (int)Rating);
        }
    }
}
