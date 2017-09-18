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
    public delegate void UpdateHandler(RomData data);

    public partial class RomDataView : UserControl
    {
        private RomData selectedRom;

        public event UpdateHandler ManualDataUpdateRequested;
        public event UpdateHandler AutomatedDataUpdateRequested;

        public RomDataView()
        {
            InitializeComponent();
        }

        public void ChildUpdate(RomData data)
        {
            selectedRom = data;

            BannerPanel.BackgroundImage = data.Banner;
            BoxArtPanel.BackgroundImage = data.BoxArt;
            FriendlyNameLabel.Text = data.FriendlyName;
            DescriptionBox.Text = data.Description;
            DeveloperLabel.Text = data.Developer;
            PublisherLabel.Text = data.Publisher;
            ReleaseDateLabel.Text = data.PrettyPrintReleaseDate();
            NumPlayersLabel.Text = data.NumPlayers;
            RatingLabel.Text = data.PrettyPrintRating();
            TimePlayedLabel.Text = data.PrettyPrintPlayTime();
            ConsolePanel.BackgroundImage = data.Console.ConsoleImage;
        }

        private void ScrapeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AutomatedDataUpdateRequested?.Invoke(selectedRom);
        }

        private void ManualLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManualDataUpdateRequested?.Invoke(selectedRom);
        }
    }
}
