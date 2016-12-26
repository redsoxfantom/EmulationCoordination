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
            this.EmulatorTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // EmulatorTreeView
            // 
            this.EmulatorTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.EmulatorTreeView.Location = new System.Drawing.Point(0, 0);
            this.EmulatorTreeView.Name = "EmulatorTreeView";
            this.EmulatorTreeView.Size = new System.Drawing.Size(272, 697);
            this.EmulatorTreeView.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 697);
            this.Controls.Add(this.EmulatorTreeView);
            this.Name = "MainWindow";
            this.Text = "Emulator Coordinator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView EmulatorTreeView;
    }
}

