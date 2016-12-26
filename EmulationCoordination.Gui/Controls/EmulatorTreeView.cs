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
using EmulationCoordination.Utilities;

namespace EmulationCoordination.Gui.Controls
{
    public partial class EmulatorTreeView : UserControl
    {
        private EmulatorManager emuMgr;
        private List<IReadOnlyEmulator> emulators;
        private List<IReadOnlyEmulator> installedEmulators;
        private List<IReadOnlyEmulator> availableEmulators;
        private List<EmulatorConsoles> availableConsoles;

        public EmulatorTreeView()
        {
            InitializeComponent();
            emuMgr = EmulatorManager.Instance;
        }

        public void ChildUpdate()
        {
            emulators = emuMgr.GetAvailableEmulators();
            availableConsoles = emulators.SelectMany(f => f.ConsoleNames).Distinct().ToList();
            installedEmulators = emulators.Where(f => f.Installed).ToList();
            availableEmulators = emulators.Where(f => !f.Installed).ToList();

            PopulateTreeNodes(treeView.Nodes["AvailableEmulators"].Nodes, availableEmulators);
            PopulateTreeNodes(treeView.Nodes["InstalledEmulators"].Nodes, installedEmulators);
        }

        private void PopulateTreeNodes(TreeNodeCollection Nodes, List<IReadOnlyEmulator> emulators)
        {
            Nodes.Clear();
            foreach(var console in availableConsoles)
            {
                TreeNode consoleNode = new TreeNode();
                consoleNode.Text = console.FriendlyName;
                consoleNode.Tag = console;
                Nodes.Add(consoleNode);
            }

            foreach (var emulator in emulators)
            {
                foreach(var node in Nodes)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = String.Format("{0} ({1})", emulator.EmulatorName, emulator.Version);
                    newNode.Tag = emulator;
                    TreeNode consoleNode = (TreeNode)node;

                    if(emulator.ConsoleNames.Contains(consoleNode.Tag))
                    {
                        consoleNode.Nodes.Add(newNode);
                    }
                }
            }
        }
    }
}
