using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.UIControls
{
    public class Note
    {
        private readonly PictureBox pic;
        private readonly string fileName;

        public Note(string nodeFileName)
        {
            this.pic = new PictureBox();
            this.pic.Image = global::DesktopClient.Properties.Resources.Note_29;
            this.pic.BackColor = Color.Transparent;
            this.fileName = string.Format("{0}\\Nodes\\{1}", MainForm.SolutionLocation, nodeFileName.Replace("cs", "note"));
            createFile(nodeFileName);
        }

        private void createFile(string nodeFileName)
        {
            using (var fStr = File.CreateText(this.fileName))
            {
                fStr.WriteLine($@"///Note for the Node: {nodeFileName}");
                fStr.WriteLine($@"///created: {DateTime.Now.ToShortDateString()}");
            }
        }

        public PictureBox Image
        {
            get
            {
                return this.pic;
            }
        }
    }
}
