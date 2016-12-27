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
    public partial class ScraperSelectRom : UserControl
    {
        private List<RomData> availableRoms;
        private List<RadioButton> romRadios;

        public ScraperSelectRom()
        {
            InitializeComponent();

            romRadios = new List<RadioButton>();
        }

        public void Initialize(List<RomData> availableRoms)
        {
            this.availableRoms = availableRoms;

            foreach(var rom in availableRoms)
            {
                RadioButton btn = new RadioButton();
                btn.Text = "";
                btn.Tag = rom;
                btn.Dock = DockStyle.Fill;

                Label nameLbl = new Label();
                nameLbl.Text = rom.FriendlyName;
                nameLbl.Dock = DockStyle.Fill;
                nameLbl.TextAlign = ContentAlignment.MiddleCenter;

                Label releaseDateLabel = new Label();
                releaseDateLabel.Text = (rom.ReleaseDate == DateTime.MinValue) ? "Unknown" : rom.ReleaseDate.ToShortDateString();
                releaseDateLabel.Dock = DockStyle.Fill;
                releaseDateLabel.TextAlign = ContentAlignment.MiddleCenter;

                Label platformLabel = new Label();
                platformLabel.Text = rom.Console.FriendlyName;
                platformLabel.Dock = DockStyle.Fill;
                platformLabel.TextAlign = ContentAlignment.MiddleCenter;

                romRadios.Add(btn);
                RadioRomGrid.RowCount++;
                RadioRomGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                RadioRomGrid.Controls.Add(btn, 0, romRadios.Count);
                RadioRomGrid.Controls.Add(nameLbl, 1, romRadios.Count);
                RadioRomGrid.Controls.Add(releaseDateLabel, 2, romRadios.Count);
                RadioRomGrid.Controls.Add(platformLabel, 3, romRadios.Count);
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
    }
}
