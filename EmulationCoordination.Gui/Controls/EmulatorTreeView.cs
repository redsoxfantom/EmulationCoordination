﻿using System;
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
    public delegate void EmulatorUpdateHandler(IReadOnlyEmulator emulator);

    public partial class EmulatorTreeView : UserControl
    {
        private List<IReadOnlyEmulator> emulators;
        private List<IReadOnlyEmulator> installedEmulators;
        private List<IReadOnlyEmulator> availableEmulators;
        private List<EmulatorConsoles> availableConsoles;

        public event EmulatorUpdateHandler DeletionRequested;
        public event EmulatorUpdateHandler InstallationRequested;

        public EmulatorTreeView()
        {
            InitializeComponent();
        }

        public void ChildUpdate(List<IReadOnlyEmulator> emulators)
        {
            this.emulators = emulators;
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
    }
}