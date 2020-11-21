namespace Pulsar
{
    partial class PropertiesInspector
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
            this.propertiesInspectorToolStrip = new System.Windows.Forms.ToolStrip();
            this.propertiesInspectorStatusStrip = new System.Windows.Forms.StatusStrip();
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // propertiesInspectorToolStrip
            // 
            this.propertiesInspectorToolStrip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertiesInspectorToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.propertiesInspectorToolStrip.Location = new System.Drawing.Point(0, 0);
            this.propertiesInspectorToolStrip.Name = "propertiesInspectorToolStrip";
            this.propertiesInspectorToolStrip.Size = new System.Drawing.Size(800, 25);
            this.propertiesInspectorToolStrip.TabIndex = 0;
            this.propertiesInspectorToolStrip.Text = "toolStrip1";
            // 
            // propertiesInspectorStatusStrip
            // 
            this.propertiesInspectorStatusStrip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertiesInspectorStatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.propertiesInspectorStatusStrip.Location = new System.Drawing.Point(0, 463);
            this.propertiesInspectorStatusStrip.Name = "propertiesInspectorStatusStrip";
            this.propertiesInspectorStatusStrip.Size = new System.Drawing.Size(800, 22);
            this.propertiesInspectorStatusStrip.SizingGrip = false;
            this.propertiesInspectorStatusStrip.TabIndex = 1;
            this.propertiesInspectorStatusStrip.Text = "statusStrip1";
            // 
            // smallImageList
            // 
            this.smallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.smallImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 25);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(800, 438);
            this.propertyGrid.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // PropertiesInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.propertiesInspectorStatusStrip);
            this.Controls.Add(this.propertiesInspectorToolStrip);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PropertiesInspector";
            this.Text = "Properties Inspector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip propertiesInspectorToolStrip;
        private System.Windows.Forms.StatusStrip propertiesInspectorStatusStrip;
        private System.Windows.Forms.ImageList smallImageList;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}