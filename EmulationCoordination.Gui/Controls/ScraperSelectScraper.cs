using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmulationCoordination.Scrapers.Scrapers;

namespace EmulationCoordination.Gui.Controls
{
    public partial class ScraperSelectScraper : UserControl
    {
        private List<String> scrapers;
        private List<RadioButton> scraperRadioButtons;

        public ScraperSelectScraper()
        {
            InitializeComponent();
            scraperRadioButtons = new List<RadioButton>();
        }

        public void Initialize(List<String> scrapers)
        {
            this.scrapers = scrapers;

            foreach(var scraper in scrapers)
            {
                RadioButton scraperButton = new RadioButton();
                scraperButton.Text = scraper;
                scraperButton.Tag = scraper;
                scraperButton.Dock = DockStyle.Fill;

                RadioButtonGrid.RowCount++;
                RadioButtonGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                RadioButtonGrid.Controls.Add(scraperButton, 0, scraperRadioButtons.Count);
                scraperRadioButtons.Add(scraperButton);
            }
        }

        public String GetSelectedScraper()
        {
            var checkedButton = scraperRadioButtons.Where(f => f.Checked);
            if(checkedButton.Count() < 1)
            {
                return null;
            }
            return (String)checkedButton.First().Tag;
        }
    }
}
