using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmulationCoordination.Roms;

namespace EmulationCoordination.Gui.Controls
{
    public partial class RomDataView : UserControl
    {
        public RomDataView()
        {
            InitializeComponent();
        }

        public void ChildUpdate(RomData data)
        {
            BannerPanel.BackgroundImage = data.Banner;
            BoxArtPanel.BackgroundImage = data.BoxArt;
            FriendlyNameLabel.Text = data.FriendlyName;
            DescriptionBox.Text = data.Description;
            DeveloperLabel.Text = data.Developer;
            PublisherLabel.Text = data.Publisher;
            ReleaseDateLabel.Text = data.ReleaseDate.ToShortTimeString();
            NumPlayersLabel.Text = PrettyPrintNumPlayers(data.NumPlayers);
            RatingLabel.Text = PrettyPrintRating(data.Rating);
            TimePlayedLabel.Text = PrettyPrintPlayTime(data.TimePlayed);
        }

        private string PrettyPrintNumPlayers(string numPlayers)
        {
            return String.Format("{0} players", numPlayers);
        }

        private string PrettyPrintRating(float rating)
        {
            return String.Format("{0}/10",(int)rating);
        }

        private string PrettyPrintPlayTime(TimeSpan span)
        {
            if (span.TotalDays >= 2)
            {
                return String.Format("{0} Days", (int)span.TotalDays);
            }
            else if (span.TotalHours >= 2)
            {
                return String.Format("{0} Hours", (int)span.TotalHours);
            }
            else if (span.TotalMinutes >= 2)
            {
                return String.Format("{0} Minutes", (int)span.TotalMinutes);
            }
            else if (span.TotalSeconds >= 2)
            {
                return String.Format("{0} Seconds", (int)span.TotalSeconds);
            }
            else
            {
                return "Not Played";
            }
        }
    }
}
