namespace EmulationCoordination.Gui.Controls
{
    partial class EmulatorTreeView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Available Emulators");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Installed Emulators");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Custom Emulators");
            this.treeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode1.Name = "AvailableEmulators";
            treeNode1.Text = "Available Emulators";
            treeNode2.Name = "InstalledEmulators";
            treeNode2.Text = "Installed Emulators";
            treeNode3.Name = "CustomEmulators";
            treeNode3.Text = "Custom Emulators";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView.Size = new System.Drawing.Size(288, 739);
            this.treeView.TabIndex = 1;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseClick);
            // 
            // EmulatorTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Name = "EmulatorTreeView";
            this.Size = new System.Drawing.Size(288, 739);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
    }
}
