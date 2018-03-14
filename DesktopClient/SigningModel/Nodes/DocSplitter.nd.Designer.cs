using Core.Nodes;
using DesktopClient.SigningModel.DomainModel;

namespace DesktopClient.SigningModel.Nodes
{
    partial class DocSplitter
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
            this.SuspendLayout();
            // 
            // DocSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DesktopClient.Properties.Resources.Splitter;
            this.Name = "DocSplitter";
            this.Size = new System.Drawing.Size(181, 107);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
