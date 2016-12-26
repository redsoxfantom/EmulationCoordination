using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmulationCoordination.Emulators;
using EmulationCoordination.Emulators.Interfaces;

namespace EmulationCoordination.Gui.Controls
{
    public partial class EmulatorTreeView : UserControl
    {
        private EmulatorManager emuMgr;
        private List<IReadOnlyEmulator> emulators;
        private List<IReadOnlyEmulator> installedEmulators;
        private List<IReadOnlyEmulator> availableEmulators;

        public EmulatorTreeView()
        {
            InitializeComponent();
            emuMgr = EmulatorManager.Instance;
        }

        public void ChildUpdate()
        {
            emulators = emuMgr.GetAvailableEmulators();
            installedEmulators = emulators.Where(f => f.Installed).ToList();
            availableEmulators = emulators.Where(f => !f.Installed).ToList();

            PopulateTreeNodes(treeView.Nodes["AvailableEmulators"].Nodes, availableEmulators);
            PopulateTreeNodes(treeView.Nodes["InstalledEmulators"].Nodes, installedEmulators);
        }

        private void PopulateTreeNodes(TreeNodeCollection Nodes, List<IReadOnlyEmulator> emulators)
        {
            Nodes.Clear();
            foreach (var emulator in emulators)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = String.Format("{0} ({1})", emulator.EmulatorName, emulator.Version);
                newNode.Tag = emulator;
                Nodes.Add(newNode);
            }
        }
    }
}
