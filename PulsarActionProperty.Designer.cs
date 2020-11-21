namespace Pulsar
{
    partial class PulsarActionProperty
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.txtPropertyValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.Location = new System.Drawing.Point(0, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(298, 17);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "header";
            // 
            // txtPropertyValue
            // 
            this.txtPropertyValue.Location = new System.Drawing.Point(3, 20);
            this.txtPropertyValue.Name = "txtPropertyValue";
            this.txtPropertyValue.Size = new System.Drawing.Size(143, 20);
            this.txtPropertyValue.TabIndex = 1;
            this.txtPropertyValue.Text = "0";
            this.txtPropertyValue.TextChanged += new System.EventHandler(this.TextPropertyValue_Changed);
            // 
            // PulsarActionProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtPropertyValue);
            this.Controls.Add(this.lblHeading);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PulsarActionProperty";
            this.Size = new System.Drawing.Size(151, 49);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.PulsarActionProperty_Validating);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TextBox txtPropertyValue;
    }
}
