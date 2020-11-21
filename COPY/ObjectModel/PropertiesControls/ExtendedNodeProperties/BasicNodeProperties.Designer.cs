namespace ExtendedNodeProperties
{
    partial class BasicNodeProperties
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
            this.txtPositionX = new System.Windows.Forms.TextBox();
            this.lblPositionMain = new System.Windows.Forms.Label();
            this.lblPositionX = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPositionY = new System.Windows.Forms.Label();
            this.txtPositionY = new System.Windows.Forms.TextBox();
            this.lblPositionZ = new System.Windows.Forms.Label();
            this.txtPositionZ = new System.Windows.Forms.TextBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.lblRotationZ = new System.Windows.Forms.Label();
            this.txtRotationZ = new System.Windows.Forms.TextBox();
            this.lblRotationY = new System.Windows.Forms.Label();
            this.txtRotationY = new System.Windows.Forms.TextBox();
            this.lblRotationX = new System.Windows.Forms.Label();
            this.lblRotationMain = new System.Windows.Forms.Label();
            this.txtRotationX = new System.Windows.Forms.TextBox();
            this.lblScaleZ = new System.Windows.Forms.Label();
            this.txtScaleZ = new System.Windows.Forms.TextBox();
            this.lblScaleY = new System.Windows.Forms.Label();
            this.txtScaleY = new System.Windows.Forms.TextBox();
            this.lblScaleX = new System.Windows.Forms.Label();
            this.lblScaleMain = new System.Windows.Forms.Label();
            this.txtScaleX = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPositionX
            // 
            this.txtPositionX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPositionX.Font = new System.Drawing.Font("Arial", 8F);
            this.txtPositionX.Location = new System.Drawing.Point(28, 110);
            this.txtPositionX.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPositionX.MaxLength = 5;
            this.txtPositionX.Name = "txtPositionX";
            this.txtPositionX.Size = new System.Drawing.Size(54, 23);
            this.txtPositionX.TabIndex = 0;
            this.txtPositionX.Tag = "X";
            this.txtPositionX.Text = "0";
            this.txtPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPositionX.WordWrap = false;
            this.txtPositionX.TextChanged += new System.EventHandler(this.ValidatePosition);
            this.txtPositionX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.txtPositionX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Position_MouseDown);
            this.txtPositionX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Position_MouseMove);
            this.txtPositionX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Position_MouseUp);
            // 
            // lblPositionMain
            // 
            this.lblPositionMain.Font = new System.Drawing.Font("Arial", 8F);
            this.lblPositionMain.Location = new System.Drawing.Point(6, 87);
            this.lblPositionMain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPositionMain.Name = "lblPositionMain";
            this.lblPositionMain.Size = new System.Drawing.Size(77, 14);
            this.lblPositionMain.TabIndex = 1;
            this.lblPositionMain.Text = "Position:";
            // 
            // lblPositionX
            // 
            this.lblPositionX.AutoSize = true;
            this.lblPositionX.Font = new System.Drawing.Font("Arial", 8F);
            this.lblPositionX.Location = new System.Drawing.Point(6, 110);
            this.lblPositionX.Name = "lblPositionX";
            this.lblPositionX.Size = new System.Drawing.Size(20, 16);
            this.lblPositionX.TabIndex = 2;
            this.lblPositionX.Text = "X:";
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Arial", 8F);
            this.lblName.Location = new System.Drawing.Point(6, 46);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(50, 14);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Arial", 8F);
            this.txtName.Location = new System.Drawing.Point(72, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(126, 23);
            this.txtName.TabIndex = 4;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NodeNameKeyPress);
            this.txtName.Leave += new System.EventHandler(this.NodeNameLeave);
            // 
            // lblPositionY
            // 
            this.lblPositionY.AutoSize = true;
            this.lblPositionY.Font = new System.Drawing.Font("Arial", 8F);
            this.lblPositionY.Location = new System.Drawing.Point(89, 110);
            this.lblPositionY.Name = "lblPositionY";
            this.lblPositionY.Size = new System.Drawing.Size(20, 16);
            this.lblPositionY.TabIndex = 6;
            this.lblPositionY.Text = "Y:";
            // 
            // txtPositionY
            // 
            this.txtPositionY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPositionY.Font = new System.Drawing.Font("Arial", 8F);
            this.txtPositionY.Location = new System.Drawing.Point(111, 110);
            this.txtPositionY.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPositionY.MaxLength = 5;
            this.txtPositionY.Name = "txtPositionY";
            this.txtPositionY.Size = new System.Drawing.Size(54, 23);
            this.txtPositionY.TabIndex = 5;
            this.txtPositionY.Tag = "Y";
            this.txtPositionY.Text = "0";
            this.txtPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPositionY.WordWrap = false;
            this.txtPositionY.TextChanged += new System.EventHandler(this.ValidatePosition);
            this.txtPositionY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.txtPositionY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Position_MouseDown);
            this.txtPositionY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Position_MouseMove);
            this.txtPositionY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Position_MouseUp);
            // 
            // lblPositionZ
            // 
            this.lblPositionZ.AutoSize = true;
            this.lblPositionZ.Font = new System.Drawing.Font("Arial", 8F);
            this.lblPositionZ.Location = new System.Drawing.Point(172, 110);
            this.lblPositionZ.Name = "lblPositionZ";
            this.lblPositionZ.Size = new System.Drawing.Size(20, 16);
            this.lblPositionZ.TabIndex = 8;
            this.lblPositionZ.Text = "Z:";
            // 
            // txtPositionZ
            // 
            this.txtPositionZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPositionZ.Font = new System.Drawing.Font("Arial", 8F);
            this.txtPositionZ.Location = new System.Drawing.Point(194, 110);
            this.txtPositionZ.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPositionZ.MaxLength = 5;
            this.txtPositionZ.Name = "txtPositionZ";
            this.txtPositionZ.Size = new System.Drawing.Size(54, 23);
            this.txtPositionZ.TabIndex = 7;
            this.txtPositionZ.Tag = "Z";
            this.txtPositionZ.Text = "0";
            this.txtPositionZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPositionZ.WordWrap = false;
            this.txtPositionZ.TextChanged += new System.EventHandler(this.ValidatePosition);
            this.txtPositionZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.txtPositionZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Position_MouseDown);
            this.txtPositionZ.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Position_MouseMove);
            this.txtPositionZ.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Position_MouseUp);
            // 
            // chkEnabled
            // 
            this.chkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkEnabled.Font = new System.Drawing.Font("Arial", 8F);
            this.chkEnabled.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEnabled.Location = new System.Drawing.Point(217, 45);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(98, 18);
            this.chkEnabled.TabIndex = 9;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.NodeEnabledChanged);
            // 
            // lblRotationZ
            // 
            this.lblRotationZ.AutoSize = true;
            this.lblRotationZ.Font = new System.Drawing.Font("Arial", 8F);
            this.lblRotationZ.Location = new System.Drawing.Point(172, 168);
            this.lblRotationZ.Name = "lblRotationZ";
            this.lblRotationZ.Size = new System.Drawing.Size(20, 16);
            this.lblRotationZ.TabIndex = 16;
            this.lblRotationZ.Text = "Z:";
            // 
            // txtRotationZ
            // 
            this.txtRotationZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRotationZ.Font = new System.Drawing.Font("Arial", 8F);
            this.txtRotationZ.Location = new System.Drawing.Point(194, 168);
            this.txtRotationZ.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtRotationZ.Name = "txtRotationZ";
            this.txtRotationZ.Size = new System.Drawing.Size(54, 23);
            this.txtRotationZ.TabIndex = 15;
            this.txtRotationZ.Tag = "Z";
            this.txtRotationZ.Text = "0";
            this.txtRotationZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRotationZ.WordWrap = false;
            this.txtRotationZ.TextChanged += new System.EventHandler(this.ValidateRotation);
            this.txtRotationZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.txtRotationZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Rotation_MouseDown);
            this.txtRotationZ.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Rotation_MouseMove);
            this.txtRotationZ.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Rotation_MouseUp);
            // 
            // lblRotationY
            // 
            this.lblRotationY.AutoSize = true;
            this.lblRotationY.Font = new System.Drawing.Font("Arial", 8F);
            this.lblRotationY.Location = new System.Drawing.Point(89, 168);
            this.lblRotationY.Name = "lblRotationY";
            this.lblRotationY.Size = new System.Drawing.Size(20, 16);
            this.lblRotationY.TabIndex = 14;
            this.lblRotationY.Text = "Y:";
            // 
            // txtRotationY
            // 
            this.txtRotationY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRotationY.Font = new System.Drawing.Font("Arial", 8F);
            this.txtRotationY.Location = new System.Drawing.Point(111, 168);
            this.txtRotationY.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtRotationY.Name = "txtRotationY";
            this.txtRotationY.Size = new System.Drawing.Size(54, 23);
            this.txtRotationY.TabIndex = 13;
            this.txtRotationY.Tag = "Y";
            this.txtRotationY.Text = "0";
            this.txtRotationY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRotationY.WordWrap = false;
            this.txtRotationY.TextChanged += new System.EventHandler(this.ValidateRotation);
            this.txtRotationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.txtRotationY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Rotation_MouseDown);
            this.txtRotationY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Rotation_MouseMove);
            this.txtRotationY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Rotation_MouseUp);
            // 
            // lblRotationX
            // 
            this.lblRotationX.AutoSize = true;
            this.lblRotationX.Font = new System.Drawing.Font("Arial", 8F);
            this.lblRotationX.Location = new System.Drawing.Point(6, 168);
            this.lblRotationX.Name = "lblRotationX";
            this.lblRotationX.Size = new System.Drawing.Size(20, 16);
            this.lblRotationX.TabIndex = 12;
            this.lblRotationX.Text = "X:";
            // 
            // lblRotationMain
            // 
            this.lblRotationMain.Font = new System.Drawing.Font("Arial", 8F);
            this.lblRotationMain.Location = new System.Drawing.Point(6, 145);
            this.lblRotationMain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRotationMain.Name = "lblRotationMain";
            this.lblRotationMain.Size = new System.Drawing.Size(77, 14);
            this.lblRotationMain.TabIndex = 11;
            this.lblRotationMain.Text = "Rotation:";
            // 
            // txtRotationX
            // 
            this.txtRotationX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRotationX.Font = new System.Drawing.Font("Arial", 8F);
            this.txtRotationX.Location = new System.Drawing.Point(28, 168);
            this.txtRotationX.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtRotationX.Name = "txtRotationX";
            this.txtRotationX.Size = new System.Drawing.Size(54, 23);
            this.txtRotationX.TabIndex = 10;
            this.txtRotationX.Tag = "X";
            this.txtRotationX.Text = "0";
            this.txtRotationX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRotationX.WordWrap = false;
            this.txtRotationX.TextChanged += new System.EventHandler(this.ValidateRotation);
            this.txtRotationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.txtRotationX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Rotation_MouseDown);
            this.txtRotationX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Rotation_MouseMove);
            this.txtRotationX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Rotation_MouseUp);
            // 
            // lblScaleZ
            // 
            this.lblScaleZ.AutoSize = true;
            this.lblScaleZ.Font = new System.Drawing.Font("Arial", 8F);
            this.lblScaleZ.Location = new System.Drawing.Point(172, 227);
            this.lblScaleZ.Name = "lblScaleZ";
            this.lblScaleZ.Size = new System.Drawing.Size(20, 16);
            this.lblScaleZ.TabIndex = 23;
            this.lblScaleZ.Text = "Z:";
            // 
            // txtScaleZ
            // 
            this.txtScaleZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScaleZ.Font = new System.Drawing.Font("Arial", 8F);
            this.txtScaleZ.Location = new System.Drawing.Point(194, 227);
            this.txtScaleZ.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtScaleZ.Name = "txtScaleZ";
            this.txtScaleZ.Size = new System.Drawing.Size(54, 23);
            this.txtScaleZ.TabIndex = 22;
            this.txtScaleZ.Tag = "Z";
            this.txtScaleZ.Text = "1";
            this.txtScaleZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtScaleZ.WordWrap = false;
            this.txtScaleZ.TextChanged += new System.EventHandler(this.ValidateScale);
            this.txtScaleZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.txtScaleZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Scale_MouseDown);
            this.txtScaleZ.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scale_MouseMove);
            this.txtScaleZ.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Scale_MouseUp);
            // 
            // lblScaleY
            // 
            this.lblScaleY.AutoSize = true;
            this.lblScaleY.Font = new System.Drawing.Font("Arial", 8F);
            this.lblScaleY.Location = new System.Drawing.Point(89, 227);
            this.lblScaleY.Name = "lblScaleY";
            this.lblScaleY.Size = new System.Drawing.Size(20, 16);
            this.lblScaleY.TabIndex = 21;
            this.lblScaleY.Text = "Y:";
            // 
            // txtScaleY
            // 
            this.txtScaleY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScaleY.Font = new System.Drawing.Font("Arial", 8F);
            this.txtScaleY.Location = new System.Drawing.Point(111, 227);
            this.txtScaleY.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtScaleY.Name = "txtScaleY";
            this.txtScaleY.Size = new System.Drawing.Size(54, 23);
            this.txtScaleY.TabIndex = 20;
            this.txtScaleY.Tag = "Y";
            this.txtScaleY.Text = "1";
            this.txtScaleY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtScaleY.WordWrap = false;
            this.txtScaleY.TextChanged += new System.EventHandler(this.ValidateScale);
            this.txtScaleY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.txtScaleY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Scale_MouseDown);
            this.txtScaleY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scale_MouseMove);
            this.txtScaleY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Scale_MouseUp);
            // 
            // lblScaleX
            // 
            this.lblScaleX.AutoSize = true;
            this.lblScaleX.Font = new System.Drawing.Font("Arial", 8F);
            this.lblScaleX.Location = new System.Drawing.Point(6, 227);
            this.lblScaleX.Name = "lblScaleX";
            this.lblScaleX.Size = new System.Drawing.Size(20, 16);
            this.lblScaleX.TabIndex = 19;
            this.lblScaleX.Text = "X:";
            // 
            // lblScaleMain
            // 
            this.lblScaleMain.Font = new System.Drawing.Font("Arial", 8F);
            this.lblScaleMain.Location = new System.Drawing.Point(6, 204);
            this.lblScaleMain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScaleMain.Name = "lblScaleMain";
            this.lblScaleMain.Size = new System.Drawing.Size(77, 14);
            this.lblScaleMain.TabIndex = 18;
            this.lblScaleMain.Text = "Scale:";
            // 
            // txtScaleX
            // 
            this.txtScaleX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScaleX.Font = new System.Drawing.Font("Arial", 8F);
            this.txtScaleX.Location = new System.Drawing.Point(28, 227);
            this.txtScaleX.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtScaleX.Name = "txtScaleX";
            this.txtScaleX.Size = new System.Drawing.Size(54, 23);
            this.txtScaleX.TabIndex = 17;
            this.txtScaleX.Tag = "X";
            this.txtScaleX.Text = "1";
            this.txtScaleX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtScaleX.WordWrap = false;
            this.txtScaleX.TextChanged += new System.EventHandler(this.ValidateScale);
            this.txtScaleX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.txtScaleX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Scale_MouseDown);
            this.txtScaleX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scale_MouseMove);
            this.txtScaleX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Scale_MouseUp);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(330, 32);
            this.lblHeader.TabIndex = 27;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.Yellow;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Location = new System.Drawing.Point(3, 3);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(31, 26);
            this.btnView.TabIndex = 28;
            this.btnView.Text = "^";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(42, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.TabIndex = 29;
            this.label1.Text = "Node";
            // 
            // BasicNodeProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblScaleZ);
            this.Controls.Add(this.txtScaleZ);
            this.Controls.Add(this.lblScaleY);
            this.Controls.Add(this.txtScaleY);
            this.Controls.Add(this.lblScaleX);
            this.Controls.Add(this.lblScaleMain);
            this.Controls.Add(this.txtScaleX);
            this.Controls.Add(this.lblRotationZ);
            this.Controls.Add(this.txtRotationZ);
            this.Controls.Add(this.lblRotationY);
            this.Controls.Add(this.txtRotationY);
            this.Controls.Add(this.lblRotationX);
            this.Controls.Add(this.lblRotationMain);
            this.Controls.Add(this.txtRotationX);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.lblPositionZ);
            this.Controls.Add(this.txtPositionZ);
            this.Controls.Add(this.lblPositionY);
            this.Controls.Add(this.txtPositionY);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPositionX);
            this.Controls.Add(this.lblPositionMain);
            this.Controls.Add(this.txtPositionX);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "BasicNodeProperties";
            this.Size = new System.Drawing.Size(333, 269);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPositionX;
        private System.Windows.Forms.Label lblPositionMain;
        private System.Windows.Forms.Label lblPositionX;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPositionY;
        private System.Windows.Forms.TextBox txtPositionY;
        private System.Windows.Forms.Label lblPositionZ;
        private System.Windows.Forms.TextBox txtPositionZ;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Label lblRotationZ;
        private System.Windows.Forms.TextBox txtRotationZ;
        private System.Windows.Forms.Label lblRotationY;
        private System.Windows.Forms.TextBox txtRotationY;
        private System.Windows.Forms.Label lblRotationX;
        private System.Windows.Forms.Label lblRotationMain;
        private System.Windows.Forms.TextBox txtRotationX;
        private System.Windows.Forms.Label lblScaleZ;
        private System.Windows.Forms.TextBox txtScaleZ;
        private System.Windows.Forms.Label lblScaleY;
        private System.Windows.Forms.TextBox txtScaleY;
        private System.Windows.Forms.Label lblScaleX;
        private System.Windows.Forms.Label lblScaleMain;
        private System.Windows.Forms.TextBox txtScaleX;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label1;
    }
}
