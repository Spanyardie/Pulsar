namespace Pulsar
{
    partial class SceneTree
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SceneTree));
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.sceneTreeStatus = new System.Windows.Forms.StatusStrip();
            this.sceneTreeTools = new System.Windows.Forms.ToolStrip();
            this.sceneTreeView = new System.Windows.Forms.TreeView();
            this.sceneTreeImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // dockPanel1
            // 
            this.dockPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(700, 485);
            this.dockPanel1.TabIndex = 0;
            // 
            // sceneTreeStatus
            // 
            this.sceneTreeStatus.Font = new System.Drawing.Font("Arial", 8F);
            this.sceneTreeStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sceneTreeStatus.Location = new System.Drawing.Point(0, 463);
            this.sceneTreeStatus.Name = "sceneTreeStatus";
            this.sceneTreeStatus.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.sceneTreeStatus.Size = new System.Drawing.Size(700, 22);
            this.sceneTreeStatus.SizingGrip = false;
            this.sceneTreeStatus.TabIndex = 3;
            this.sceneTreeStatus.Text = "statusStrip1";
            // 
            // sceneTreeTools
            // 
            this.sceneTreeTools.Font = new System.Drawing.Font("Arial", 8F);
            this.sceneTreeTools.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sceneTreeTools.Location = new System.Drawing.Point(0, 0);
            this.sceneTreeTools.Name = "sceneTreeTools";
            this.sceneTreeTools.Size = new System.Drawing.Size(700, 25);
            this.sceneTreeTools.TabIndex = 4;
            this.sceneTreeTools.Text = "toolStrip1";
            // 
            // sceneTreeView
            // 
            this.sceneTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneTreeView.FullRowSelect = true;
            this.sceneTreeView.HideSelection = false;
            this.sceneTreeView.ImageIndex = 0;
            this.sceneTreeView.ImageList = this.sceneTreeImageList;
            this.sceneTreeView.Location = new System.Drawing.Point(0, 25);
            this.sceneTreeView.Name = "sceneTreeView";
            this.sceneTreeView.SelectedImageIndex = 0;
            this.sceneTreeView.Size = new System.Drawing.Size(700, 438);
            this.sceneTreeView.TabIndex = 6;
            // 
            // sceneTreeImageList
            // 
            this.sceneTreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("sceneTreeImageList.ImageStream")));
            this.sceneTreeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.sceneTreeImageList.Images.SetKeyName(0, "component.png");
            this.sceneTreeImageList.Images.SetKeyName(1, "iconfinder_codeblocks_3246770.png");
            this.sceneTreeImageList.Images.SetKeyName(2, "iconfinder_eog_3246767.png");
            this.sceneTreeImageList.Images.SetKeyName(3, "iconfinder_ghex_3246761.png");
            this.sceneTreeImageList.Images.SetKeyName(4, "iconfinder_google-keep_3246755.png");
            this.sceneTreeImageList.Images.SetKeyName(5, "iconfinder_mypaint_3246752.png");
            this.sceneTreeImageList.Images.SetKeyName(6, "iconfinder_preferences-system_3246751.png");
            this.sceneTreeImageList.Images.SetKeyName(7, "iconfinder_stellarium_3246749.png");
            this.sceneTreeImageList.Images.SetKeyName(8, "iconfinder_vmware-workstation_3246748.png");
            this.sceneTreeImageList.Images.SetKeyName(9, "iconfinder_calligrakrita_3246772.png");
            // 
            // SceneTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 485);
            this.Controls.Add(this.sceneTreeView);
            this.Controls.Add(this.sceneTreeTools);
            this.Controls.Add(this.sceneTreeStatus);
            this.Controls.Add(this.dockPanel1);
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.Name = "SceneTree";
            this.Text = "Scene Explorer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.StatusStrip sceneTreeStatus;
        private System.Windows.Forms.ToolStrip sceneTreeTools;
        private System.Windows.Forms.TreeView sceneTreeView;
        private System.Windows.Forms.ImageList sceneTreeImageList;
    }
}