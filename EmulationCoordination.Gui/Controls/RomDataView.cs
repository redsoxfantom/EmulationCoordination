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
            BackgroundImage = data.Background;
        }
    }
}
