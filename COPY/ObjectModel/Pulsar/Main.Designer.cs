namespace Pulsar
{
    partial class Main
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
            this.Tools = new System.Windows.Forms.ToolStrip();
            this.StatusPanel = new System.Windows.Forms.StatusStrip();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.componentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primitivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sphereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cylinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planeStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sceneExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainDockerPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tools
            // 
            this.Tools.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tools.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Tools.Location = new System.Drawing.Point(0, 25);
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(1683, 25);
            this.Tools.TabIndex = 1;
            this.Tools.Text = "toolStrip1";
            // 
            // StatusPanel
            // 
            this.StatusPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusPanel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusPanel.Location = new System.Drawing.Point(0, 901);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.StatusPanel.Size = new System.Drawing.Size(1683, 22);
            this.StatusPanel.TabIndex = 2;
            this.StatusPanel.Text = "statusStrip1";
            // 
            // MainMenu
            // 
            this.MainMenu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.componentsToolStripMenuItem,
            this.windowsToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MainMenu.Size = new System.Drawing.Size(1683, 25);
            this.MainMenu.TabIndex = 4;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSceneToolStripMenuItem,
            this.importSceneToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(45, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addSceneToolStripMenuItem
            // 
            this.addSceneToolStripMenuItem.Name = "addSceneToolStripMenuItem";
            this.addSceneToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.addSceneToolStripMenuItem.Text = "Add Scene";
            // 
            // importSceneToolStripMenuItem
            // 
            this.importSceneToolStripMenuItem.Name = "importSceneToolStripMenuItem";
            this.importSceneToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.importSceneToolStripMenuItem.Text = "Import Scene";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(132, 26);
            this.DeleteToolStripMenuItem.Text = "Delete";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // componentsToolStripMenuItem
            // 
            this.componentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.primitivesToolStripMenuItem});
            this.componentsToolStripMenuItem.Name = "componentsToolStripMenuItem";
            this.componentsToolStripMenuItem.Size = new System.Drawing.Size(106, 21);
            this.componentsToolStripMenuItem.Text = "Components";
            // 
            // primitivesToolStripMenuItem
            // 
            this.primitivesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boxToolStripMenuItem,
            this.sphereToolStripMenuItem,
            this.cylinderToolStripMenuItem,
            this.coneToolStripMenuItem,
            this.planeStripMenuItem});
            this.primitivesToolStripMenuItem.Name = "primitivesToolStripMenuItem";
            this.primitivesToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.primitivesToolStripMenuItem.Text = "Primitives";
            // 
            // boxToolStripMenuItem
            // 
            this.boxToolStripMenuItem.Name = "boxToolStripMenuItem";
            this.boxToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.boxToolStripMenuItem.Text = "Box";
            this.boxToolStripMenuItem.Click += new System.EventHandler(this.BoxToolStripMenuItem_Click);
            // 
            // sphereToolStripMenuItem
            // 
            this.sphereToolStripMenuItem.Name = "sphereToolStripMenuItem";
            this.sphereToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.sphereToolStripMenuItem.Text = "Sphere";
            this.sphereToolStripMenuItem.Click += new System.EventHandler(this.SphereToolStripMenuItem_Click);
            // 
            // cylinderToolStripMenuItem
            // 
            this.cylinderToolStripMenuItem.Name = "cylinderToolStripMenuItem";
            this.cylinderToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.cylinderToolStripMenuItem.Text = "Cylinder";
            this.cylinderToolStripMenuItem.Click += new System.EventHandler(this.CylinderToolStripMenuItem_Click);
            // 
            // coneToolStripMenuItem
            // 
            this.coneToolStripMenuItem.Name = "coneToolStripMenuItem";
            this.coneToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.coneToolStripMenuItem.Text = "Cone";
            this.coneToolStripMenuItem.Click += new System.EventHandler(this.ConeToolStripMenuItem_Click);
            // 
            // planeStripMenuItem
            // 
            this.planeStripMenuItem.Name = "planeStripMenuItem";
            this.planeStripMenuItem.Size = new System.Drawing.Size(143, 26);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem,
            this.sceneExplorerToolStripMenuItem,
            this.assetsToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.CheckOnClick = true;
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // sceneExplorerToolStripMenuItem
            // 
            this.sceneExplorerToolStripMenuItem.CheckOnClick = true;
            this.sceneExplorerToolStripMenuItem.Name = "sceneExplorerToolStripMenuItem";
            this.sceneExplorerToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.sceneExplorerToolStripMenuItem.Text = "Scene Explorer";
            // 
            // assetsToolStripMenuItem
            // 
            this.assetsToolStripMenuItem.CheckOnClick = true;
            this.assetsToolStripMenuItem.Name = "assetsToolStripMenuItem";
            this.assetsToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.assetsToolStripMenuItem.Text = "Assets";
            // 
            // MainDockerPanel
            // 
            this.MainDockerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDockerPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainDockerPanel.Location = new System.Drawing.Point(0, 50);
            this.MainDockerPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MainDockerPanel.Name = "MainDockerPanel";
            this.MainDockerPanel.Size = new System.Drawing.Size(1683, 851);
            this.MainDockerPanel.TabIndex = 5;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1683, 923);
            this.Controls.Add(this.MainDockerPanel);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.Tools);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Text = "Pulsar";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip Tools;
        private System.Windows.Forms.StatusStrip StatusPanel;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSceneToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel MainDockerPanel;
        private System.Windows.Forms.ToolStripMenuItem componentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primitivesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sphereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cylinderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planeStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sceneExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

