namespace Pulsar
{
    partial class PulsarActionTransformSpace
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
            this.groupTransformSpace = new System.Windows.Forms.GroupBox();
            this.radioTransformSpaceWorld = new System.Windows.Forms.RadioButton();
            this.radioTransformSpaceParent = new System.Windows.Forms.RadioButton();
            this.radioTransformSpaceLocal = new System.Windows.Forms.RadioButton();
            this.groupTransformSpace.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupTransformSpace
            // 
            this.groupTransformSpace.Controls.Add(this.radioTransformSpaceWorld);
            this.groupTransformSpace.Controls.Add(this.radioTransformSpaceParent);
            this.groupTransformSpace.Controls.Add(this.radioTransformSpaceLocal);
            this.groupTransformSpace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupTransformSpace.Location = new System.Drawing.Point(0, 0);
            this.groupTransformSpace.Name = "groupTransformSpace";
            this.groupTransformSpace.Size = new System.Drawing.Size(124, 102);
            this.groupTransformSpace.TabIndex = 34;
            this.groupTransformSpace.TabStop = false;
            this.groupTransformSpace.Text = "Transform space";
            // 
            // radioTransformSpaceWorld
            // 
            this.radioTransformSpaceWorld.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioTransformSpaceWorld.Location = new System.Drawing.Point(9, 67);
            this.radioTransformSpaceWorld.Name = "radioTransformSpaceWorld";
            this.radioTransformSpaceWorld.Size = new System.Drawing.Size(86, 18);
            this.radioTransformSpaceWorld.TabIndex = 2;
            this.radioTransformSpaceWorld.TabStop = true;
            this.radioTransformSpaceWorld.Text = "World";
            this.radioTransformSpaceWorld.UseVisualStyleBackColor = true;
            this.radioTransformSpaceWorld.CheckedChanged += new System.EventHandler(this.TransformSpace_Changed);
            // 
            // radioTransformSpaceParent
            // 
            this.radioTransformSpaceParent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioTransformSpaceParent.Location = new System.Drawing.Point(9, 43);
            this.radioTransformSpaceParent.Name = "radioTransformSpaceParent";
            this.radioTransformSpaceParent.Size = new System.Drawing.Size(86, 18);
            this.radioTransformSpaceParent.TabIndex = 1;
            this.radioTransformSpaceParent.TabStop = true;
            this.radioTransformSpaceParent.Text = "Parent";
            this.radioTransformSpaceParent.UseVisualStyleBackColor = true;
            this.radioTransformSpaceParent.CheckedChanged += new System.EventHandler(this.TransformSpace_Changed);
            // 
            // radioTransformSpaceLocal
            // 
            this.radioTransformSpaceLocal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioTransformSpaceLocal.Checked = true;
            this.radioTransformSpaceLocal.Location = new System.Drawing.Point(9, 19);
            this.radioTransformSpaceLocal.Name = "radioTransformSpaceLocal";
            this.radioTransformSpaceLocal.Size = new System.Drawing.Size(86, 18);
            this.radioTransformSpaceLocal.TabIndex = 0;
            this.radioTransformSpaceLocal.TabStop = true;
            this.radioTransformSpaceLocal.Text = "Local";
            this.radioTransformSpaceLocal.UseVisualStyleBackColor = true;
            this.radioTransformSpaceLocal.CheckedChanged += new System.EventHandler(this.TransformSpace_Changed);
            // 
            // PulsarActionTransformSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupTransformSpace);
            this.Name = "PulsarActionTransformSpace";
            this.Size = new System.Drawing.Size(125, 103);
            this.groupTransformSpace.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupTransformSpace;
        private System.Windows.Forms.RadioButton radioTransformSpaceWorld;
        private System.Windows.Forms.RadioButton radioTransformSpaceParent;
        private System.Windows.Forms.RadioButton radioTransformSpaceLocal;
    }
}
