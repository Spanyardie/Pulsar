namespace Pulsar
{
    partial class PulsarActionsSetup
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
                _actionTargetControl?.Dispose();
                _basicPropertyControl?.Dispose();
                _bezierConfigControl?.Dispose();
                _nodeActions?.Dispose();
                _vector3PropertyControl?.Dispose();
                _transformSpaceControl?.Dispose();
                _vector4PropertyControl?.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.actionsList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEntity = new System.Windows.Forms.Label();
            this.radioSingle = new System.Windows.Forms.RadioButton();
            this.radioSequence = new System.Windows.Forms.RadioButton();
            this.radioParallel = new System.Windows.Forms.RadioButton();
            this.treeViewActions = new System.Windows.Forms.TreeView();
            this.contextEntityAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextEntityActionDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextEntityActionEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextEntityActionCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.lblEntityActions = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.actionPropertiesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.chkRetainAction = new System.Windows.Forms.CheckBox();
            this.lblActionProperties = new System.Windows.Forms.Label();
            this.radioPlus = new System.Windows.Forms.RadioButton();
            this.lblMode = new System.Windows.Forms.Label();
            this.radioPlusPlus = new System.Windows.Forms.RadioButton();
            this.btnComplete = new System.Windows.Forms.Button();
            this.contextEntityAction.SuspendLayout();
            this.panel1.SuspendLayout();
            this.actionPropertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // actionsList
            // 
            this.actionsList.FormattingEnabled = true;
            this.actionsList.ItemHeight = 14;
            this.actionsList.Location = new System.Drawing.Point(15, 105);
            this.actionsList.Name = "actionsList";
            this.actionsList.Size = new System.Drawing.Size(145, 382);
            this.actionsList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Action:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(693, 78);
            this.label2.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 18);
            this.label3.TabIndex = 42;
            this.label3.Text = "Entity:";
            // 
            // lblEntity
            // 
            this.lblEntity.BackColor = System.Drawing.Color.White;
            this.lblEntity.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntity.Location = new System.Drawing.Point(68, 9);
            this.lblEntity.Name = "lblEntity";
            this.lblEntity.Size = new System.Drawing.Size(212, 18);
            this.lblEntity.TabIndex = 43;
            // 
            // radioSingle
            // 
            this.radioSingle.AutoSize = true;
            this.radioSingle.BackColor = System.Drawing.Color.White;
            this.radioSingle.Checked = true;
            this.radioSingle.Location = new System.Drawing.Point(15, 43);
            this.radioSingle.Name = "radioSingle";
            this.radioSingle.Size = new System.Drawing.Size(54, 18);
            this.radioSingle.TabIndex = 44;
            this.radioSingle.TabStop = true;
            this.radioSingle.Text = "Single";
            this.radioSingle.UseVisualStyleBackColor = false;
            this.radioSingle.CheckedChanged += new System.EventHandler(this.Single_CheckChanged);
            // 
            // radioSequence
            // 
            this.radioSequence.AutoSize = true;
            this.radioSequence.BackColor = System.Drawing.Color.White;
            this.radioSequence.Location = new System.Drawing.Point(306, 43);
            this.radioSequence.Name = "radioSequence";
            this.radioSequence.Size = new System.Drawing.Size(74, 18);
            this.radioSequence.TabIndex = 45;
            this.radioSequence.Text = "Sequence";
            this.radioSequence.UseVisualStyleBackColor = false;
            this.radioSequence.CheckedChanged += new System.EventHandler(this.Sequence_CheckChanged);
            // 
            // radioParallel
            // 
            this.radioParallel.AutoSize = true;
            this.radioParallel.BackColor = System.Drawing.Color.White;
            this.radioParallel.Location = new System.Drawing.Point(417, 43);
            this.radioParallel.Name = "radioParallel";
            this.radioParallel.Size = new System.Drawing.Size(59, 18);
            this.radioParallel.TabIndex = 46;
            this.radioParallel.Text = "Parallel";
            this.radioParallel.UseVisualStyleBackColor = false;
            this.radioParallel.CheckedChanged += new System.EventHandler(this.Parallel_CheckChanged);
            // 
            // treeViewActions
            // 
            this.treeViewActions.AllowDrop = true;
            this.treeViewActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewActions.ContextMenuStrip = this.contextEntityAction;
            this.treeViewActions.HideSelection = false;
            this.treeViewActions.Location = new System.Drawing.Point(166, 105);
            this.treeViewActions.Name = "treeViewActions";
            this.treeViewActions.Size = new System.Drawing.Size(231, 382);
            this.treeViewActions.TabIndex = 47;
            this.treeViewActions.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewActions_NodeMouseClick);
            this.treeViewActions.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeViewActions_DragDrop);
            this.treeViewActions.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.TreeViewActions_QueryContinueDrag);
            //this.treeViewActions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TreeviewActions_KeyPress);
            // 
            // contextEntityAction
            // 
            this.contextEntityAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextEntityActionDelete,
            this.contextEntityActionEdit,
            this.contextEntityActionCancel});
            this.contextEntityAction.Name = "contextEntityAction";
            this.contextEntityAction.Size = new System.Drawing.Size(111, 70);
            this.contextEntityAction.Opening += new System.ComponentModel.CancelEventHandler(this.ContextEntityAction_Opening);
            this.contextEntityAction.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ContextEntityAction_MouseClick);
            // 
            // contextEntityActionDelete
            // 
            this.contextEntityActionDelete.Name = "contextEntityActionDelete";
            this.contextEntityActionDelete.Size = new System.Drawing.Size(110, 22);
            this.contextEntityActionDelete.Text = "Delete";
            this.contextEntityActionDelete.Click += new System.EventHandler(this.ContextEntityActionDelete_Click);
            // 
            // contextEntityActionEdit
            // 
            this.contextEntityActionEdit.Name = "contextEntityActionEdit";
            this.contextEntityActionEdit.Size = new System.Drawing.Size(110, 22);
            this.contextEntityActionEdit.Text = "Edit";
            this.contextEntityActionEdit.Click += new System.EventHandler(this.ContextEntityActionEdit_Click);
            // 
            // contextEntityActionCancel
            // 
            this.contextEntityActionCancel.Name = "contextEntityActionCancel";
            this.contextEntityActionCancel.Size = new System.Drawing.Size(110, 22);
            this.contextEntityActionCancel.Text = "Cancel";
            this.contextEntityActionCancel.Click += new System.EventHandler(this.ContextEntityActionCancel_Click);
            // 
            // lblEntityActions
            // 
            this.lblEntityActions.AutoSize = true;
            this.lblEntityActions.Location = new System.Drawing.Point(166, 88);
            this.lblEntityActions.Name = "lblEntityActions";
            this.lblEntityActions.Size = new System.Drawing.Size(74, 14);
            this.lblEntityActions.TabIndex = 48;
            this.lblEntityActions.Text = "Entity actions:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.actionPropertiesPanel);
            this.panel1.Location = new System.Drawing.Point(403, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 382);
            this.panel1.TabIndex = 50;
            // 
            // actionPropertiesPanel
            // 
            this.actionPropertiesPanel.AutoScroll = true;
            this.actionPropertiesPanel.Controls.Add(this.chkRetainAction);
            this.actionPropertiesPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.actionPropertiesPanel.Location = new System.Drawing.Point(7, 3);
            this.actionPropertiesPanel.Name = "actionPropertiesPanel";
            this.actionPropertiesPanel.Size = new System.Drawing.Size(276, 376);
            this.actionPropertiesPanel.TabIndex = 41;
            // 
            // chkRetainAction
            // 
            this.chkRetainAction.AutoSize = true;
            this.chkRetainAction.Location = new System.Drawing.Point(3, 3);
            this.chkRetainAction.Name = "chkRetainAction";
            this.chkRetainAction.Size = new System.Drawing.Size(89, 18);
            this.chkRetainAction.TabIndex = 51;
            this.chkRetainAction.Text = "Retain Action";
            this.chkRetainAction.UseVisualStyleBackColor = true;
            this.chkRetainAction.Visible = false;
            this.chkRetainAction.CheckedChanged += new System.EventHandler(this.RetainAction_CheckChanged);
            // 
            // lblActionProperties
            // 
            this.lblActionProperties.AutoSize = true;
            this.lblActionProperties.Location = new System.Drawing.Point(400, 88);
            this.lblActionProperties.Name = "lblActionProperties";
            this.lblActionProperties.Size = new System.Drawing.Size(59, 14);
            this.lblActionProperties.TabIndex = 51;
            this.lblActionProperties.Text = "Properties:";
            // 
            // radioPlus
            // 
            this.radioPlus.AutoSize = true;
            this.radioPlus.BackColor = System.Drawing.Color.White;
            this.radioPlus.Location = new System.Drawing.Point(106, 43);
            this.radioPlus.Name = "radioPlus";
            this.radioPlus.Size = new System.Drawing.Size(60, 18);
            this.radioPlus.TabIndex = 53;
            this.radioPlus.Text = "Single+";
            this.radioPlus.UseVisualStyleBackColor = false;
            this.radioPlus.CheckedChanged += new System.EventHandler(this.SinglePlus_CheckChanged);
            // 
            // lblMode
            // 
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Location = new System.Drawing.Point(459, 13);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(221, 17);
            this.lblMode.TabIndex = 52;
            this.lblMode.Text = "Current Mode: ";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioPlusPlus
            // 
            this.radioPlusPlus.AutoSize = true;
            this.radioPlusPlus.BackColor = System.Drawing.Color.White;
            this.radioPlusPlus.Location = new System.Drawing.Point(203, 43);
            this.radioPlusPlus.Name = "radioPlusPlus";
            this.radioPlusPlus.Size = new System.Drawing.Size(66, 18);
            this.radioPlusPlus.TabIndex = 54;
            this.radioPlusPlus.Text = "Single++";
            this.radioPlusPlus.UseVisualStyleBackColor = false;
            this.radioPlusPlus.CheckedChanged += new System.EventHandler(this.SinglePlusPlus_CheckChanged);
            // 
            // btnComplete
            // 
            this.btnComplete.Location = new System.Drawing.Point(605, 41);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(75, 23);
            this.btnComplete.TabIndex = 55;
            this.btnComplete.Text = "Complete";
            this.btnComplete.UseVisualStyleBackColor = true;
            this.btnComplete.Visible = false;
            this.btnComplete.Click += new System.EventHandler(this.Complete_Click);
            // 
            // PulsarActionsSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 699);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.radioPlusPlus);
            this.Controls.Add(this.radioPlus);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.lblActionProperties);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblEntityActions);
            this.Controls.Add(this.treeViewActions);
            this.Controls.Add(this.radioParallel);
            this.Controls.Add(this.radioSequence);
            this.Controls.Add(this.radioSingle);
            this.Controls.Add(this.lblEntity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.actionsList);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PulsarActionsSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actions Setup";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PulsarActionsSetup_FormClosed);
            this.Shown += new System.EventHandler(this.PulsarActionsSetup_Shown);
            this.contextEntityAction.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.actionPropertiesPanel.ResumeLayout(false);
            this.actionPropertiesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox actionsList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEntity;
        private System.Windows.Forms.RadioButton radioSingle;
        private System.Windows.Forms.RadioButton radioSequence;
        private System.Windows.Forms.RadioButton radioParallel;
        private System.Windows.Forms.TreeView treeViewActions;
        private System.Windows.Forms.Label lblEntityActions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel actionPropertiesPanel;
        private System.Windows.Forms.Label lblActionProperties;
        private System.Windows.Forms.RadioButton radioPlus;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.RadioButton radioPlusPlus;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.ContextMenuStrip contextEntityAction;
        private System.Windows.Forms.ToolStripMenuItem contextEntityActionEdit;
        private System.Windows.Forms.ToolStripMenuItem contextEntityActionCancel;
        private System.Windows.Forms.CheckBox chkRetainAction;
        private System.Windows.Forms.ToolStripMenuItem contextEntityActionDelete;
    }
}