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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.PlayGameBtn = new System.Windows.Forms.Button();
            this.emulatorTreeView = new EmulationCoordination.Gui.Controls.EmulatorTreeView();
            this.romDataView = new EmulationCoordination.Gui.Controls.RomDataView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.emulatorTreeView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(802, 576);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.romDataView, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(185, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.73585F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.26415F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(615, 572);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.PlayGameBtn, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 446);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(610, 120);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // PlayGameBtn
            // 
            this.PlayGameBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayGameBtn.Location = new System.Drawing.Point(205, 2);
            this.PlayGameBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PlayGameBtn.Name = "PlayGameBtn";
            this.PlayGameBtn.Size = new System.Drawing.Size(199, 36);
            this.PlayGameBtn.TabIndex = 0;
            this.PlayGameBtn.Text = "Play Game";
            this.PlayGameBtn.UseVisualStyleBackColor = true;
            this.PlayGameBtn.Visible = false;
            this.PlayGameBtn.Click += new System.EventHandler(this.PlayGameBtn_Click);
            // 
            // emulatorTreeView
            // 
            this.emulatorTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.emulatorTreeView.Location = new System.Drawing.Point(2, 2);
            this.emulatorTreeView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.emulatorTreeView.Name = "emulatorTreeView";
            this.emulatorTreeView.Size = new System.Drawing.Size(179, 572);
            this.emulatorTreeView.TabIndex = 1;
            this.emulatorTreeView.DeletionRequested += new EmulationCoordination.Gui.Controls.EmulatorUpdateHandler(this.emulatorTreeView_DeletionRequested);
            this.emulatorTreeView.InstallationRequested += new EmulationCoordination.Gui.Controls.EmulatorUpdateHandler(this.emulatorTreeView_InstallationRequested);
            this.emulatorTreeView.CustomRemovalRequested += new EmulationCoordination.Gui.Controls.EmulatorUpdateHandler(this.EmulatorTreeView_CustomRemovalRequested);
            this.emulatorTreeView.CreateCustomRom += new EmulationCoordination.Gui.Controls.CreateCustomEmulatorHandler(this.emulatorTreeView_CreateCustomRom);
            this.emulatorTreeView.RomSelected += new EmulationCoordination.Gui.Controls.RomUpdateHandler(this.emulatorTreeView_RomSelected);
            this.emulatorTreeView.RomDeselected += new System.EventHandler(this.emulatorTreeView_RomDeselected);
            // 
            // romDataView
            // 
            this.romDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.romDataView.Location = new System.Drawing.Point(2, 2);
            this.romDataView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.romDataView.Name = "romDataView";
            this.romDataView.Size = new System.Drawing.Size(611, 440);
            this.romDataView.TabIndex = 2;
            this.romDataView.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 576);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainWindow";
            this.Text = "Emulator Coordinator";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void RomDataView_AutomatedDataUpdateRequested(Roms.RomData data)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private Controls.EmulatorTreeView emulatorTreeView;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Controls.RomDataView romDataView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button PlayGameBtn;
    }
}

