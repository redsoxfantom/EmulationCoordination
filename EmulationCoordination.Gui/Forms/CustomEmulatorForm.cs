using EmulationCoordination.Emulators.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmulationCoordination.Gui.Forms
{
    public partial class CustomEmulatorForm : Form
    {
        public CustomEmulator Emulator { get; private set; }
        private EmulatorConsoles Console;

        public CustomEmulatorForm()
        {
            InitializeComponent();
            UpdateSupportedConsoles();
        }

        private void UpdateSupportedConsoles()
        {
            mConsolesTextBox.Items.Clear();
            foreach(var console in EmulatorConsoles.Values)
            {
                mConsolesTextBox.Items.Add(console);
            }
        }

        public void Initialize(EmulatorConsoles console, List<IKnownEmulator> knownEmulatorTypes)
        {
            DialogResult = DialogResult.Cancel;
            Console = console;
            for(int i = 0; i < mConsolesTextBox.Items.Count; i++)
            {
                if((EmulatorConsoles)mConsolesTextBox.Items[i] == console)
                {
                    mConsolesTextBox.SetItemChecked(i, true);
                }
            }
            if(knownEmulatorTypes.Count == 0)
            {
                emulatorTypeCbx.Visible = false;
                emulatorTypeLabel.Visible = false;
            }
            else
            {
                foreach(var knownEmulator in knownEmulatorTypes)
                {
                    emulatorTypeCbx.Items.Add(knownEmulator);
                }
                emulatorTypeCbx.SelectedValueChanged += EmulatorTypeCbx_SelectedValueChanged;
            }
            Text = String.Format("Add Emulator");
        }

        private void EmulatorTypeCbx_SelectedValueChanged(object sender, EventArgs e)
        {
            IKnownEmulator emulatorType = (IKnownEmulator)emulatorTypeCbx.SelectedItem;
            mEmulatorNameTextBox.Text = emulatorType.Name;
            mEmulatorVersionTextBox.Text = emulatorType.Version;
            mEmulatorArgs.Text = emulatorType.Arguments;
        }

        private void mBrowseButton_Click(object sender, EventArgs e)
        {
            mPathToExecutableTextBox.Text = FileUtilities.UseFilePicker(FileUtilities.FilePickerType.LOAD, "Select Emulator Executable");
            mEmulatorNameTextBox.Text = Path.GetFileName(mPathToExecutableTextBox.Text);
        }

        private void mHelpBtn_Click(object sender, EventArgs e)
        {
            String helpTxt = "Enter the arguments that will be passed to the custom emulator.\n" +
                             "The following variables can be used, and will be replaced with\n" +
                             "their associated values prior to executing the custom emulator:\n" +
                             "$ROM_NAME - The filename of the rom that will be run.\n" +
                             "  This does not include the path.\n" +
                             "$ROM_PATH - The path of the rom that will be run.\n" +
                             "  This does not include the filename.\n" +
                             "$FULL_ROM_PATH - The path and filename of the rom that will be run.";
            MessageBox.Show(helpTxt,"Command Arguments Help");
        }

        private void mDoneBtn_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(mPathToExecutableTextBox.Text) ||
               String.IsNullOrEmpty(mEmulatorNameTextBox.Text) ||
               String.IsNullOrEmpty(mEmulatorVersionTextBox.Text) ||
               String.IsNullOrEmpty(mEmulatorArgs.Text) ||
               mConsolesTextBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("All fields must be filled out", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Emulator = new CustomEmulator(mPathToExecutableTextBox.Text, mEmulatorArgs.Text, mEmulatorVersionTextBox.Text, 
                    mEmulatorNameTextBox.Text, mConsolesTextBox.CheckedItems.OfType<EmulatorConsoles>().ToList());
                DialogResult = DialogResult.OK;
            }
        }
    }
}
