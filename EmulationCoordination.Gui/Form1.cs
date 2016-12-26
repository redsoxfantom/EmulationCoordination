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
        private List<IReadOnlyEmulator> emulators;
        private List<IReadOnlyEmulator> installedEmulators;
        private List<IReadOnlyEmulator> availableEmulators;

        public MainWindow()
        {
            InitializeComponent();

            emuMgr = EmulatorManager.Instance;
            UpdateEmulators();
        }

        private void UpdateEmulators()
        {
            emulators = emuMgr.GetAvailableEmulators();
            installedEmulators = emulators.Where(f => f.Installed).ToList();
            availableEmulators = emulators.Where(f => !f.Installed).ToList();

            PopulateTreeNodes(EmulatorTreeView.Nodes["AvailableEmulators"].Nodes, availableEmulators);
            PopulateTreeNodes(EmulatorTreeView.Nodes["InstalledEmulators"].Nodes, installedEmulators);
        }

        private void PopulateTreeNodes(TreeNodeCollection Nodes, List<IReadOnlyEmulator> emulators)
        {
            Nodes.Clear();
            foreach(var emulator in emulators)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = String.Format("{0} ({1})", emulator.EmulatorName, emulator.Version);
                newNode.Tag = emulator;
                Nodes.Add(newNode);
            }
        }
    }
}
