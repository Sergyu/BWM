namespace DesktopClient.UIControls
{
    partial class ActionEditor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.domainObjects = new System.Windows.Forms.ComboBox();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.actionEditFunc = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOut = new System.Windows.Forms.Label();
            this.comboOut = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboOut);
            this.groupBox1.Controls.Add(this.lblOut);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.domainObjects);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 172);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "In/Out Object";
            // 
            // domainObjects
            // 
            this.domainObjects.FormattingEnabled = true;
            this.domainObjects.Location = new System.Drawing.Point(80, 32);
            this.domainObjects.Name = "domainObjects";
            this.domainObjects.Size = new System.Drawing.Size(153, 24);
            this.domainObjects.TabIndex = 0;
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.actionEditFunc);
            this.groupBoxAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAction.Location = new System.Drawing.Point(273, -1);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(812, 172);
            this.groupBoxAction.TabIndex = 1;
            this.groupBoxAction.TabStop = false;
            this.groupBoxAction.Text = "Action Function";
            // 
            // actionEditFunc
            // 
            this.actionEditFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionEditFunc.Location = new System.Drawing.Point(3, 19);
            this.actionEditFunc.Name = "actionEditFunc";
            this.actionEditFunc.Size = new System.Drawing.Size(806, 150);
            this.actionEditFunc.TabIndex = 0;
            this.actionEditFunc.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "In";
            // 
            // lblOut
            // 
            this.lblOut.AutoSize = true;
            this.lblOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOut.Location = new System.Drawing.Point(42, 73);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(31, 17);
            this.lblOut.TabIndex = 2;
            this.lblOut.Text = "Out";
            this.lblOut.Visible = false;
            // 
            // comboOut
            // 
            this.comboOut.FormattingEnabled = true;
            this.comboOut.Location = new System.Drawing.Point(80, 70);
            this.comboOut.Name = "comboOut";
            this.comboOut.Size = new System.Drawing.Size(153, 24);
            this.comboOut.TabIndex = 3;
            this.comboOut.Visible = false;
            // 
            // ActionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBoxAction);
            this.Controls.Add(this.groupBox1);
            this.Name = "ActionEditor";
            this.Size = new System.Drawing.Size(1088, 174);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox domainObjects;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.RichTextBox actionEditFunc;
        private System.Windows.Forms.ComboBox comboOut;
        private System.Windows.Forms.Label lblOut;
        private System.Windows.Forms.Label label1;
    }
}
