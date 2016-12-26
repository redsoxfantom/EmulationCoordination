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

        private void emulatorTreeView_DeletionRequested(IReadOnlyEmulator emulator)
        {
            backgroundWorker1.DoWork += (sender, e) => 
            {
                IReadOnlyEmulator emu = (IReadOnlyEmulator)e.Argument;
                emuMgr.DeleteEmulator(emu);
            };
            backgroundWorker1.RunWorkerCompleted += (sender, e) =>
            {
                UpdateChildren();
            };
            backgroundWorker1.RunWorkerAsync(emulator);
        }

        private void emulatorTreeView_InstallationRequested(IReadOnlyEmulator emulator)
        {
            backgroundWorker1.DoWork += (sender, e) =>
            {
                IReadOnlyEmulator emu = (IReadOnlyEmulator)e.Argument;
                emuMgr.DownloadAndInstallEmulator(emulator);
            };
            backgroundWorker1.RunWorkerCompleted += (sender, e) =>
            {
                UpdateChildren();
            };
            backgroundWorker1.RunWorkerAsync(emulator);
        }
    }
}
