namespace Pulsar
{
    partial class PulsarAssets
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PulsarAssets));
            this.assetWatcher = new System.IO.FileSystemWatcher();
            this.AssetsControl = new System.Windows.Forms.TabControl();
            this.animationsPage = new System.Windows.Forms.TabPage();
            this.animationsListView = new System.Windows.Forms.ListView();
            this.largeImages = new System.Windows.Forms.ImageList(this.components);
            this.fontsPage = new System.Windows.Forms.TabPage();
            this.fontsListView = new System.Windows.Forms.ListView();
            this.materialsPage = new System.Windows.Forms.TabPage();
            this.materialsListView = new System.Windows.Forms.ListView();
            this.modelsPage = new System.Windows.Forms.TabPage();
            this.modelsListView = new System.Windows.Forms.ListView();
            this.texturesPage = new System.Windows.Forms.TabPage();
            this.texturesListView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.assetWatcher)).BeginInit();
            this.AssetsControl.SuspendLayout();
            this.animationsPage.SuspendLayout();
            this.fontsPage.SuspendLayout();
            this.materialsPage.SuspendLayout();
            this.modelsPage.SuspendLayout();
            this.texturesPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // assetWatcher
            // 
            this.assetWatcher.EnableRaisingEvents = true;
            this.assetWatcher.IncludeSubdirectories = true;
            this.assetWatcher.Path = "C:\\Users\\sebastian.quelcutti\\OneDrive - collinson365\\Documents\\Visual Studio 2019" +
    "\\Temp_Projects\\WindowsFormsApp1\\Assets";
            this.assetWatcher.SynchronizingObject = this;
            this.assetWatcher.Changed += new System.IO.FileSystemEventHandler(this.AssetWatcher_Changed);
            // 
            // AssetsControl
            // 
            this.AssetsControl.Controls.Add(this.animationsPage);
            this.AssetsControl.Controls.Add(this.fontsPage);
            this.AssetsControl.Controls.Add(this.materialsPage);
            this.AssetsControl.Controls.Add(this.modelsPage);
            this.AssetsControl.Controls.Add(this.texturesPage);
            this.AssetsControl.Font = new System.Drawing.Font("Arial", 8F);
            this.AssetsControl.Location = new System.Drawing.Point(10, 12);
            this.AssetsControl.Name = "AssetsControl";
            this.AssetsControl.SelectedIndex = 0;
            this.AssetsControl.Size = new System.Drawing.Size(679, 548);
            this.AssetsControl.TabIndex = 0;
            this.AssetsControl.Resize += new System.EventHandler(this.TabControl_Resize);
            // 
            // animationsPage
            // 
            this.animationsPage.Controls.Add(this.animationsListView);
            this.animationsPage.Location = new System.Drawing.Point(4, 25);
            this.animationsPage.Name = "animationsPage";
            this.animationsPage.Padding = new System.Windows.Forms.Padding(3);
            this.animationsPage.Size = new System.Drawing.Size(671, 519);
            this.animationsPage.TabIndex = 0;
            this.animationsPage.Text = "Animations";
            this.animationsPage.UseVisualStyleBackColor = true;
            // 
            // animationsListView
            // 
            this.animationsListView.Font = new System.Drawing.Font("Arial", 8F);
            this.animationsListView.LargeImageList = this.largeImages;
            this.animationsListView.Location = new System.Drawing.Point(5, 6);
            this.animationsListView.MultiSelect = false;
            this.animationsListView.Name = "animationsListView";
            this.animationsListView.Size = new System.Drawing.Size(662, 507);
            this.animationsListView.TabIndex = 0;
            this.animationsListView.TileSize = new System.Drawing.Size(223, 36);
            this.animationsListView.UseCompatibleStateImageBehavior = false;
            // 
            // largeImages
            // 
            this.largeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImages.ImageStream")));
            this.largeImages.TransparentColor = System.Drawing.Color.Transparent;
            this.largeImages.Images.SetKeyName(0, "folder.png");
            this.largeImages.Images.SetKeyName(1, "animation.png");
            this.largeImages.Images.SetKeyName(2, "fonts.png");
            this.largeImages.Images.SetKeyName(3, "material.png");
            this.largeImages.Images.SetKeyName(4, "model.png");
            this.largeImages.Images.SetKeyName(5, "texture.png");
            this.largeImages.Images.SetKeyName(6, "component.png");
            // 
            // fontsPage
            // 
            this.fontsPage.Controls.Add(this.fontsListView);
            this.fontsPage.Location = new System.Drawing.Point(4, 25);
            this.fontsPage.Name = "fontsPage";
            this.fontsPage.Size = new System.Drawing.Size(671, 519);
            this.fontsPage.TabIndex = 1;
            this.fontsPage.Text = "Fonts";
            this.fontsPage.UseVisualStyleBackColor = true;
            // 
            // fontsListView
            // 
            this.fontsListView.LargeImageList = this.largeImages;
            this.fontsListView.Location = new System.Drawing.Point(5, 6);
            this.fontsListView.MultiSelect = false;
            this.fontsListView.Name = "fontsListView";
            this.fontsListView.Size = new System.Drawing.Size(662, 507);
            this.fontsListView.TabIndex = 1;
            this.fontsListView.TileSize = new System.Drawing.Size(223, 36);
            this.fontsListView.UseCompatibleStateImageBehavior = false;
            // 
            // materialsPage
            // 
            this.materialsPage.Controls.Add(this.materialsListView);
            this.materialsPage.Location = new System.Drawing.Point(4, 25);
            this.materialsPage.Name = "materialsPage";
            this.materialsPage.Size = new System.Drawing.Size(671, 519);
            this.materialsPage.TabIndex = 2;
            this.materialsPage.Text = "Materials";
            this.materialsPage.UseVisualStyleBackColor = true;
            // 
            // materialsListView
            // 
            this.materialsListView.LargeImageList = this.largeImages;
            this.materialsListView.Location = new System.Drawing.Point(5, 6);
            this.materialsListView.MultiSelect = false;
            this.materialsListView.Name = "materialsListView";
            this.materialsListView.Size = new System.Drawing.Size(662, 507);
            this.materialsListView.TabIndex = 1;
            this.materialsListView.TileSize = new System.Drawing.Size(223, 36);
            this.materialsListView.UseCompatibleStateImageBehavior = false;
            // 
            // modelsPage
            // 
            this.modelsPage.Controls.Add(this.modelsListView);
            this.modelsPage.Location = new System.Drawing.Point(4, 25);
            this.modelsPage.Name = "modelsPage";
            this.modelsPage.Size = new System.Drawing.Size(671, 519);
            this.modelsPage.TabIndex = 3;
            this.modelsPage.Text = "Models";
            this.modelsPage.UseVisualStyleBackColor = true;
            // 
            // modelsListView
            // 
            this.modelsListView.LargeImageList = this.largeImages;
            this.modelsListView.Location = new System.Drawing.Point(5, 6);
            this.modelsListView.MultiSelect = false;
            this.modelsListView.Name = "modelsListView";
            this.modelsListView.Size = new System.Drawing.Size(662, 507);
            this.modelsListView.TabIndex = 1;
            this.modelsListView.TileSize = new System.Drawing.Size(223, 36);
            this.modelsListView.UseCompatibleStateImageBehavior = false;
            // 
            // texturesPage
            // 
            this.texturesPage.Controls.Add(this.texturesListView);
            this.texturesPage.Location = new System.Drawing.Point(4, 25);
            this.texturesPage.Name = "texturesPage";
            this.texturesPage.Size = new System.Drawing.Size(671, 519);
            this.texturesPage.TabIndex = 4;
            this.texturesPage.Text = "Textures";
            this.texturesPage.UseVisualStyleBackColor = true;
            // 
            // texturesListView
            // 
            this.texturesListView.LargeImageList = this.largeImages;
            this.texturesListView.Location = new System.Drawing.Point(5, 6);
            this.texturesListView.MultiSelect = false;
            this.texturesListView.Name = "texturesListView";
            this.texturesListView.Size = new System.Drawing.Size(662, 507);
            this.texturesListView.TabIndex = 1;
            this.texturesListView.TileSize = new System.Drawing.Size(223, 36);
            this.texturesListView.UseCompatibleStateImageBehavior = false;
            // 
            // PulsarAssets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 572);
            this.Controls.Add(this.AssetsControl);
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PulsarAssets";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Assets";
            this.Resize += new System.EventHandler(this.PulsarAssets_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.assetWatcher)).EndInit();
            this.AssetsControl.ResumeLayout(false);
            this.animationsPage.ResumeLayout(false);
            this.fontsPage.ResumeLayout(false);
            this.materialsPage.ResumeLayout(false);
            this.modelsPage.ResumeLayout(false);
            this.texturesPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.FileSystemWatcher assetWatcher;
        private System.Windows.Forms.TabControl AssetsControl;
        private System.Windows.Forms.TabPage animationsPage;
        private System.Windows.Forms.ListView animationsListView;
        private System.Windows.Forms.ImageList largeImages;
        private System.Windows.Forms.TabPage fontsPage;
        private System.Windows.Forms.ListView fontsListView;
        private System.Windows.Forms.TabPage materialsPage;
        private System.Windows.Forms.TabPage modelsPage;
        private System.Windows.Forms.TabPage texturesPage;
        private System.Windows.Forms.ListView materialsListView;
        private System.Windows.Forms.ListView modelsListView;
        private System.Windows.Forms.ListView texturesListView;
    }
}