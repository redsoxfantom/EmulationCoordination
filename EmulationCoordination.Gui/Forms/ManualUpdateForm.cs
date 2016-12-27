using EmulationCoordination.Roms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmulationCoordination.Gui.Forms
{
    public partial class ManualUpdateForm : Form
    {
        private RomData romToUpdate;

        public ManualUpdateForm()
        {
            InitializeComponent();
        }

        public void Initialize(RomData rom)
        {
            Text = String.Format("Updating {0}",rom.FriendlyName);
            BannerPanel.BackgroundImage = rom.Banner;
            BoxartPanel.BackgroundImage = rom.BoxArt;
            NameTextBox.Text = rom.FriendlyName;
            PublisherBox.Text = rom.Publisher;
            DeveloperBox.Text = rom.Developer;
            DescriptionBox.Text = rom.Description;
            NumPlayersBox.SelectedText = rom.NumPlayers;
            if(rom.ReleaseDate == DateTime.MinValue)
            {
                rom.ReleaseDate = ReleaseDateBox.MinDate;
            }
            ReleaseDateBox.Value = rom.ReleaseDate;
            RatingBox.SelectedIndex = (int)rom.Rating;

            romToUpdate = rom;
        }

        private void BannerPanel_Click(object sender, EventArgs e)
        {

        }

        private void BoxartPanel_Click(object sender, EventArgs e)
        {

        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            romToUpdate.Banner = BannerPanel.BackgroundImage;
            romToUpdate.BoxArt = BoxartPanel.BackgroundImage;
            romToUpdate.FriendlyName = NameTextBox.Text;
            romToUpdate.Publisher = PublisherBox.Text;
            romToUpdate.Developer = DeveloperBox.Text;
            romToUpdate.Description = DescriptionBox.Text;
            romToUpdate.NumPlayers = NumPlayersBox.Text;
            romToUpdate.ReleaseDate = ReleaseDateBox.Value;
            romToUpdate.Rating = float.Parse(RatingBox.Text);

            Tag = romToUpdate;
            DialogResult = DialogResult.OK;
        }
    }
}
