namespace EmulationCoordination.Gui
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.emulatorTreeView = new EmulationCoordination.Gui.Controls.EmulatorTreeView();
            this.romDataView = new EmulationCoordination.Gui.Controls.RomDataView();
            this.SuspendLayout();
            // 
            // emulatorTreeView
            // 
            this.emulatorTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.emulatorTreeView.Location = new System.Drawing.Point(0, 0);
            this.emulatorTreeView.Name = "emulatorTreeView";
            this.emulatorTreeView.Size = new System.Drawing.Size(363, 697);
            this.emulatorTreeView.TabIndex = 1;
            this.emulatorTreeView.DeletionRequested += new EmulationCoordination.Gui.Controls.EmulatorUpdateHandler(this.emulatorTreeView_DeletionRequested);
            this.emulatorTreeView.InstallationRequested += new EmulationCoordination.Gui.Controls.EmulatorUpdateHandler(this.emulatorTreeView_InstallationRequested);
            // 
            // romDataView
            // 
            this.romDataView.Dock = System.Windows.Forms.DockStyle.Right;
            this.romDataView.Location = new System.Drawing.Point(382, 0);
            this.romDataView.Name = "romDataView";
            this.romDataView.Size = new System.Drawing.Size(688, 697);
            this.romDataView.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 697);
            this.Controls.Add(this.romDataView);
            this.Controls.Add(this.emulatorTreeView);
            this.Name = "MainWindow";
            this.Text = "Emulator Coordinator";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.EmulatorTreeView emulatorTreeView;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Controls.RomDataView romDataView;
    }
}

