using EmulationCoordination.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmulationCoordination.Gui
{
    public partial class MainWindow : Form
    {
        private EmulatorManager emuMgr;

        public MainWindow()
        {
            InitializeComponent();

            emuMgr = EmulatorManager.Instance;
            UpdateChildren();
        }

        private void UpdateChildren()
        {
            emulatorTreeView.ChildUpdate(emuMgr.GetAvailableEmulators());
        }
    }
}
