namespace Pulsar
{
    partial class PulsarActionVector3
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
            this.groupVector3 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtVectorZ = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtVectorY = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtVectorX = new System.Windows.Forms.TextBox();
            this.groupVector3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupVector3
            // 
            this.groupVector3.Controls.Add(this.label27);
            this.groupVector3.Controls.Add(this.txtVectorZ);
            this.groupVector3.Controls.Add(this.label28);
            this.groupVector3.Controls.Add(this.txtVectorY);
            this.groupVector3.Controls.Add(this.label29);
            this.groupVector3.Controls.Add(this.txtVectorX);
            this.groupVector3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupVector3.Location = new System.Drawing.Point(0, 0);
            this.groupVector3.Name = "groupVector3";
            this.groupVector3.Size = new System.Drawing.Size(194, 49);
            this.groupVector3.TabIndex = 30;
            this.groupVector3.TabStop = false;
            this.groupVector3.Text = "Vector3";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(130, 20);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(17, 13);
            this.label27.TabIndex = 26;
            this.label27.Text = "Z:";
            // 
            // txtVectorZ
            // 
            this.txtVectorZ.Location = new System.Drawing.Point(153, 17);
            this.txtVectorZ.Name = "txtVectorZ";
            this.txtVectorZ.Size = new System.Drawing.Size(33, 20);
            this.txtVectorZ.TabIndex = 25;
            this.txtVectorZ.Text = "0";
            this.txtVectorZ.TextChanged += new System.EventHandler(this.VectorZ_Changed);
            this.txtVectorZ.Validating += new System.ComponentModel.CancelEventHandler(this.VectorZ_Validating);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(68, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(17, 13);
            this.label28.TabIndex = 24;
            this.label28.Text = "Y:";
            // 
            // txtVectorY
            // 
            this.txtVectorY.Location = new System.Drawing.Point(91, 17);
            this.txtVectorY.Name = "txtVectorY";
            this.txtVectorY.Size = new System.Drawing.Size(33, 20);
            this.txtVectorY.TabIndex = 23;
            this.txtVectorY.Text = "0";
            this.txtVectorY.TextChanged += new System.EventHandler(this.VectorY_Changed);
            this.txtVectorY.Validating += new System.ComponentModel.CancelEventHandler(this.VectorY_Validating);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 20);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(17, 13);
            this.label29.TabIndex = 22;
            this.label29.Text = "X:";
            // 
            // txtVectorX
            // 
            this.txtVectorX.Location = new System.Drawing.Point(29, 17);
            this.txtVectorX.Name = "txtVectorX";
            this.txtVectorX.Size = new System.Drawing.Size(33, 20);
            this.txtVectorX.TabIndex = 21;
            this.txtVectorX.Text = "0";
            this.txtVectorX.TextChanged += new System.EventHandler(this.VectorX_Changed);
            this.txtVectorX.Validating += new System.ComponentModel.CancelEventHandler(this.VectorX_Validating);
            // 
            // PulsarActionVector3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupVector3);
            this.Name = "PulsarActionVector3";
            this.Size = new System.Drawing.Size(195, 50);
            this.groupVector3.ResumeLayout(false);
            this.groupVector3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupVector3;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtVectorZ;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtVectorY;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtVectorX;
    }
}
