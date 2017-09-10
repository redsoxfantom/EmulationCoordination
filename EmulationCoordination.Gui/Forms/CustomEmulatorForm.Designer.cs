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
            this.label1 = new System.Windows.Forms.Label();
            this.mHelpBtn = new System.Windows.Forms.Button();
            this.mPathToExecutableTextBox = new System.Windows.Forms.TextBox();
            this.mEmulatorArgs = new System.Windows.Forms.TextBox();
            this.mBrowseButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mEmulatorNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mEmulatorVersionTextBox = new System.Windows.Forms.TextBox();
            this.mConsolesTextBox = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(613, 418);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.mHelpBtn, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.mPathToExecutableTextBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.mEmulatorArgs, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.mBrowseButton, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.mEmulatorNameTextBox, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.mEmulatorVersionTextBox, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.mConsolesTextBox, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 8);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(607, 335);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path to Emulator Executable:";
            // 
            // mHelpBtn
            // 
            this.mHelpBtn.Location = new System.Drawing.Point(488, 66);
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
            this.mPathToExecutableTextBox.Size = new System.Drawing.Size(479, 22);
            this.mPathToExecutableTextBox.TabIndex = 1;
            // 
            // mEmulatorArgs
            // 
            this.mEmulatorArgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mEmulatorArgs.Location = new System.Drawing.Point(3, 66);
            this.mEmulatorArgs.Name = "mEmulatorArgs";
            this.mEmulatorArgs.Size = new System.Drawing.Size(479, 22);
            this.mEmulatorArgs.TabIndex = 4;
            // 
            // mBrowseButton
            // 
            this.mBrowseButton.Location = new System.Drawing.Point(488, 20);
            this.mBrowseButton.Name = "mBrowseButton";
            this.mBrowseButton.Size = new System.Drawing.Size(116, 23);
            this.mBrowseButton.TabIndex = 2;
            this.mBrowseButton.Text = "Browse";
            this.mBrowseButton.UseVisualStyleBackColor = true;
            this.mBrowseButton.Click += new System.EventHandler(this.mBrowseButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Emulator Command Arguments:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Emulator Name:";
            // 
            // mEmulatorNameTextBox
            // 
            this.mEmulatorNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mEmulatorNameTextBox.Location = new System.Drawing.Point(3, 112);
            this.mEmulatorNameTextBox.Name = "mEmulatorNameTextBox";
            this.mEmulatorNameTextBox.Size = new System.Drawing.Size(479, 22);
            this.mEmulatorNameTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Emulator Version:";
            // 
            // mEmulatorVersionTextBox
            // 
            this.mEmulatorVersionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mEmulatorVersionTextBox.Location = new System.Drawing.Point(3, 157);
            this.mEmulatorVersionTextBox.Name = "mEmulatorVersionTextBox";
            this.mEmulatorVersionTextBox.Size = new System.Drawing.Size(479, 22);
            this.mEmulatorVersionTextBox.TabIndex = 9;
            this.mEmulatorVersionTextBox.Text = "0.0.1";
            // 
            // mConsolesTextBox
            // 
            this.mConsolesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mConsolesTextBox.FormattingEnabled = true;
            this.mConsolesTextBox.Location = new System.Drawing.Point(3, 202);
            this.mConsolesTextBox.Name = "mConsolesTextBox";
            this.mConsolesTextBox.Size = new System.Drawing.Size(479, 130);
            this.mConsolesTextBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(479, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Supported Consoles:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.mDoneBtn, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 344);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(607, 71);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // mDoneBtn
            // 
            this.mDoneBtn.Location = new System.Drawing.Point(266, 45);
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
            this.ClientSize = new System.Drawing.Size(613, 418);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button mHelpBtn;
        private System.Windows.Forms.TextBox mPathToExecutableTextBox;
        private System.Windows.Forms.Button mBrowseButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button mDoneBtn;
        private System.Windows.Forms.TextBox mEmulatorArgs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mEmulatorNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox mEmulatorVersionTextBox;
        private System.Windows.Forms.CheckedListBox mConsolesTextBox;
        private System.Windows.Forms.Label label5;
    }
}