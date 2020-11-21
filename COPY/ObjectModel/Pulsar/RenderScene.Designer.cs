namespace Pulsar
{
    partial class RenderScene
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
            this.renderSceneTools = new System.Windows.Forms.ToolStrip();
            this.renderSceneObjectTranslate = new System.Windows.Forms.ToolStripButton();
            this.renderSceneObjectRotate = new System.Windows.Forms.ToolStripButton();
            this.renderSceneObjectScale = new System.Windows.Forms.ToolStripButton();
            this.renderSceneStatus = new System.Windows.Forms.StatusStrip();
            this.renderSurface = new Urho.Extensions.WinForms.UrhoSurface();
            this.renderSceneToolbarImages = new System.Windows.Forms.ImageList(this.components);
            this.renderSceneTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // renderSceneTools
            // 
            this.renderSceneTools.Font = new System.Drawing.Font("Arial", 8F);
            this.renderSceneTools.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.renderSceneTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renderSceneObjectTranslate,
            this.renderSceneObjectRotate,
            this.renderSceneObjectScale});
            this.renderSceneTools.Location = new System.Drawing.Point(0, 0);
            this.renderSceneTools.Name = "renderSceneTools";
            this.renderSceneTools.Size = new System.Drawing.Size(700, 27);
            this.renderSceneTools.TabIndex = 0;
            this.renderSceneTools.Text = "toolStrip1";
            // 
            // renderSceneObjectTranslate
            // 
            this.renderSceneObjectTranslate.Checked = true;
            this.renderSceneObjectTranslate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.renderSceneObjectTranslate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renderSceneObjectTranslate.Image = global::Pulsar.Properties.Resources.translateActive;
            this.renderSceneObjectTranslate.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.renderSceneObjectTranslate.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.renderSceneObjectTranslate.Name = "renderSceneObjectTranslate";
            this.renderSceneObjectTranslate.Size = new System.Drawing.Size(29, 24);
            this.renderSceneObjectTranslate.Text = "Move";
            this.renderSceneObjectTranslate.Click += new System.EventHandler(this.RenderSceneObjectTranslate_Click);
            // 
            // renderSceneObjectRotate
            // 
            this.renderSceneObjectRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renderSceneObjectRotate.Image = global::Pulsar.Properties.Resources.rotate;
            this.renderSceneObjectRotate.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.renderSceneObjectRotate.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.renderSceneObjectRotate.Name = "renderSceneObjectRotate";
            this.renderSceneObjectRotate.Size = new System.Drawing.Size(29, 24);
            this.renderSceneObjectRotate.Text = "Rotate";
            this.renderSceneObjectRotate.Click += new System.EventHandler(this.RenderSceneObjectRotate_Click);
            // 
            // renderSceneObjectScale
            // 
            this.renderSceneObjectScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renderSceneObjectScale.Image = global::Pulsar.Properties.Resources.scale;
            this.renderSceneObjectScale.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.renderSceneObjectScale.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.renderSceneObjectScale.Name = "renderSceneObjectScale";
            this.renderSceneObjectScale.Size = new System.Drawing.Size(29, 24);
            this.renderSceneObjectScale.Text = "Scale";
            this.renderSceneObjectScale.Click += new System.EventHandler(this.RenderSceneObjectScale_Click);
            // 
            // renderSceneStatus
            // 
            this.renderSceneStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.renderSceneStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.renderSceneStatus.Location = new System.Drawing.Point(0, 463);
            this.renderSceneStatus.Name = "renderSceneStatus";
            this.renderSceneStatus.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.renderSceneStatus.Size = new System.Drawing.Size(700, 22);
            this.renderSceneStatus.SizingGrip = false;
            this.renderSceneStatus.TabIndex = 1;
            this.renderSceneStatus.Text = "renderSceneStatusStrip";
            // 
            // renderSurface
            // 
            this.renderSurface.AutoSize = true;
            this.renderSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderSurface.ForceFocus = false;
            this.renderSurface.FpsLimit = 60;
            this.renderSurface.Location = new System.Drawing.Point(0, 27);
            this.renderSurface.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.renderSurface.Name = "renderSurface";
            this.renderSurface.Paused = false;
            this.renderSurface.Size = new System.Drawing.Size(700, 436);
            this.renderSurface.TabIndex = 2;
            // 
            // renderSceneToolbarImages
            // 
            this.renderSceneToolbarImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.renderSceneToolbarImages.ImageSize = new System.Drawing.Size(16, 16);
            this.renderSceneToolbarImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // RenderScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 485);
            this.Controls.Add(this.renderSurface);
            this.Controls.Add(this.renderSceneStatus);
            this.Controls.Add(this.renderSceneTools);
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.Name = "RenderScene";
            this.Text = "Scene";
            this.Load += new System.EventHandler(this.RenderScene_Load);
            this.renderSceneTools.ResumeLayout(false);
            this.renderSceneTools.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip renderSceneTools;
        private System.Windows.Forms.StatusStrip renderSceneStatus;
        private Urho.Extensions.WinForms.UrhoSurface renderSurface;
        private System.Windows.Forms.ToolStripButton renderSceneObjectTranslate;
        private System.Windows.Forms.ImageList renderSceneToolbarImages;
        private System.Windows.Forms.ToolStripButton renderSceneObjectRotate;
        private System.Windows.Forms.ToolStripButton renderSceneObjectScale;
    }
}