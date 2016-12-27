using EmulationCoordination.Gui.Controls;
using EmulationCoordination.Roms;
using EmulationCoordination.Scrapers;
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
    public partial class ScraperUpdateForm : Form
    {
        private RomData selectedRom;
        private ScraperManager scrapMgr;

        private ScraperSelectScraper selectControl;
        private String selectedScraper = null;

        public ScraperUpdateForm()
        {
            InitializeComponent();

            selectControl = new ScraperSelectScraper();
        }

        public void Initialize(RomData selectedRom)
        {
            this.selectedRom = selectedRom;
            scrapMgr = ScraperManager.Instance;
            Text = String.Format("Updating {0}",selectedRom.FriendlyName);

            selectControl.Initialize(scrapMgr.GetAllScrapers());
            selectControl.Dock = DockStyle.Fill;
            InstructionsLabel.Text = "Select a Scraper to use";
            SubControlPanel.Controls.Add(selectControl);
        }

        private void AdvanceButton_Click(object sender, EventArgs e)
        {
            if(SubControlPanel.Controls[0] == selectControl)
            {
                selectedScraper = selectControl.GetSelectedScraper();
                if (selectedScraper != null)
                {
                    AdvanceToSelectingRomData();
                }
            }
        }

        private void AdvanceToSelectingRomData()
        {

        }
    }
}
