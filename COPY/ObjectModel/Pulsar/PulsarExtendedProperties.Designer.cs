namespace Pulsar
{
    partial class PulsarExtendedProperties
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
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Location = new System.Drawing.Point(0, 0);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(243, 450);
            this.propertiesPanel.TabIndex = 0;
            // 
            // PulsarExtendedProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(264, 450);
            this.Controls.Add(this.propertiesPanel);
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PulsarExtendedProperties";
            this.Text = "Properties Inspector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel propertiesPanel;
    }
}