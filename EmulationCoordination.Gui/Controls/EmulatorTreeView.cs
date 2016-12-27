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
using EmulationCoordination.Roms;

namespace EmulationCoordination.Gui.Controls
{
    public delegate void EmulatorUpdateHandler(IReadOnlyEmulator emulator);
    public delegate void RomUpdateHandler(RomData rom);

    public partial class EmulatorTreeView : UserControl
    {
        public event EmulatorUpdateHandler DeletionRequested;
        public event EmulatorUpdateHandler InstallationRequested;
        public event RomUpdateHandler RomSelected;
        public event EventHandler RomDeselected;

        public EmulatorTreeView()
        {
            InitializeComponent();
        }

        public void ChildUpdate(List<IReadOnlyEmulator> emulators, List<RomData> roms)
        {
            var availableConsoles = emulators.SelectMany(f => f.ConsoleNames).Distinct().ToList();
            var installedEmulators = emulators.Where(f => f.Installed).ToList();
            var availableEmulators = emulators.Where(f => !f.Installed).ToList();

            PopulateTreeNodes(treeView.Nodes["AvailableEmulators"].Nodes, availableEmulators, availableConsoles, new List<RomData>());
            PopulateTreeNodes(treeView.Nodes["InstalledEmulators"].Nodes, installedEmulators, availableConsoles, roms);
        }

        private void PopulateTreeNodes(TreeNodeCollection Nodes, 
                                       List<IReadOnlyEmulator> emulators, 
                                       List<EmulatorConsoles> consoles, 
                                       List<RomData> roms)
        {
            Nodes.Clear();
            foreach(var console in consoles)
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
                    TreeNode emulatorNode = new TreeNode();
                    emulatorNode.Text = String.Format("{0} ({1})", emulator.EmulatorName, emulator.Version);
                    emulatorNode.Tag = emulator;
                    TreeNode consoleNode = (TreeNode)node;
                    
                    if(emulator.ConsoleNames.Contains(consoleNode.Tag))
                    {
                        consoleNode.Nodes.Add(emulatorNode);
                    }

                    var consoleSpecificRoms = roms.Where(f => { return f.Console.Equals(consoleNode.Tag); }).ToList();
                    foreach(var consoleSpecificRom in consoleSpecificRoms)
                    {
                        TreeNode romNode = new TreeNode();
                        romNode.Tag = consoleSpecificRom;
                        romNode.Text = consoleSpecificRom.FriendlyName;
                        emulatorNode.Nodes.Add(romNode);
                    }
                }
            }
        }

        private void treeView_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                TreeNode node = treeView.GetNodeAt(e.Location);
                treeView.SelectedNode = node;

                if(typeof(IReadOnlyEmulator).IsAssignableFrom(node.Tag?.GetType()))
                {
                    HandleRightClickEmulator((IReadOnlyEmulator)node.Tag);
                }
                else
                {
                    treeView.ContextMenuStrip = null;
                }
            }
        }

        private void HandleRightClickEmulator(IReadOnlyEmulator tag)
        {
            ContextMenuStrip ctxMenu = new ContextMenuStrip();

            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Tag = tag;
            if(tag.Installed)
            {
                menuItem.Text = "Delete Emulator";
                menuItem.Click += Delete_Selected;
            }
            else
            {
                menuItem.Text = "Install Emulator";
                menuItem.Click += Install_Selected;
            }
            ctxMenu.Items.Add(menuItem);

            treeView.ContextMenuStrip = ctxMenu;
        }

        private void Install_Selected(object sender, EventArgs e)
        {
            IReadOnlyEmulator emu = (IReadOnlyEmulator)((ToolStripMenuItem)sender).Tag;
            InstallationRequested?.Invoke(emu);
        }

        private void Delete_Selected(object sender, EventArgs e)
        {
            IReadOnlyEmulator emu = (IReadOnlyEmulator)((ToolStripMenuItem)sender).Tag;
            DeletionRequested?.Invoke(emu);
        }

        public IReadOnlyEmulator GetSelectedEmulator()
        {
            TreeNode e = treeView.SelectedNode;

            if (typeof(RomData).IsAssignableFrom(e.Tag?.GetType()))
            {
                return (IReadOnlyEmulator)e.Parent.Tag;
            }
            else
            {
                return null;
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(typeof(RomData).IsAssignableFrom(e.Node.Tag?.GetType()))
            {
                RomSelected?.Invoke((RomData)e.Node.Tag);
                treeView.SelectedNode = e.Node;
            }
            else
            {
                RomDeselected?.Invoke(this, null);
                treeView.SelectedNode = e.Node;
            }
        }
    }
}
