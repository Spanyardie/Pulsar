namespace ExtendedLightProperties
{
    partial class LightProperties
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
            this.colorPicker = new System.Windows.Forms.ColorDialog();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblColorView = new System.Windows.Forms.Label();
            this.btnColorPick = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lstLightType = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAspectRatio = new System.Windows.Forms.TextBox();
            this.txtBrightness = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkUsePhysicalValues = new System.Windows.Forms.CheckBox();
            this.txtEffectiveSpecularIntensity = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSpecularIntensity = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFadeDistance = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFieldOfView = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkPerVertex = new System.Windows.Forms.CheckBox();
            this.txtRadius = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtShadowFadeDistance = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtShadowIntensity = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtShadowMaximumExtrusion = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtShadowNearFarRatio = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtShadowResolution = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(332, 32);
            this.lblHeader.TabIndex = 0;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.Yellow;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.ForeColor = System.Drawing.Color.Black;
            this.btnView.Location = new System.Drawing.Point(3, 3);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(31, 26);
            this.btnView.TabIndex = 1;
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
            this.label1.Size = new System.Drawing.Size(57, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Light";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(3, 41);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(54, 16);
            this.lblColor.TabIndex = 3;
            this.lblColor.Text = "Colour:";
            // 
            // lblColorView
            // 
            this.lblColorView.BackColor = System.Drawing.Color.White;
            this.lblColorView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblColorView.Location = new System.Drawing.Point(182, 41);
            this.lblColorView.Name = "lblColorView";
            this.lblColorView.Size = new System.Drawing.Size(125, 17);
            this.lblColorView.TabIndex = 4;
            // 
            // btnColorPick
            // 
            this.btnColorPick.BackColor = System.Drawing.Color.Blue;
            this.btnColorPick.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColorPick.ForeColor = System.Drawing.Color.White;
            this.btnColorPick.Location = new System.Drawing.Point(309, 39);
            this.btnColorPick.Name = "btnColorPick";
            this.btnColorPick.Size = new System.Drawing.Size(21, 23);
            this.btnColorPick.TabIndex = 9;
            this.btnColorPick.Text = "P";
            this.btnColorPick.UseVisualStyleBackColor = false;
            this.btnColorPick.Click += new System.EventHandler(this.btnColorPick_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Type:";
            // 
            // lstLightType
            // 
            this.lstLightType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLightType.DisplayMember = "_type";
            this.lstLightType.FormattingEnabled = true;
            this.lstLightType.ItemHeight = 16;
            this.lstLightType.Items.AddRange(new object[] {
            "Directional",
            "Point",
            "Spot"});
            this.lstLightType.Location = new System.Drawing.Point(182, 66);
            this.lstLightType.Name = "lstLightType";
            this.lstLightType.Size = new System.Drawing.Size(125, 50);
            this.lstLightType.TabIndex = 13;
            this.lstLightType.ValueMember = "_lightType";
            this.lstLightType.SelectedIndexChanged += new System.EventHandler(this.LightType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Aspect ratio:";
            // 
            // txtAspectRatio
            // 
            this.txtAspectRatio.Location = new System.Drawing.Point(182, 125);
            this.txtAspectRatio.Name = "txtAspectRatio";
            this.txtAspectRatio.Size = new System.Drawing.Size(125, 22);
            this.txtAspectRatio.TabIndex = 15;
            this.txtAspectRatio.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateAspectRatio);
            this.txtAspectRatio.Validated += new System.EventHandler(this.OnAspectRatioChanged);
            // 
            // txtBrightness
            // 
            this.txtBrightness.Location = new System.Drawing.Point(182, 151);
            this.txtBrightness.Name = "txtBrightness";
            this.txtBrightness.Size = new System.Drawing.Size(125, 22);
            this.txtBrightness.TabIndex = 17;
            this.txtBrightness.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateBrightness);
            this.txtBrightness.Validated += new System.EventHandler(this.OnBrightnessChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Brightness:";
            // 
            // txtTemperature
            // 
            this.txtTemperature.Location = new System.Drawing.Point(182, 177);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(125, 22);
            this.txtTemperature.TabIndex = 19;
            this.txtTemperature.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateTemperature);
            this.txtTemperature.Validated += new System.EventHandler(this.OnTemperatureChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 16);
            this.label7.TabIndex = 18;
            this.label7.Text = "Temperature:";
            // 
            // chkUsePhysicalValues
            // 
            this.chkUsePhysicalValues.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUsePhysicalValues.Location = new System.Drawing.Point(3, 205);
            this.chkUsePhysicalValues.Name = "chkUsePhysicalValues";
            this.chkUsePhysicalValues.Size = new System.Drawing.Size(304, 24);
            this.chkUsePhysicalValues.TabIndex = 20;
            this.chkUsePhysicalValues.Text = "Use physical values:";
            this.chkUsePhysicalValues.UseVisualStyleBackColor = true;
            this.chkUsePhysicalValues.CheckedChanged += new System.EventHandler(this.OnUsePhysicalValuesChanged);
            // 
            // txtEffectiveSpecularIntensity
            // 
            this.txtEffectiveSpecularIntensity.Location = new System.Drawing.Point(182, 239);
            this.txtEffectiveSpecularIntensity.Name = "txtEffectiveSpecularIntensity";
            this.txtEffectiveSpecularIntensity.Size = new System.Drawing.Size(125, 22);
            this.txtEffectiveSpecularIntensity.TabIndex = 22;
            this.txtEffectiveSpecularIntensity.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateEffectiveSpecularIntensity);
            this.txtEffectiveSpecularIntensity.Validated += new System.EventHandler(this.OnEffectiveSpecularIntensityChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 242);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(180, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Effective specular intensity:";
            // 
            // txtSpecularIntensity
            // 
            this.txtSpecularIntensity.Location = new System.Drawing.Point(182, 266);
            this.txtSpecularIntensity.Name = "txtSpecularIntensity";
            this.txtSpecularIntensity.Size = new System.Drawing.Size(125, 22);
            this.txtSpecularIntensity.TabIndex = 24;
            this.txtSpecularIntensity.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateSpecularIntensity);
            this.txtSpecularIntensity.Validated += new System.EventHandler(this.OnSpecularIntensityChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 269);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Specular intensity:";
            // 
            // txtFadeDistance
            // 
            this.txtFadeDistance.Location = new System.Drawing.Point(182, 292);
            this.txtFadeDistance.Name = "txtFadeDistance";
            this.txtFadeDistance.Size = new System.Drawing.Size(125, 22);
            this.txtFadeDistance.TabIndex = 26;
            this.txtFadeDistance.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateFadeDistance);
            this.txtFadeDistance.Validated += new System.EventHandler(this.OnFadeDistanceChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 295);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 16);
            this.label10.TabIndex = 25;
            this.label10.Text = "Fade distance:";
            // 
            // txtFieldOfView
            // 
            this.txtFieldOfView.Location = new System.Drawing.Point(182, 318);
            this.txtFieldOfView.Name = "txtFieldOfView";
            this.txtFieldOfView.Size = new System.Drawing.Size(125, 22);
            this.txtFieldOfView.TabIndex = 28;
            this.txtFieldOfView.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateFieldOfView);
            this.txtFieldOfView.Validated += new System.EventHandler(this.OnFieldOfViewChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 321);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 16);
            this.label11.TabIndex = 27;
            this.label11.Text = "Field of view:";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(182, 344);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(125, 22);
            this.txtLength.TabIndex = 30;
            this.txtLength.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateLength);
            this.txtLength.Validated += new System.EventHandler(this.OnLengthChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 347);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 16);
            this.label12.TabIndex = 29;
            this.label12.Text = "Length:";
            // 
            // chkPerVertex
            // 
            this.chkPerVertex.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPerVertex.Location = new System.Drawing.Point(3, 372);
            this.chkPerVertex.Name = "chkPerVertex";
            this.chkPerVertex.Size = new System.Drawing.Size(304, 24);
            this.chkPerVertex.TabIndex = 31;
            this.chkPerVertex.Text = "Per vertex:";
            this.chkPerVertex.UseVisualStyleBackColor = true;
            this.chkPerVertex.CheckedChanged += new System.EventHandler(this.OnPerVertexChanged);
            // 
            // txtRadius
            // 
            this.txtRadius.Location = new System.Drawing.Point(182, 406);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(125, 22);
            this.txtRadius.TabIndex = 33;
            this.txtRadius.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateRadius);
            this.txtRadius.Validated += new System.EventHandler(this.OnRadiusChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 409);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 16);
            this.label13.TabIndex = 32;
            this.label13.Text = "Radius:";
            // 
            // txtRange
            // 
            this.txtRange.Location = new System.Drawing.Point(182, 432);
            this.txtRange.Name = "txtRange";
            this.txtRange.Size = new System.Drawing.Size(125, 22);
            this.txtRange.TabIndex = 35;
            this.txtRange.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateRange);
            this.txtRange.Validated += new System.EventHandler(this.OnRangeChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 435);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 16);
            this.label14.TabIndex = 34;
            this.label14.Text = "Range:";
            // 
            // txtShadowFadeDistance
            // 
            this.txtShadowFadeDistance.Location = new System.Drawing.Point(182, 459);
            this.txtShadowFadeDistance.Name = "txtShadowFadeDistance";
            this.txtShadowFadeDistance.Size = new System.Drawing.Size(125, 22);
            this.txtShadowFadeDistance.TabIndex = 37;
            this.txtShadowFadeDistance.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateShadowFadeDistance);
            this.txtShadowFadeDistance.Validated += new System.EventHandler(this.OnShadowFadeDistanceChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 462);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(151, 16);
            this.label15.TabIndex = 36;
            this.label15.Text = "Shadow fade distance:";
            // 
            // txtShadowIntensity
            // 
            this.txtShadowIntensity.Location = new System.Drawing.Point(182, 485);
            this.txtShadowIntensity.Name = "txtShadowIntensity";
            this.txtShadowIntensity.Size = new System.Drawing.Size(125, 22);
            this.txtShadowIntensity.TabIndex = 39;
            this.txtShadowIntensity.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateShadowIntensity);
            this.txtShadowIntensity.Validated += new System.EventHandler(this.OnShadowIntensityChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 488);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(118, 16);
            this.label16.TabIndex = 38;
            this.label16.Text = "Shadow intensity:";
            // 
            // txtShadowMaximumExtrusion
            // 
            this.txtShadowMaximumExtrusion.Location = new System.Drawing.Point(182, 511);
            this.txtShadowMaximumExtrusion.Name = "txtShadowMaximumExtrusion";
            this.txtShadowMaximumExtrusion.Size = new System.Drawing.Size(125, 22);
            this.txtShadowMaximumExtrusion.TabIndex = 41;
            this.txtShadowMaximumExtrusion.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateShadowMaximumExtrusion);
            this.txtShadowMaximumExtrusion.Validated += new System.EventHandler(this.OnShadowMaximumExtrusionChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 514);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(156, 16);
            this.label17.TabIndex = 40;
            this.label17.Text = "Shadow max. extrusion:";
            // 
            // txtShadowNearFarRatio
            // 
            this.txtShadowNearFarRatio.Location = new System.Drawing.Point(182, 537);
            this.txtShadowNearFarRatio.Name = "txtShadowNearFarRatio";
            this.txtShadowNearFarRatio.Size = new System.Drawing.Size(125, 22);
            this.txtShadowNearFarRatio.TabIndex = 43;
            this.txtShadowNearFarRatio.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateShadowNearFarRatio);
            this.txtShadowNearFarRatio.Validated += new System.EventHandler(this.OnShadowNearFarRatioChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 540);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(148, 16);
            this.label18.TabIndex = 42;
            this.label18.Text = "Shadow near/far ratio:";
            // 
            // txtShadowResolution
            // 
            this.txtShadowResolution.Location = new System.Drawing.Point(182, 563);
            this.txtShadowResolution.Name = "txtShadowResolution";
            this.txtShadowResolution.Size = new System.Drawing.Size(125, 22);
            this.txtShadowResolution.TabIndex = 45;
            this.txtShadowResolution.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateShadowResolution);
            this.txtShadowResolution.Validated += new System.EventHandler(this.OnShadowResolutionChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 566);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(128, 16);
            this.label19.TabIndex = 44;
            this.label19.Text = "Shadow resolution:";
            // 
            // LightProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtShadowResolution);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtShadowNearFarRatio);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtShadowMaximumExtrusion);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtShadowIntensity);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtShadowFadeDistance);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtRange);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtRadius);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.chkPerVertex);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtFieldOfView);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtFadeDistance);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSpecularIntensity);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEffectiveSpecularIntensity);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chkUsePhysicalValues);
            this.Controls.Add(this.txtTemperature);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBrightness);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAspectRatio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lstLightType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnColorPick);
            this.Controls.Add(this.lblColorView);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LightProperties";
            this.Size = new System.Drawing.Size(332, 604);
            this.Resize += new System.EventHandler(this.LightProperties_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorPicker;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblColorView;
        private System.Windows.Forms.Button btnColorPick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstLightType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAspectRatio;
        private System.Windows.Forms.TextBox txtBrightness;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkUsePhysicalValues;
        private System.Windows.Forms.TextBox txtEffectiveSpecularIntensity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSpecularIntensity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFadeDistance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFieldOfView;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkPerVertex;
        private System.Windows.Forms.TextBox txtRadius;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtRange;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtShadowFadeDistance;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtShadowIntensity;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtShadowMaximumExtrusion;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtShadowNearFarRatio;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtShadowResolution;
        private System.Windows.Forms.Label label19;
    }
}
