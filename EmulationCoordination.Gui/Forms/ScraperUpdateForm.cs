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
        private ScraperSelectRom selectRomControl;
        private String selectedScraper = null;

        public ScraperUpdateForm()
        {
            InitializeComponent();

            selectControl = new ScraperSelectScraper();
            selectRomControl = new ScraperSelectRom();
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
            else if(SubControlPanel.Controls[0] == selectRomControl)
            {
                RomData finalRom = selectRomControl.GetSelectedData();
                if(finalRom != null)
                {
                    AdvanceToGettingFinalRomData(finalRom);
                }
            }
        }

        private void AdvanceToGettingFinalRomData(RomData finalRom)
        {

        }

        private void AdvanceToSelectingRomData()
        {
            SubControlPanel.Controls.Remove(selectControl);
            selectControl.Dispose();
            InstructionsLabel.Text = String.Format("Searching {0} for {1}...",selectedScraper,selectedRom.FriendlyName);
            AdvanceButton.Enabled = false;

            backgroundWorker1.DoWork += (sender, e) =>
            {
                String selectedScraper = (String)((object[])e.Argument)[0];
                RomData romToSearchFor = (RomData)((object[])e.Argument)[1];

                List<RomData> foundRoms = scrapMgr.Search(romToSearchFor, selectedScraper);
                e.Result = foundRoms;
            };
            backgroundWorker1.RunWorkerCompleted += (sender, e) =>
            {
                List<RomData> foundData = (List<RomData>)e.Result;
                InstructionsLabel.Text = String.Format("Select a game");
                selectRomControl.Initialize(foundData);
                selectRomControl.Dock = DockStyle.Fill;
                SubControlPanel.Controls.Add(selectRomControl);

                AdvanceButton.Text = "Submit";
                AdvanceButton.Enabled = true;
            };
            backgroundWorker1.RunWorkerAsync(new object[] { selectedScraper,selectedRom });
        }
    }
}
