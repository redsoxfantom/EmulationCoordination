﻿namespace EmulationCoordination.Gui
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
            this.emulatorTreeView = new EmulationCoordination.Gui.Controls.EmulatorTreeView();
            this.SuspendLayout();
            // 
            // emulatorTreeView
            // 
            this.emulatorTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.emulatorTreeView.Location = new System.Drawing.Point(0, 0);
            this.emulatorTreeView.Name = "emulatorTreeView";
            this.emulatorTreeView.Size = new System.Drawing.Size(288, 697);
            this.emulatorTreeView.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 697);
            this.Controls.Add(this.emulatorTreeView);
            this.Name = "MainWindow";
            this.Text = "Emulator Coordinator";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.EmulatorTreeView emulatorTreeView;
    }
}
