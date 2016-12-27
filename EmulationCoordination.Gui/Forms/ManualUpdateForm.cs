using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;
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
            string imageFile = FileUtilities.UseFilePicker(FileUtilities.FilePickerType.LOAD, "Select Banner Image", "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG");
            if (!String.IsNullOrEmpty(imageFile))
            {
                try
                {
                    romToUpdate.Banner = Image.FromFile(imageFile);
                    BannerPanel.BackgroundImage = romToUpdate.Banner;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, String.Format("Could not load image: {0}", ex.Message), "ERROR");
                    romToUpdate.Banner = Resource.DefaultBanner;
                    BannerPanel.BackgroundImage = Resource.DefaultBanner;
                }
            }
        }

        private void BoxartPanel_Click(object sender, EventArgs e)
        {
            string imageFile = FileUtilities.UseFilePicker(FileUtilities.FilePickerType.LOAD, "Select BoxArt Image", "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG");
            if (!String.IsNullOrEmpty(imageFile))
            {
                try
                {
                    romToUpdate.BoxArt = Image.FromFile(imageFile);
                    BoxartPanel.BackgroundImage = romToUpdate.BoxArt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, String.Format("Could not load image: {0}", ex.Message), "ERROR");
                    romToUpdate.BoxArt = Resource.DefaultBoxart;
                    BoxartPanel.BackgroundImage = Resource.DefaultBoxart;
                }
            }
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
