namespace DesktopClient.UIControls
{
    partial class DesignEditor
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
            this.comboOut = new System.Windows.Forms.ComboBox();
            this.lblOut = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.domainObjects = new System.Windows.Forms.ComboBox();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.pctBoxClose = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxClose)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboOut);
            this.groupBox1.Controls.Add(this.lblOut);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.domainObjects);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 220);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "In/Out Object";
            // 
            // comboOut
            // 
            this.comboOut.FormattingEnabled = true;
            this.comboOut.Location = new System.Drawing.Point(40, 70);
            this.comboOut.Name = "comboOut";
            this.comboOut.Size = new System.Drawing.Size(153, 21);
            this.comboOut.TabIndex = 3;
            this.comboOut.Visible = false;
            // 
            // lblOut
            // 
            this.lblOut.AutoSize = true;
            this.lblOut.Location = new System.Drawing.Point(2, 73);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(25, 13);
            this.lblOut.TabIndex = 2;
            this.lblOut.Text = "Out";
            this.lblOut.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "In";
            // 
            // domainObjects
            // 
            this.domainObjects.FormattingEnabled = true;
            this.domainObjects.Location = new System.Drawing.Point(40, 32);
            this.domainObjects.Name = "domainObjects";
            this.domainObjects.Size = new System.Drawing.Size(153, 21);
            this.domainObjects.TabIndex = 0;
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBoxAction.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.groupBoxAction.Location = new System.Drawing.Point(227, 0);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(873, 220);
            this.groupBoxAction.TabIndex = 1;
            this.groupBoxAction.TabStop = false;
            this.groupBoxAction.Text = "Action Function";
            // 
            // pctBoxClose
            // 
            this.pctBoxClose.Image = global::DesktopClient.Properties.Resources.close_24;
            this.pctBoxClose.Location = new System.Drawing.Point(1070, -1);
            this.pctBoxClose.Name = "pctBoxClose";
            this.pctBoxClose.Size = new System.Drawing.Size(24, 24);
            this.pctBoxClose.TabIndex = 0;
            this.pctBoxClose.TabStop = false;
            this.pctBoxClose.Click += PctBoxClose_Click;
            // 
            // DesignEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pctBoxClose);
            this.Controls.Add(this.groupBoxAction);
            this.Controls.Add(this.groupBox1);
            this.Name = "DesignEditor";
            this.Size = new System.Drawing.Size(1100, 220);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxClose)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox domainObjects;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.ComboBox comboOut;
        private System.Windows.Forms.Label lblOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pctBoxClose;
    }
}
