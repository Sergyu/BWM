using DesktopClient.UIControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Nodes;

namespace DesktopClient.SigningModel.DomainModel
{
    public class PayloadDescriptor : IUINodeControl
    {
        private Control _rawControl;

        public PayloadDescriptor()
        {
           
        }

        #region IUINodeControl
        private IUINodeControl uIControl;

        public string FileName => "Payload.obj.cs";

        public string Name => "DomainModel";

        public int ImageIndex => 4;

        public IList<Note> Notes => null;

        public Control GetDesignEditor()
        {
            throw new NotImplementedException();
        }

        public Control GetRawEditor()
        {
            if (null == this._rawControl)
            {
                var richTextBox = new CSharpEditor();
                var folderPrefix = this.FileName.Contains(".obj.") ? "DomainModel" : "Nodes";
                this._rawControl = richTextBox.CreateTab($@"E:\Projects\BWMT\BWM\DesktopClient\SigningModel\{folderPrefix}\{FileName}");
            }
            return _rawControl;
        }

        public Rectangle GetScreenPosition()
        {
            throw new NotImplementedException();
        }

        public IList<object> GetPayloads()
        {
            throw new NotImplementedException();
        }

        public List<ITicker> Tick()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
