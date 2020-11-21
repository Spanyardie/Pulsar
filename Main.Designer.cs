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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.renderSceneTools = new System.Windows.Forms.ToolStrip();
            this.renderSceneObjectTranslate = new System.Windows.Forms.ToolStripButton();
            this.renderSceneObjectRotate = new System.Windows.Forms.ToolStripButton();
            this.renderSceneObjectScale = new System.Windows.Forms.ToolStripButton();
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.sceneTreeView = new System.Windows.Forms.TreeView();
            this.AssetsControl = new System.Windows.Forms.TabControl();
            this.animationsPage = new System.Windows.Forms.TabPage();
            this.animationsListView = new System.Windows.Forms.ListView();
            this.fontsPage = new System.Windows.Forms.TabPage();
            this.fontsListView = new System.Windows.Forms.ListView();
            this.materialsPage = new System.Windows.Forms.TabPage();
            this.materialsListView = new System.Windows.Forms.ListView();
            this.modelsPage = new System.Windows.Forms.TabPage();
            this.modelsListView = new System.Windows.Forms.ListView();
            this.texturesPage = new System.Windows.Forms.TabPage();
            this.texturesListView = new System.Windows.Forms.ListView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.renderSurface = new Urho.Extensions.WinForms.UrhoSurface();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.assetsWatcher = new System.IO.FileSystemWatcher();
            this.sceneToolImages = new System.Windows.Forms.ImageList(this.components);
            this.renderSceneTools.SuspendLayout();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.AssetsControl.SuspendLayout();
            this.animationsPage.SuspendLayout();
            this.fontsPage.SuspendLayout();
            this.materialsPage.SuspendLayout();
            this.modelsPage.SuspendLayout();
            this.texturesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.assetsWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // renderSceneTools
            // 
            this.renderSceneTools.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.renderSceneTools.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.renderSceneTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renderSceneObjectTranslate,
            this.renderSceneObjectRotate,
            this.renderSceneObjectScale});
            this.renderSceneTools.Location = new System.Drawing.Point(0, 24);
            this.renderSceneTools.Name = "renderSceneTools";
            this.renderSceneTools.Size = new System.Drawing.Size(1390, 27);
            this.renderSceneTools.TabIndex = 1;
            this.renderSceneTools.Text = "Scene Tools";
            // 
            // renderSceneObjectTranslate
            // 
            this.renderSceneObjectTranslate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renderSceneObjectTranslate.Image = global::Pulsar.Properties.Resources.translate;
            this.renderSceneObjectTranslate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renderSceneObjectTranslate.Name = "renderSceneObjectTranslate";
            this.renderSceneObjectTranslate.Size = new System.Drawing.Size(24, 24);
            this.renderSceneObjectTranslate.Text = "Translate";
            this.renderSceneObjectTranslate.ToolTipText = "Move";
            this.renderSceneObjectTranslate.Click += new System.EventHandler(this.RenderSceneObjectTranslate_Click);
            // 
            // renderSceneObjectRotate
            // 
            this.renderSceneObjectRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renderSceneObjectRotate.Image = global::Pulsar.Properties.Resources.rotate;
            this.renderSceneObjectRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renderSceneObjectRotate.Name = "renderSceneObjectRotate";
            this.renderSceneObjectRotate.Size = new System.Drawing.Size(24, 24);
            this.renderSceneObjectRotate.Text = "Rotate";
            this.renderSceneObjectRotate.ToolTipText = "Rotate";
            this.renderSceneObjectRotate.Click += new System.EventHandler(this.RenderSceneObjectRotate_Click);
            // 
            // renderSceneObjectScale
            // 
            this.renderSceneObjectScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renderSceneObjectScale.Image = global::Pulsar.Properties.Resources.scale;
            this.renderSceneObjectScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renderSceneObjectScale.Name = "renderSceneObjectScale";
            this.renderSceneObjectScale.Size = new System.Drawing.Size(24, 24);
            this.renderSceneObjectScale.Text = "Scale";
            this.renderSceneObjectScale.ToolTipText = "Scale";
            this.renderSceneObjectScale.Click += new System.EventHandler(this.RenderSceneObjectScale_Click);
            // 
            // StatusPanel
            // 
            this.StatusPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusPanel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusPanel.Location = new System.Drawing.Point(0, 901);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.StatusPanel.Size = new System.Drawing.Size(1390, 22);
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
            this.MainMenu.Size = new System.Drawing.Size(1390, 24);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addSceneToolStripMenuItem
            // 
            this.addSceneToolStripMenuItem.Name = "addSceneToolStripMenuItem";
            this.addSceneToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addSceneToolStripMenuItem.Text = "Add Scene";
            // 
            // importSceneToolStripMenuItem
            // 
            this.importSceneToolStripMenuItem.Name = "importSceneToolStripMenuItem";
            this.importSceneToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.importSceneToolStripMenuItem.Text = "Import Scene";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.DeleteToolStripMenuItem.Text = "Delete";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // componentsToolStripMenuItem
            // 
            this.componentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.primitivesToolStripMenuItem});
            this.componentsToolStripMenuItem.Name = "componentsToolStripMenuItem";
            this.componentsToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
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
            this.primitivesToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.primitivesToolStripMenuItem.Text = "Primitives";
            // 
            // boxToolStripMenuItem
            // 
            this.boxToolStripMenuItem.Name = "boxToolStripMenuItem";
            this.boxToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.boxToolStripMenuItem.Text = "Box";
            this.boxToolStripMenuItem.Click += new System.EventHandler(this.BoxToolStripMenuItem_Click);
            // 
            // sphereToolStripMenuItem
            // 
            this.sphereToolStripMenuItem.Name = "sphereToolStripMenuItem";
            this.sphereToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.sphereToolStripMenuItem.Text = "Sphere";
            this.sphereToolStripMenuItem.Click += new System.EventHandler(this.SphereToolStripMenuItem_Click);
            // 
            // cylinderToolStripMenuItem
            // 
            this.cylinderToolStripMenuItem.Name = "cylinderToolStripMenuItem";
            this.cylinderToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.cylinderToolStripMenuItem.Text = "Cylinder";
            this.cylinderToolStripMenuItem.Click += new System.EventHandler(this.CylinderToolStripMenuItem_Click);
            // 
            // coneToolStripMenuItem
            // 
            this.coneToolStripMenuItem.Name = "coneToolStripMenuItem";
            this.coneToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.coneToolStripMenuItem.Text = "Cone";
            this.coneToolStripMenuItem.Click += new System.EventHandler(this.ConeToolStripMenuItem_Click);
            // 
            // planeStripMenuItem
            // 
            this.planeStripMenuItem.Name = "planeStripMenuItem";
            this.planeStripMenuItem.Size = new System.Drawing.Size(119, 22);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem,
            this.sceneExplorerToolStripMenuItem,
            this.assetsToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.CheckOnClick = true;
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // sceneExplorerToolStripMenuItem
            // 
            this.sceneExplorerToolStripMenuItem.CheckOnClick = true;
            this.sceneExplorerToolStripMenuItem.Name = "sceneExplorerToolStripMenuItem";
            this.sceneExplorerToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.sceneExplorerToolStripMenuItem.Text = "Scene Explorer";
            // 
            // assetsToolStripMenuItem
            // 
            this.assetsToolStripMenuItem.CheckOnClick = true;
            this.assetsToolStripMenuItem.Name = "assetsToolStripMenuItem";
            this.assetsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.assetsToolStripMenuItem.Text = "Assets";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1390, 850);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 10;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.sceneTreeView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.AssetsControl);
            this.splitContainer3.Size = new System.Drawing.Size(244, 850);
            this.splitContainer3.SplitterDistance = 436;
            this.splitContainer3.TabIndex = 10;
            // 
            // sceneTreeView
            // 
            this.sceneTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sceneTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneTreeView.FullRowSelect = true;
            this.sceneTreeView.HideSelection = false;
            this.sceneTreeView.Location = new System.Drawing.Point(0, 0);
            this.sceneTreeView.Name = "sceneTreeView";
            this.sceneTreeView.Size = new System.Drawing.Size(244, 436);
            this.sceneTreeView.TabIndex = 10;
            // 
            // AssetsControl
            // 
            this.AssetsControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.AssetsControl.Controls.Add(this.animationsPage);
            this.AssetsControl.Controls.Add(this.fontsPage);
            this.AssetsControl.Controls.Add(this.materialsPage);
            this.AssetsControl.Controls.Add(this.modelsPage);
            this.AssetsControl.Controls.Add(this.texturesPage);
            this.AssetsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AssetsControl.Font = new System.Drawing.Font("Arial", 8F);
            this.AssetsControl.Location = new System.Drawing.Point(0, 0);
            this.AssetsControl.Name = "AssetsControl";
            this.AssetsControl.SelectedIndex = 0;
            this.AssetsControl.Size = new System.Drawing.Size(244, 410);
            this.AssetsControl.TabIndex = 1;
            // 
            // animationsPage
            // 
            this.animationsPage.Controls.Add(this.animationsListView);
            this.animationsPage.Location = new System.Drawing.Point(4, 26);
            this.animationsPage.Name = "animationsPage";
            this.animationsPage.Padding = new System.Windows.Forms.Padding(3);
            this.animationsPage.Size = new System.Drawing.Size(236, 380);
            this.animationsPage.TabIndex = 0;
            this.animationsPage.Text = "Animations";
            this.animationsPage.UseVisualStyleBackColor = true;
            // 
            // animationsListView
            // 
            this.animationsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.animationsListView.Font = new System.Drawing.Font("Arial", 8F);
            this.animationsListView.HideSelection = false;
            this.animationsListView.Location = new System.Drawing.Point(3, 3);
            this.animationsListView.MultiSelect = false;
            this.animationsListView.Name = "animationsListView";
            this.animationsListView.Size = new System.Drawing.Size(230, 374);
            this.animationsListView.TabIndex = 0;
            this.animationsListView.TileSize = new System.Drawing.Size(223, 36);
            this.animationsListView.UseCompatibleStateImageBehavior = false;
            this.animationsListView.View = System.Windows.Forms.View.List;
            // 
            // fontsPage
            // 
            this.fontsPage.Controls.Add(this.fontsListView);
            this.fontsPage.Location = new System.Drawing.Point(4, 26);
            this.fontsPage.Name = "fontsPage";
            this.fontsPage.Size = new System.Drawing.Size(236, 380);
            this.fontsPage.TabIndex = 1;
            this.fontsPage.Text = "Fonts";
            this.fontsPage.UseVisualStyleBackColor = true;
            // 
            // fontsListView
            // 
            this.fontsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fontsListView.HideSelection = false;
            this.fontsListView.Location = new System.Drawing.Point(0, 0);
            this.fontsListView.MultiSelect = false;
            this.fontsListView.Name = "fontsListView";
            this.fontsListView.Size = new System.Drawing.Size(236, 380);
            this.fontsListView.TabIndex = 1;
            this.fontsListView.TileSize = new System.Drawing.Size(223, 36);
            this.fontsListView.UseCompatibleStateImageBehavior = false;
            this.fontsListView.View = System.Windows.Forms.View.List;
            // 
            // materialsPage
            // 
            this.materialsPage.Controls.Add(this.materialsListView);
            this.materialsPage.Location = new System.Drawing.Point(4, 26);
            this.materialsPage.Name = "materialsPage";
            this.materialsPage.Size = new System.Drawing.Size(236, 380);
            this.materialsPage.TabIndex = 2;
            this.materialsPage.Text = "Materials";
            this.materialsPage.UseVisualStyleBackColor = true;
            // 
            // materialsListView
            // 
            this.materialsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialsListView.HideSelection = false;
            this.materialsListView.Location = new System.Drawing.Point(0, 0);
            this.materialsListView.MultiSelect = false;
            this.materialsListView.Name = "materialsListView";
            this.materialsListView.Size = new System.Drawing.Size(236, 380);
            this.materialsListView.TabIndex = 1;
            this.materialsListView.TileSize = new System.Drawing.Size(223, 36);
            this.materialsListView.UseCompatibleStateImageBehavior = false;
            this.materialsListView.View = System.Windows.Forms.View.List;
            // 
            // modelsPage
            // 
            this.modelsPage.Controls.Add(this.modelsListView);
            this.modelsPage.Location = new System.Drawing.Point(4, 26);
            this.modelsPage.Name = "modelsPage";
            this.modelsPage.Size = new System.Drawing.Size(236, 380);
            this.modelsPage.TabIndex = 3;
            this.modelsPage.Text = "Models";
            this.modelsPage.UseVisualStyleBackColor = true;
            // 
            // modelsListView
            // 
            this.modelsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelsListView.HideSelection = false;
            this.modelsListView.Location = new System.Drawing.Point(0, 0);
            this.modelsListView.MultiSelect = false;
            this.modelsListView.Name = "modelsListView";
            this.modelsListView.Size = new System.Drawing.Size(236, 380);
            this.modelsListView.TabIndex = 1;
            this.modelsListView.TileSize = new System.Drawing.Size(223, 36);
            this.modelsListView.UseCompatibleStateImageBehavior = false;
            this.modelsListView.View = System.Windows.Forms.View.List;
            // 
            // texturesPage
            // 
            this.texturesPage.Controls.Add(this.texturesListView);
            this.texturesPage.Location = new System.Drawing.Point(4, 26);
            this.texturesPage.Name = "texturesPage";
            this.texturesPage.Size = new System.Drawing.Size(236, 380);
            this.texturesPage.TabIndex = 4;
            this.texturesPage.Text = "Textures";
            this.texturesPage.UseVisualStyleBackColor = true;
            // 
            // texturesListView
            // 
            this.texturesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.texturesListView.HideSelection = false;
            this.texturesListView.Location = new System.Drawing.Point(0, 0);
            this.texturesListView.MultiSelect = false;
            this.texturesListView.Name = "texturesListView";
            this.texturesListView.Size = new System.Drawing.Size(236, 380);
            this.texturesListView.TabIndex = 1;
            this.texturesListView.TileSize = new System.Drawing.Size(223, 36);
            this.texturesListView.UseCompatibleStateImageBehavior = false;
            this.texturesListView.View = System.Windows.Forms.View.List;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.renderSurface);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertiesPanel);
            this.splitContainer2.Size = new System.Drawing.Size(1142, 850);
            this.splitContainer2.SplitterDistance = 864;
            this.splitContainer2.TabIndex = 0;
            // 
            // renderSurface
            // 
            this.renderSurface.AutoSize = true;
            this.renderSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderSurface.ForceFocus = false;
            this.renderSurface.FpsLimit = 60;
            this.renderSurface.Location = new System.Drawing.Point(0, 0);
            this.renderSurface.Margin = new System.Windows.Forms.Padding(4);
            this.renderSurface.Name = "renderSurface";
            this.renderSurface.Paused = false;
            this.renderSurface.Size = new System.Drawing.Size(864, 850);
            this.renderSurface.TabIndex = 12;
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.AutoScroll = true;
            this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesPanel.Location = new System.Drawing.Point(0, 0);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(274, 850);
            this.propertiesPanel.TabIndex = 11;
            // 
            // assetsWatcher
            // 
            this.assetsWatcher.EnableRaisingEvents = true;
            this.assetsWatcher.Path = "E:\\VSProjects\\Windows\\Pulsar\\Assets";
            this.assetsWatcher.SynchronizingObject = this;
            // 
            // sceneToolImages
            // 
            this.sceneToolImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("sceneToolImages.ImageStream")));
            this.sceneToolImages.TransparentColor = System.Drawing.Color.Transparent;
            this.sceneToolImages.Images.SetKeyName(0, "rotate.png");
            this.sceneToolImages.Images.SetKeyName(1, "rotateActive.png");
            this.sceneToolImages.Images.SetKeyName(2, "scale.png");
            this.sceneToolImages.Images.SetKeyName(3, "scaleActive.png");
            this.sceneToolImages.Images.SetKeyName(4, "translate.png");
            this.sceneToolImages.Images.SetKeyName(5, "translateActive.png");
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1390, 923);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.renderSceneTools);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Text = "Pulsar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.renderSceneTools.ResumeLayout(false);
            this.renderSceneTools.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.AssetsControl.ResumeLayout(false);
            this.animationsPage.ResumeLayout(false);
            this.fontsPage.ResumeLayout(false);
            this.materialsPage.ResumeLayout(false);
            this.modelsPage.ResumeLayout(false);
            this.texturesPage.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.assetsWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip renderSceneTools;
        private System.Windows.Forms.StatusStrip StatusPanel;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSceneToolStripMenuItem;
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Urho.Extensions.WinForms.UrhoSurface renderSurface;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView sceneTreeView;
        private System.Windows.Forms.TabControl AssetsControl;
        private System.Windows.Forms.TabPage animationsPage;
        private System.Windows.Forms.ListView animationsListView;
        private System.Windows.Forms.TabPage fontsPage;
        private System.Windows.Forms.ListView fontsListView;
        private System.Windows.Forms.TabPage materialsPage;
        private System.Windows.Forms.ListView materialsListView;
        private System.Windows.Forms.TabPage modelsPage;
        private System.Windows.Forms.ListView modelsListView;
        private System.Windows.Forms.TabPage texturesPage;
        private System.Windows.Forms.ListView texturesListView;
        private System.IO.FileSystemWatcher assetsWatcher;
        private System.Windows.Forms.ToolStripButton renderSceneObjectTranslate;
        private System.Windows.Forms.ToolStripButton renderSceneObjectRotate;
        private System.Windows.Forms.ToolStripButton renderSceneObjectScale;
        private System.Windows.Forms.ImageList sceneToolImages;
    }
}

