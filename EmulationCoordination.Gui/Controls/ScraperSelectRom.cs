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
    public delegate void SearchAgainHandler(string newSearchTerm);

    public partial class ScraperSelectRom : UserControl
    {
        private List<RomData> availableRoms;
        private List<RadioButton> romRadios;
        public event SearchAgainHandler OnSearchAgain;

        public ScraperSelectRom()
        {
            InitializeComponent();

            romRadios = new List<RadioButton>();
        }

        public void Initialize(List<RomData> availableRoms)
        {
            this.availableRoms = availableRoms;
            RadioRomGrid.RowCount = 0;
            RadioRomGrid.RowStyles.Clear();
            RadioRomGrid.Controls.Clear();

            if (availableRoms.Count > 0)
            {
                Label titleLabel = new Label();
                titleLabel.Text = "Title";
                titleLabel.TextAlign = ContentAlignment.MiddleCenter;
                titleLabel.Dock = DockStyle.Fill;
                Label dateLabel = new Label();
                dateLabel.Text = "Release Date";
                dateLabel.TextAlign = ContentAlignment.MiddleCenter;
                dateLabel.Dock = DockStyle.Fill;
                Label platformLabel = new Label();
                platformLabel.Text = "Platform";
                platformLabel.Dock = DockStyle.Fill;
                platformLabel.TextAlign = ContentAlignment.MiddleCenter;
                RadioRomGrid.RowCount++;
                RadioRomGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                RadioRomGrid.Controls.Add(titleLabel, 1, 0);
                RadioRomGrid.Controls.Add(dateLabel, 2, 0);
                RadioRomGrid.Controls.Add(platformLabel, 3, 0);

                foreach (var rom in availableRoms)
                {
                    RadioButton btn = new RadioButton();
                    btn.Text = "";
                    btn.Tag = rom;
                    btn.Dock = DockStyle.Fill;

                    titleLabel = new Label();
                    titleLabel.Text = rom.FriendlyName;
                    titleLabel.Dock = DockStyle.Fill;
                    titleLabel.TextAlign = ContentAlignment.MiddleCenter;

                    dateLabel = new Label();
                    dateLabel.Text = (rom.ReleaseDate == DateTime.MinValue) ? "Unknown" : rom.ReleaseDate.ToShortDateString();
                    dateLabel.Dock = DockStyle.Fill;
                    dateLabel.TextAlign = ContentAlignment.MiddleCenter;

                    platformLabel = new Label();
                    platformLabel.Text = rom.Console.FriendlyName;
                    platformLabel.Dock = DockStyle.Fill;
                    platformLabel.TextAlign = ContentAlignment.MiddleCenter;

                    romRadios.Add(btn);
                    RadioRomGrid.RowCount++;
                    RadioRomGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    RadioRomGrid.Controls.Add(btn, 0, romRadios.Count);
                    RadioRomGrid.Controls.Add(titleLabel, 1, romRadios.Count);
                    RadioRomGrid.Controls.Add(dateLabel, 2, romRadios.Count);
                    RadioRomGrid.Controls.Add(platformLabel, 3, romRadios.Count);
                }
            }
            else
            {
                Label noResults = new Label();
                noResults.Text = "No Results Found";
                noResults.Dock = DockStyle.Fill;
                noResults.TextAlign = ContentAlignment.MiddleCenter;

                RadioRomGrid.RowCount++;
                RadioRomGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                RadioRomGrid.Controls.Add(noResults, 1, 0);
            }
        }

        public RomData GetSelectedData()
        {
            var checkedButton = romRadios.Where(f => f.Checked);
            if (checkedButton.Count() < 1)
            {
                return null;
            }
            return (RomData)checkedButton.First().Tag;
        }

        private void searchAgainButton_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(searchTextBox.Text))
            {
                OnSearchAgain?.Invoke(searchTextBox.Text);
            }
        }
    }
}
