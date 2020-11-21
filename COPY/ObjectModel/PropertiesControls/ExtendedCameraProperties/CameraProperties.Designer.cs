namespace ExtendedCameraProperties
{
    partial class CameraProperties
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.label1111 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAspectRatio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAutoAspectRatio = new System.Windows.Forms.CheckBox();
            this.txtFarClip = new System.Windows.Forms.TextBox();
            this.txtNearClip = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkUseClipping = new System.Windows.Forms.CheckBox();
            this.chkFlipVertical = new System.Windows.Forms.CheckBox();
            this.txtSkew = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkOrthographic = new System.Windows.Forms.CheckBox();
            this.txtOrthographicSize = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFieldOfView = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLODBias = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtZoom = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(333, 32);
            this.lblHeader.TabIndex = 0;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.Yellow;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Location = new System.Drawing.Point(3, 3);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(31, 26);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "^";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label1111
            // 
            this.label1111.AutoSize = true;
            this.label1111.BackColor = System.Drawing.Color.White;
            this.label1111.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1111.Location = new System.Drawing.Point(40, 4);
            this.label1111.Name = "label1111";
            this.label1111.Size = new System.Drawing.Size(82, 22);
            this.label1111.TabIndex = 2;
            this.label1111.Text = "Camera";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Aspect ratio:";
            // 
            // txtAspectRatio
            // 
            this.txtAspectRatio.Location = new System.Drawing.Point(190, 39);
            this.txtAspectRatio.Name = "txtAspectRatio";
            this.txtAspectRatio.Size = new System.Drawing.Size(110, 22);
            this.txtAspectRatio.TabIndex = 4;
            this.txtAspectRatio.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateAspectRatio);
            this.txtAspectRatio.Validated += new System.EventHandler(this.OnAspectRatioChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Far clip:";
            // 
            // chkAutoAspectRatio
            // 
            this.chkAutoAspectRatio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoAspectRatio.Location = new System.Drawing.Point(2, 67);
            this.chkAutoAspectRatio.Name = "chkAutoAspectRatio";
            this.chkAutoAspectRatio.Size = new System.Drawing.Size(298, 21);
            this.chkAutoAspectRatio.TabIndex = 6;
            this.chkAutoAspectRatio.Text = "Auto Aspect Ratio:";
            this.chkAutoAspectRatio.UseVisualStyleBackColor = true;
            this.chkAutoAspectRatio.CheckedChanged += new System.EventHandler(this.OnAspectRatioChanged);
            // 
            // txtFarClip
            // 
            this.txtFarClip.Location = new System.Drawing.Point(190, 94);
            this.txtFarClip.Name = "txtFarClip";
            this.txtFarClip.Size = new System.Drawing.Size(110, 22);
            this.txtFarClip.TabIndex = 7;
            this.txtFarClip.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateFarClip);
            this.txtFarClip.Validated += new System.EventHandler(this.OnFarClipChanged);
            // 
            // txtNearClip
            // 
            this.txtNearClip.Location = new System.Drawing.Point(190, 122);
            this.txtNearClip.Name = "txtNearClip";
            this.txtNearClip.Size = new System.Drawing.Size(110, 22);
            this.txtNearClip.TabIndex = 9;
            this.txtNearClip.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateNearClip);
            this.txtNearClip.Validated += new System.EventHandler(this.OnNearClipChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Near clip:";
            // 
            // chkUseClipping
            // 
            this.chkUseClipping.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseClipping.Location = new System.Drawing.Point(3, 150);
            this.chkUseClipping.Name = "chkUseClipping";
            this.chkUseClipping.Size = new System.Drawing.Size(297, 21);
            this.chkUseClipping.TabIndex = 10;
            this.chkUseClipping.Text = "Use clipping:";
            this.chkUseClipping.UseVisualStyleBackColor = true;
            this.chkUseClipping.CheckedChanged += new System.EventHandler(this.OnUseClippingChanged);
            // 
            // chkFlipVertical
            // 
            this.chkFlipVertical.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFlipVertical.Location = new System.Drawing.Point(3, 177);
            this.chkFlipVertical.Name = "chkFlipVertical";
            this.chkFlipVertical.Size = new System.Drawing.Size(297, 21);
            this.chkFlipVertical.TabIndex = 11;
            this.chkFlipVertical.Text = "Flip vertical:";
            this.chkFlipVertical.UseVisualStyleBackColor = true;
            this.chkFlipVertical.CheckedChanged += new System.EventHandler(this.OnFlipVerticalChanged);
            // 
            // txtSkew
            // 
            this.txtSkew.Location = new System.Drawing.Point(190, 204);
            this.txtSkew.Name = "txtSkew";
            this.txtSkew.Size = new System.Drawing.Size(110, 22);
            this.txtSkew.TabIndex = 13;
            this.txtSkew.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateSkew);
            this.txtSkew.Validated += new System.EventHandler(this.OnSkewChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Skew:";
            // 
            // chkOrthographic
            // 
            this.chkOrthographic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOrthographic.Location = new System.Drawing.Point(2, 232);
            this.chkOrthographic.Name = "chkOrthographic";
            this.chkOrthographic.Size = new System.Drawing.Size(298, 21);
            this.chkOrthographic.TabIndex = 16;
            this.chkOrthographic.Text = "Orthographic:";
            this.chkOrthographic.UseVisualStyleBackColor = true;
            this.chkOrthographic.CheckedChanged += new System.EventHandler(this.OnOrthographicChanged);
            // 
            // txtOrthographicSize
            // 
            this.txtOrthographicSize.Location = new System.Drawing.Point(190, 259);
            this.txtOrthographicSize.Name = "txtOrthographicSize";
            this.txtOrthographicSize.Size = new System.Drawing.Size(110, 22);
            this.txtOrthographicSize.TabIndex = 18;
            this.txtOrthographicSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateOrthographicSize);
            this.txtOrthographicSize.Validated += new System.EventHandler(this.OnOrthographicSizeChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Orthographic Size:";
            // 
            // txtFieldOfView
            // 
            this.txtFieldOfView.Location = new System.Drawing.Point(190, 287);
            this.txtFieldOfView.Name = "txtFieldOfView";
            this.txtFieldOfView.Size = new System.Drawing.Size(110, 22);
            this.txtFieldOfView.TabIndex = 20;
            this.txtFieldOfView.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateFieldOfView);
            this.txtFieldOfView.Validated += new System.EventHandler(this.OnFieldOfViewChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "Field Of View:";
            // 
            // txtLODBias
            // 
            this.txtLODBias.Location = new System.Drawing.Point(190, 315);
            this.txtLODBias.Name = "txtLODBias";
            this.txtLODBias.Size = new System.Drawing.Size(110, 22);
            this.txtLODBias.TabIndex = 22;
            this.txtLODBias.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateLODBias);
            this.txtLODBias.Validated += new System.EventHandler(this.OnLODBiasChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 318);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 21;
            this.label9.Text = "LOD Bias:";
            // 
            // txtZoom
            // 
            this.txtZoom.Location = new System.Drawing.Point(190, 343);
            this.txtZoom.Name = "txtZoom";
            this.txtZoom.Size = new System.Drawing.Size(110, 22);
            this.txtZoom.TabIndex = 24;
            this.txtZoom.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateZoom);
            this.txtZoom.Validated += new System.EventHandler(this.OnZoomChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 346);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "Zoom:";
            // 
            // CameraProperties
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtZoom);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtLODBias);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtFieldOfView);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtOrthographicSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkOrthographic);
            this.Controls.Add(this.txtSkew);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkFlipVertical);
            this.Controls.Add(this.chkUseClipping);
            this.Controls.Add(this.txtNearClip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFarClip);
            this.Controls.Add(this.chkAutoAspectRatio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAspectRatio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1111);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CameraProperties";
            this.Size = new System.Drawing.Size(332, 385);
            this.Resize += new System.EventHandler(this.CameraProperties_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label1111;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAspectRatio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAutoAspectRatio;
        private System.Windows.Forms.TextBox txtFarClip;
        private System.Windows.Forms.TextBox txtNearClip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkUseClipping;
        private System.Windows.Forms.CheckBox chkFlipVertical;
        private System.Windows.Forms.TextBox txtSkew;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkOrthographic;
        private System.Windows.Forms.TextBox txtOrthographicSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFieldOfView;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLODBias;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtZoom;
        private System.Windows.Forms.Label label10;
    }
}
