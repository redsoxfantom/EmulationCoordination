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
    }
}
