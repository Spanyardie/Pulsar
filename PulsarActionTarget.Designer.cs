namespace Pulsar
{
    partial class PulsarActionTarget
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
                _target?.Dispose();
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
            this.lblTarget = new System.Windows.Forms.Label();
            this.cboTarget = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(0, 0);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(68, 13);
            this.lblTarget.TabIndex = 38;
            this.lblTarget.Text = "Target node:";
            // 
            // cboTarget
            // 
            this.cboTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTarget.FormattingEnabled = true;
            this.cboTarget.Location = new System.Drawing.Point(3, 17);
            this.cboTarget.Name = "cboTarget";
            this.cboTarget.Size = new System.Drawing.Size(208, 21);
            this.cboTarget.TabIndex = 37;
            this.cboTarget.SelectedIndexChanged += new System.EventHandler(this.Target_SelectedIndexChanged);
            // 
            // PulsarActionTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTarget);
            this.Controls.Add(this.cboTarget);
            this.Name = "PulsarActionTarget";
            this.Size = new System.Drawing.Size(214, 42);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.ComboBox cboTarget;
    }
}
