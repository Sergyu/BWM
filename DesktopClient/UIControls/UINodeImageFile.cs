using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.UIControls
{
    public class UINodeImageFile : IUINodeControl
    {
        private PictureBox rawRenderer;
        private ActionEditor designRenderer;

        public UINodeImageFile(string name, string fileName, int idx)
        {
            this.Name = name;
            this.FileName = fileName;
            this.ImageIndex = idx;
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
            if (null == rawRenderer)
            {
                rawRenderer = new PictureBox();
                rawRenderer.Image = global::DesktopClient.Properties.Resources.SigningM;

                rawRenderer.Location = new System.Drawing.Point(15, 15);
                rawRenderer.Name = "model";
                rawRenderer.Size = new System.Drawing.Size(1390, 990);
            }
                

            return rawRenderer;
        }
    }

    public class Rectangle : Attribute
    {
        private int _endX;
        private int _endY;

        private int _startX;
        private int _startY;

        public Rectangle(int x, int y)
        {
            this._startX = x;
            this._startY = y;
            this._endX = this._startX + 90;
            this._endY = this._startY + 90;
        }

        public int X { get { return _startX;  } }
        public int Y { get { return _startY; } }

        public bool IsIn(int x, int y)
        {
            return     this._startX <= x
                    && this._endX >= x
                    && this._startY <= y
                    && this._endY >= y;
        }
    }
}
