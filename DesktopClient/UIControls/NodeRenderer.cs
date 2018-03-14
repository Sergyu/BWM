using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.UIControls
{
    /*
    public class NodeRenderer : IUINodeControl
    {
        private Control tab;
        private Control designRenderer;

        
        public NodeRenderer(string name, string fileName, int imgIdx = 3, Control designEditor = null)
        {
            this.Name = name;
            this.FileName = fileName;
            this.ImageIndex = imgIdx;
            this.designRenderer = designEditor;
        }

        public string Name { get; private set; }

        public string FileName { get; private set; }

        public int ImageIndex { get; private set; }

        public void Close(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public Control DesignRender()
        {
            return designRenderer;
        }

        public Control RawRender()
        {
            if (null == this.tab)
            {
                var richTextBox = new CSharpEditor();
                var folderPrefix = this.FileName.Contains(".obj.") ? "DomainModel" : "Nodes";
                this.tab = richTextBox.CreateTab($@"E:\Projects\BWMT\BWM\DesktopClient\SigningModel\{folderPrefix}\{FileName}");
            }
            return tab;
        }
    }*/
}
