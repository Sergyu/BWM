namespace DesktopClient.UIControls
{
    partial class SplitterEditor
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
            this.comboOut1 = new System.Windows.Forms.ComboBox();
            this.lblOut = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.domainObjects1 = new System.Windows.Forms.ComboBox();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.actionEditFunc = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboOut1);
            this.groupBox1.Controls.Add(this.lblOut);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.domainObjects1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 172);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "In/Out Object";
            // 
            // comboOut1
            // 
            this.comboOut1.FormattingEnabled = true;
            this.comboOut1.Location = new System.Drawing.Point(80, 70);
            this.comboOut1.Name = "comboOut1";
            this.comboOut1.Size = new System.Drawing.Size(153, 24);
            this.comboOut1.TabIndex = 3;
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
            // domainObjects1
            // 
            this.domainObjects1.FormattingEnabled = true;
            this.domainObjects1.Location = new System.Drawing.Point(80, 32);
            this.domainObjects1.Name = "domainObjects1";
            this.domainObjects1.Size = new System.Drawing.Size(153, 24);
            this.domainObjects1.TabIndex = 0;
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.actionEditFunc);
            this.groupBoxAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAction.Location = new System.Drawing.Point(273, -1);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(817, 172);
            this.groupBoxAction.TabIndex = 1;
            this.groupBoxAction.TabStop = false;
            this.groupBoxAction.Text = "SplitterFunction";
            // 
            // actionEditFunc
            // 
            this.actionEditFunc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.actionEditFunc.Location = new System.Drawing.Point(6, 22);
            this.actionEditFunc.Name = "actionEditFunc";
            this.actionEditFunc.Size = new System.Drawing.Size(805, 144);
            this.actionEditFunc.TabIndex = 0;
            this.actionEditFunc.Text = "";
            // 
            // SplitterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBoxAction);
            this.Controls.Add(this.groupBox1);
            this.Name = "SplitterEditor";
            this.Size = new System.Drawing.Size(1100, 180);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox domainObjects1;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.RichTextBox actionEditFunc;
        private System.Windows.Forms.ComboBox comboOut1;
        private System.Windows.Forms.Label lblOut;
        private System.Windows.Forms.Label label1;
    }
}
