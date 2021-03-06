﻿using EmulationCoordination.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Gui.Forms;
using EmulationCoordination.Roms;
using EmulationCoordination.Scrapers;
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
        private List<IReadOnlyEmulator> emulators;
        private List<RomData> roms;
        private RomData selectedRom = null;

        private EmulatorManager emuMgr;
        private RomManager romMgr;
        
        public MainWindow()
        {
            InitializeComponent();

            emuMgr = EmulatorManager.Instance;
            romMgr = RomManager.Instance;
            romMgr.NewRomAdded += RomMgr_NewRomAdded;

            UpdateEmulatorList();
        }

        private void RomMgr_NewRomAdded(RomData newData)
        {
            roms.Add(newData);
            this.Invoke(new Action(()=> { emulatorTreeView.ChildUpdate(emulators, roms); }));
        }

        private void UpdateEmulatorList()
        {
            emulators = emuMgr.GetAvailableEmulators();
            roms = new List<RomData>();
            var availableConsoles = emulators.SelectMany(f => f.ConsoleNames).Distinct().ToList();
            foreach(var console in availableConsoles)
            {
                roms.AddRange(romMgr.GetRoms(console));
            }

            emulatorTreeView.ChildUpdate(emulators,roms);
        }

        private void EmulatorTreeView_CustomRemovalRequested(Emulators.Interfaces.IReadOnlyEmulator emulator)
        {
            backgroundWorker1.DoWork += (sender, e) =>
            {
                IReadOnlyEmulator emu = (IReadOnlyEmulator)e.Argument;
                emuMgr.DeleteEmulator(emu);
            };
            backgroundWorker1.RunWorkerCompleted += (sender, e) =>
            {
                UpdateEmulatorList();
            };
            backgroundWorker1.RunWorkerAsync(emulator);
        }

        private void emulatorTreeView_RomSelected(RomData rom)
        {
            selectedRom = rom;
            romDataView.ChildUpdate(rom);
            romDataView.Visible = true;
            PlayGameBtn.Visible = true;
        }

        private void emulatorTreeView_RomDeselected(object sender, EventArgs e)
        {
            selectedRom = null;
            romDataView.Visible = false;
            PlayGameBtn.Visible = false;
        }

        private void PlayGameBtn_Click(object sender, EventArgs e)
        {
            var emulator = emulatorTreeView.GetSelectedEmulator();
            emuMgr.RunEmulator(emulator, selectedRom);
        }

        private void romDataView_ManualDataUpdateRequested(RomData data)
        {
            using (ManualUpdateForm form = new ManualUpdateForm())
            {
                form.Initialize(data);
                if(form.ShowDialog(this) == DialogResult.OK)
                {
                    selectedRom = (RomData)form.Tag;
                    romMgr.UpdateRomData(selectedRom);
                    UpdateEmulatorList();
                    romDataView.ChildUpdate(selectedRom);
                }
            }
        }

        private void romDataView_AutomatedDataUpdateRequested(RomData data)
        {
            using (ScraperUpdateForm form = new ScraperUpdateForm())
            {
                form.Initialize(data);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    selectedRom = (RomData)form.Tag;
                    romMgr.UpdateRomData(selectedRom);
                    UpdateEmulatorList();
                    romDataView.ChildUpdate(selectedRom);
                }
            }
        }

        private void emulatorTreeView_CreateCustomRom(Utilities.EmulatorConsoles console)
        {
            List<IKnownEmulator> knownEmulatorTypes = emuMgr.GetKnownEmulatorTypesForConsole(console);
            using (CustomEmulatorForm form = new CustomEmulatorForm())
            {
                form.Initialize(console,knownEmulatorTypes);
                if(form.ShowDialog(this) == DialogResult.OK)
                {
                    var emulator = form.Emulator;
                    emuMgr.AddCustomEmulator(emulator);
                    UpdateEmulatorList();
                }
            }
        }
    }
}
