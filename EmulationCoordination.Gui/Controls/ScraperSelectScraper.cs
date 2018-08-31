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

        public ScraperSelectScraper()
        {
            InitializeComponent();
        }

        public void Initialize(List<String> scrapers)
        {
            cbxScraperSelection.Items.AddRange(scrapers.ToArray());
        }

        public String GetSelectedScraper()
        {
            string scraper = (String)cbxScraperSelection.SelectedItem;
            if(String.IsNullOrEmpty(scraper))
            {
                return null;
            }
            return scraper;
        }
    }
}
