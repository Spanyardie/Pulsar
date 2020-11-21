namespace ExtendedModelProperties
{
    partial class ModelProperties
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
            this.components = new System.ComponentModel.Container();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.btnModelFile = new System.Windows.Forms.Button();
            this.imageModel = new System.Windows.Forms.ImageList(this.components);
            this.txtLOD = new System.Windows.Forms.TextBox();
            this.lblLOD = new System.Windows.Forms.Label();
            this.btnMaterialFile = new System.Windows.Forms.Button();
            this.txtMaterialName = new System.Windows.Forms.TextBox();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.btnViewMaterial = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Arial", 8F);
            this.lblModel.Location = new System.Drawing.Point(3, 44);
            this.lblModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(50, 16);
            this.lblModel.TabIndex = 0;
            this.lblModel.Text = "Model:";
            // 
            // txtModelName
            // 
            this.txtModelName.AllowDrop = true;
            this.txtModelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModelName.Font = new System.Drawing.Font("Arial", 8F);
            this.txtModelName.Location = new System.Drawing.Point(61, 42);
            this.txtModelName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.Size = new System.Drawing.Size(155, 23);
            this.txtModelName.TabIndex = 1;
            // 
            // btnModelFile
            // 
            this.btnModelFile.BackColor = System.Drawing.Color.Blue;
            this.btnModelFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModelFile.Font = new System.Drawing.Font("Arial", 8F);
            this.btnModelFile.ForeColor = System.Drawing.Color.White;
            this.btnModelFile.Location = new System.Drawing.Point(226, 42);
            this.btnModelFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnModelFile.Name = "btnModelFile";
            this.btnModelFile.Size = new System.Drawing.Size(31, 26);
            this.btnModelFile.TabIndex = 2;
            this.btnModelFile.Text = "O";
            this.btnModelFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnModelFile.UseVisualStyleBackColor = false;
            this.btnModelFile.Click += new System.EventHandler(this.btnModelFile_Click);
            // 
            // imageModel
            // 
            this.imageModel.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageModel.ImageSize = new System.Drawing.Size(16, 16);
            this.imageModel.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // txtLOD
            // 
            this.txtLOD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLOD.Font = new System.Drawing.Font("Arial", 8F);
            this.txtLOD.Location = new System.Drawing.Point(61, 70);
            this.txtLOD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLOD.Name = "txtLOD";
            this.txtLOD.Size = new System.Drawing.Size(155, 23);
            this.txtLOD.TabIndex = 6;
            // 
            // lblLOD
            // 
            this.lblLOD.AutoSize = true;
            this.lblLOD.Font = new System.Drawing.Font("Arial", 8F);
            this.lblLOD.Location = new System.Drawing.Point(3, 72);
            this.lblLOD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLOD.Name = "lblLOD";
            this.lblLOD.Size = new System.Drawing.Size(41, 16);
            this.lblLOD.TabIndex = 5;
            this.lblLOD.Text = "LOD:";
            // 
            // btnMaterialFile
            // 
            this.btnMaterialFile.BackColor = System.Drawing.Color.Blue;
            this.btnMaterialFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaterialFile.Font = new System.Drawing.Font("Arial", 8F);
            this.btnMaterialFile.ForeColor = System.Drawing.Color.White;
            this.btnMaterialFile.Location = new System.Drawing.Point(226, 98);
            this.btnMaterialFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMaterialFile.Name = "btnMaterialFile";
            this.btnMaterialFile.Size = new System.Drawing.Size(31, 26);
            this.btnMaterialFile.TabIndex = 9;
            this.btnMaterialFile.Text = "O";
            this.btnMaterialFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMaterialFile.UseVisualStyleBackColor = false;
            this.btnMaterialFile.Click += new System.EventHandler(this.btnMaterialFile_Click);
            // 
            // txtMaterialName
            // 
            this.txtMaterialName.AllowDrop = true;
            this.txtMaterialName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaterialName.Font = new System.Drawing.Font("Arial", 8F);
            this.txtMaterialName.Location = new System.Drawing.Point(61, 99);
            this.txtMaterialName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMaterialName.Name = "txtMaterialName";
            this.txtMaterialName.Size = new System.Drawing.Size(155, 23);
            this.txtMaterialName.TabIndex = 8;
            // 
            // lblMaterial
            // 
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.Font = new System.Drawing.Font("Arial", 8F);
            this.lblMaterial.Location = new System.Drawing.Point(3, 101);
            this.lblMaterial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(62, 16);
            this.lblMaterial.TabIndex = 7;
            this.lblMaterial.Text = "Material:";
            // 
            // btnViewMaterial
            // 
            this.btnViewMaterial.BackColor = System.Drawing.Color.Yellow;
            this.btnViewMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewMaterial.Font = new System.Drawing.Font("Arial", 8F);
            this.btnViewMaterial.ForeColor = System.Drawing.Color.Black;
            this.btnViewMaterial.Location = new System.Drawing.Point(265, 98);
            this.btnViewMaterial.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnViewMaterial.Name = "btnViewMaterial";
            this.btnViewMaterial.Size = new System.Drawing.Size(31, 26);
            this.btnViewMaterial.TabIndex = 10;
            this.btnViewMaterial.Text = "V";
            this.btnViewMaterial.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnViewMaterial.UseVisualStyleBackColor = false;
            this.btnViewMaterial.Click += new System.EventHandler(this.btnViewMaterial_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Select a model";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(42, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 22);
            this.label1.TabIndex = 12;
            this.label1.Text = "Model";
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Green;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Arial", 8F);
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(226, 3);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(31, 26);
            this.btnSettings.TabIndex = 14;
            this.btnSettings.Text = "S";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSettings.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Red;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Arial", 8F);
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(265, 3);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(31, 26);
            this.btnRemove.TabIndex = 13;
            this.btnRemove.Text = "X";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.Yellow;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Font = new System.Drawing.Font("Arial", 8F);
            this.btnView.ForeColor = System.Drawing.Color.Black;
            this.btnView.Location = new System.Drawing.Point(4, 3);
            this.btnView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(31, 26);
            this.btnView.TabIndex = 15;
            this.btnView.Text = "^";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(312, 32);
            this.lblHeader.TabIndex = 16;
            // 
            // ModelProperties
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnViewMaterial);
            this.Controls.Add(this.btnMaterialFile);
            this.Controls.Add(this.txtMaterialName);
            this.Controls.Add(this.lblMaterial);
            this.Controls.Add(this.txtLOD);
            this.Controls.Add(this.lblLOD);
            this.Controls.Add(this.btnModelFile);
            this.Controls.Add(this.txtModelName);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.Name = "ModelProperties";
            this.Size = new System.Drawing.Size(312, 134);
            this.Resize += new System.EventHandler(this.ModelProperties_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModelName;
        private System.Windows.Forms.Button btnModelFile;
        private System.Windows.Forms.ImageList imageModel;
        private System.Windows.Forms.TextBox txtLOD;
        private System.Windows.Forms.Label lblLOD;
        private System.Windows.Forms.Button btnMaterialFile;
        private System.Windows.Forms.TextBox txtMaterialName;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.Button btnViewMaterial;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label lblHeader;
    }
}
