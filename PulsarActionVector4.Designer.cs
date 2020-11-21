namespace Pulsar
{
    partial class PulsarActionVector4
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
            this.groupVector4 = new System.Windows.Forms.GroupBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtVector4A = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtVector4B = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.txtVector4G = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txtVector4R = new System.Windows.Forms.TextBox();
            this.groupVector4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupVector4
            // 
            this.groupVector4.Controls.Add(this.label39);
            this.groupVector4.Controls.Add(this.txtVector4A);
            this.groupVector4.Controls.Add(this.label36);
            this.groupVector4.Controls.Add(this.txtVector4B);
            this.groupVector4.Controls.Add(this.label37);
            this.groupVector4.Controls.Add(this.txtVector4G);
            this.groupVector4.Controls.Add(this.label38);
            this.groupVector4.Controls.Add(this.txtVector4R);
            this.groupVector4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupVector4.Location = new System.Drawing.Point(0, 0);
            this.groupVector4.Name = "groupVector4";
            this.groupVector4.Size = new System.Drawing.Size(257, 49);
            this.groupVector4.TabIndex = 33;
            this.groupVector4.TabStop = false;
            this.groupVector4.Text = "Vector4";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(192, 20);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(17, 13);
            this.label39.TabIndex = 28;
            this.label39.Text = "A:";
            // 
            // txtVector4A
            // 
            this.txtVector4A.Location = new System.Drawing.Point(215, 17);
            this.txtVector4A.Name = "txtVector4A";
            this.txtVector4A.Size = new System.Drawing.Size(33, 20);
            this.txtVector4A.TabIndex = 27;
            this.txtVector4A.TextChanged += new System.EventHandler(this.Vector4A_Changed);
            this.txtVector4A.Validating += new System.ComponentModel.CancelEventHandler(this.VectorA_Validating);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(130, 20);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(17, 13);
            this.label36.TabIndex = 26;
            this.label36.Text = "B:";
            // 
            // txtVector4B
            // 
            this.txtVector4B.Location = new System.Drawing.Point(153, 17);
            this.txtVector4B.Name = "txtVector4B";
            this.txtVector4B.Size = new System.Drawing.Size(33, 20);
            this.txtVector4B.TabIndex = 25;
            this.txtVector4B.TextChanged += new System.EventHandler(this.Vector4B_Changed);
            this.txtVector4B.Validating += new System.ComponentModel.CancelEventHandler(this.VectorB_Validating);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(68, 20);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(18, 13);
            this.label37.TabIndex = 24;
            this.label37.Text = "G:";
            // 
            // txtVector4G
            // 
            this.txtVector4G.Location = new System.Drawing.Point(91, 17);
            this.txtVector4G.Name = "txtVector4G";
            this.txtVector4G.Size = new System.Drawing.Size(33, 20);
            this.txtVector4G.TabIndex = 23;
            this.txtVector4G.TextChanged += new System.EventHandler(this.Vector4G_Changed);
            this.txtVector4G.Validating += new System.ComponentModel.CancelEventHandler(this.VectorG_Validating);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 20);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(18, 13);
            this.label38.TabIndex = 22;
            this.label38.Text = "R:";
            // 
            // txtVector4R
            // 
            this.txtVector4R.Location = new System.Drawing.Point(29, 17);
            this.txtVector4R.Name = "txtVector4R";
            this.txtVector4R.Size = new System.Drawing.Size(33, 20);
            this.txtVector4R.TabIndex = 21;
            this.txtVector4R.TextChanged += new System.EventHandler(this.Vector4R_Changed);
            this.txtVector4R.Validating += new System.ComponentModel.CancelEventHandler(this.VectorR_Validating);
            // 
            // PulsarActionVector4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupVector4);
            this.Name = "PulsarActionVector4";
            this.Size = new System.Drawing.Size(258, 50);
            this.groupVector4.ResumeLayout(false);
            this.groupVector4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupVector4;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtVector4A;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtVector4B;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txtVector4G;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtVector4R;
    }
}
