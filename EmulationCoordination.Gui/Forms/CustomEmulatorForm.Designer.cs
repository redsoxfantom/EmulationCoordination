namespace EmulationCoordination.Gui.Forms
{
    partial class CustomEmulatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomEmulatorForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.emulatorExeLbl = new System.Windows.Forms.Label();
            this.mHelpBtn = new System.Windows.Forms.Button();
            this.mPathToExecutableTextBox = new System.Windows.Forms.TextBox();
            this.mEmulatorArgs = new System.Windows.Forms.TextBox();
            this.mBrowseButton = new System.Windows.Forms.Button();
            this.emulatorArgsLbl = new System.Windows.Forms.Label();
            this.emulatorNameLbl = new System.Windows.Forms.Label();
            this.mEmulatorNameTextBox = new System.Windows.Forms.TextBox();
            this.emulatorVersionLbl = new System.Windows.Forms.Label();
            this.mEmulatorVersionTextBox = new System.Windows.Forms.TextBox();
            this.mConsolesTextBox = new System.Windows.Forms.CheckedListBox();
            this.supportedConsolesLbl = new System.Windows.Forms.Label();
            this.emulatorTypeLabel = new System.Windows.Forms.Label();
            this.emulatorTypeCbx = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.mDoneBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.63265F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.36735F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 465);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.emulatorExeLbl, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.mHelpBtn, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.mPathToExecutableTextBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.mEmulatorArgs, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.mBrowseButton, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.emulatorArgsLbl, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.emulatorNameLbl, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.mEmulatorNameTextBox, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.emulatorVersionLbl, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.mEmulatorVersionTextBox, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.mConsolesTextBox, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.supportedConsolesLbl, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.emulatorTypeLabel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.emulatorTypeCbx, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 12;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(769, 373);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // emulatorExeLbl
            // 
            this.emulatorExeLbl.AutoSize = true;
            this.emulatorExeLbl.Location = new System.Drawing.Point(3, 0);
            this.emulatorExeLbl.Name = "emulatorExeLbl";
            this.emulatorExeLbl.Size = new System.Drawing.Size(190, 17);
            this.emulatorExeLbl.TabIndex = 0;
            this.emulatorExeLbl.Text = "Path to Emulator Executable:";
            // 
            // mHelpBtn
            // 
            this.mHelpBtn.Location = new System.Drawing.Point(618, 113);
            this.mHelpBtn.Name = "mHelpBtn";
            this.mHelpBtn.Size = new System.Drawing.Size(22, 23);
            this.mHelpBtn.TabIndex = 5;
            this.mHelpBtn.Text = "?";
            this.mHelpBtn.UseVisualStyleBackColor = true;
            this.mHelpBtn.Click += new System.EventHandler(this.mHelpBtn_Click);
            // 
            // mPathToExecutableTextBox
            // 
            this.mPathToExecutableTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPathToExecutableTextBox.Location = new System.Drawing.Point(3, 20);
            this.mPathToExecutableTextBox.Name = "mPathToExecutableTextBox";
            this.mPathToExecutableTextBox.Size = new System.Drawing.Size(609, 22);
            this.mPathToExecutableTextBox.TabIndex = 1;
            // 
            // mEmulatorArgs
            // 
            this.mEmulatorArgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mEmulatorArgs.Location = new System.Drawing.Point(3, 113);
            this.mEmulatorArgs.Name = "mEmulatorArgs";
            this.mEmulatorArgs.Size = new System.Drawing.Size(609, 22);
            this.mEmulatorArgs.TabIndex = 4;
            // 
            // mBrowseButton
            // 
            this.mBrowseButton.Location = new System.Drawing.Point(618, 20);
            this.mBrowseButton.Name = "mBrowseButton";
            this.mBrowseButton.Size = new System.Drawing.Size(116, 23);
            this.mBrowseButton.TabIndex = 2;
            this.mBrowseButton.Text = "Browse";
            this.mBrowseButton.UseVisualStyleBackColor = true;
            this.mBrowseButton.Click += new System.EventHandler(this.mBrowseButton_Click);
            // 
            // emulatorArgsLbl
            // 
            this.emulatorArgsLbl.AutoSize = true;
            this.emulatorArgsLbl.Location = new System.Drawing.Point(3, 93);
            this.emulatorArgsLbl.Name = "emulatorArgsLbl";
            this.emulatorArgsLbl.Size = new System.Drawing.Size(207, 17);
            this.emulatorArgsLbl.TabIndex = 3;
            this.emulatorArgsLbl.Text = "Emulator Command Arguments:";
            // 
            // emulatorNameLbl
            // 
            this.emulatorNameLbl.AutoSize = true;
            this.emulatorNameLbl.Location = new System.Drawing.Point(3, 139);
            this.emulatorNameLbl.Name = "emulatorNameLbl";
            this.emulatorNameLbl.Size = new System.Drawing.Size(109, 17);
            this.emulatorNameLbl.TabIndex = 6;
            this.emulatorNameLbl.Text = "Emulator Name:";
            // 
            // mEmulatorNameTextBox
            // 
            this.mEmulatorNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mEmulatorNameTextBox.Location = new System.Drawing.Point(3, 159);
            this.mEmulatorNameTextBox.Name = "mEmulatorNameTextBox";
            this.mEmulatorNameTextBox.Size = new System.Drawing.Size(609, 22);
            this.mEmulatorNameTextBox.TabIndex = 7;
            // 
            // emulatorVersionLbl
            // 
            this.emulatorVersionLbl.AutoSize = true;
            this.emulatorVersionLbl.Location = new System.Drawing.Point(3, 184);
            this.emulatorVersionLbl.Name = "emulatorVersionLbl";
            this.emulatorVersionLbl.Size = new System.Drawing.Size(120, 17);
            this.emulatorVersionLbl.TabIndex = 8;
            this.emulatorVersionLbl.Text = "Emulator Version:";
            // 
            // mEmulatorVersionTextBox
            // 
            this.mEmulatorVersionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mEmulatorVersionTextBox.Location = new System.Drawing.Point(3, 204);
            this.mEmulatorVersionTextBox.Name = "mEmulatorVersionTextBox";
            this.mEmulatorVersionTextBox.Size = new System.Drawing.Size(609, 22);
            this.mEmulatorVersionTextBox.TabIndex = 9;
            this.mEmulatorVersionTextBox.Text = "0.0.1";
            // 
            // mConsolesTextBox
            // 
            this.mConsolesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mConsolesTextBox.FormattingEnabled = true;
            this.mConsolesTextBox.Location = new System.Drawing.Point(3, 249);
            this.mConsolesTextBox.Name = "mConsolesTextBox";
            this.mConsolesTextBox.Size = new System.Drawing.Size(609, 130);
            this.mConsolesTextBox.TabIndex = 10;
            // 
            // supportedConsolesLbl
            // 
            this.supportedConsolesLbl.AutoSize = true;
            this.supportedConsolesLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.supportedConsolesLbl.Location = new System.Drawing.Point(3, 229);
            this.supportedConsolesLbl.Name = "supportedConsolesLbl";
            this.supportedConsolesLbl.Size = new System.Drawing.Size(609, 17);
            this.supportedConsolesLbl.TabIndex = 11;
            this.supportedConsolesLbl.Text = "Supported Consoles:";
            // 
            // emulatorTypeLabel
            // 
            this.emulatorTypeLabel.AutoSize = true;
            this.emulatorTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emulatorTypeLabel.Location = new System.Drawing.Point(3, 46);
            this.emulatorTypeLabel.Name = "emulatorTypeLabel";
            this.emulatorTypeLabel.Size = new System.Drawing.Size(609, 17);
            this.emulatorTypeLabel.TabIndex = 12;
            this.emulatorTypeLabel.Text = "Emulator Type:";
            // 
            // emulatorTypeCbx
            // 
            this.emulatorTypeCbx.FormattingEnabled = true;
            this.emulatorTypeCbx.Location = new System.Drawing.Point(3, 66);
            this.emulatorTypeCbx.Name = "emulatorTypeCbx";
            this.emulatorTypeCbx.Size = new System.Drawing.Size(609, 24);
            this.emulatorTypeCbx.TabIndex = 13;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.mDoneBtn, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 382);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(769, 80);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // mDoneBtn
            // 
            this.mDoneBtn.Location = new System.Drawing.Point(347, 54);
            this.mDoneBtn.Name = "mDoneBtn";
            this.mDoneBtn.Size = new System.Drawing.Size(75, 23);
            this.mDoneBtn.TabIndex = 0;
            this.mDoneBtn.Text = "Done";
            this.mDoneBtn.UseVisualStyleBackColor = true;
            this.mDoneBtn.Click += new System.EventHandler(this.mDoneBtn_Click);
            // 
            // CustomEmulatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 465);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomEmulatorForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label emulatorExeLbl;
        private System.Windows.Forms.Button mHelpBtn;
        private System.Windows.Forms.TextBox mPathToExecutableTextBox;
        private System.Windows.Forms.Button mBrowseButton;
        private System.Windows.Forms.Label emulatorArgsLbl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button mDoneBtn;
        private System.Windows.Forms.TextBox mEmulatorArgs;
        private System.Windows.Forms.Label emulatorNameLbl;
        private System.Windows.Forms.TextBox mEmulatorNameTextBox;
        private System.Windows.Forms.Label emulatorVersionLbl;
        private System.Windows.Forms.TextBox mEmulatorVersionTextBox;
        private System.Windows.Forms.CheckedListBox mConsolesTextBox;
        private System.Windows.Forms.Label supportedConsolesLbl;
        private System.Windows.Forms.Label emulatorTypeLabel;
        private System.Windows.Forms.ComboBox emulatorTypeCbx;
    }
}