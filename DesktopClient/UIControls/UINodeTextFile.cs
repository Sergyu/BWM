using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.UIControls
{
    public class UINodeTextFile : IUINodeControl
    {
        private RichTextBox richTextBox;
        private Control designRenderer;

        
        public UINodeTextFile(string name, string fileName, int imgIdx = 3, Control designEditor = null)
        {
            this.Name = name;
            this.FileName = fileName;
            this.ImageIndex = imgIdx;
            this.designRenderer = designEditor;
        }

        public string Name { get; private set; }

        public string FileName { get; private set; }

        public int ImageIndex { get; private set; }

        public Control DesignRender()
        {
            if (null == designRenderer)
            {
               // designRenderer = new ActionEditor();
            }
            return designRenderer;
        }

        public Control RawRender()
        {
            if (null == this.richTextBox)
            {
                richTextBox = new RichTextBox();
                richTextBox.Location = new System.Drawing.Point(5, 25);
                richTextBox.Size = new System.Drawing.Size(1100, 800);
                richTextBox.BorderStyle = BorderStyle.None;
                var folderPrefix = this.FileName.Contains(".obj.") ? "DomainModel" : "Nodes";
                richTextBox.Text = File.ReadAllText($@"E:\Projects\BWMT\BWM\DesktopClient\SigningModel\{folderPrefix}\{FileName}");
            }
            return richTextBox;
        }
    }
}
