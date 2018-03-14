using Core.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.UIControls
{
    public abstract class BaseUINode<TCore, Tin,Tout> : UserControl, IUINodeControl, ITicker, IIdentity, ICompilable
        where TCore : IIn<Tin>, IIdentity
        where Tin: class
    {
        private string _fileName;
        private DesignEditor.EditorType _designType;
        private Control _rawControl;
        protected DesignEditor designControl;
        private List<Note> _notes;

        public TCore Node
        {
            get;
            private set;
        }

        public BaseUINode(string fileName, DesignEditor.EditorType designType, TCore modelNode)
        {
            this._fileName = fileName;
            this._designType = designType;
            this.Node = modelNode;
            this._notes = new List<Note>();
        }


        #region IUINodeControl
       

        public string FileName => this._fileName;

        public int ImageIndex => 3;

        int IIdentity.Id => this.Node.Id;

        string IIdentity.Name => this.Node.Name;

        public IList<Note> Notes => this._notes;

        public Control GetDesignEditor()
        {
            if (null == this.designControl)
            {
                this.designControl = new DesignEditor(this._designType);
                this.designControl.SetDefaultAction(this.getDefaultEditorText());
                this.designControl.SetInType<Tin>();
                if (typeof(Tin) != typeof(Tout))
                {
                    this.designControl.SetOutType<Tout>();
                }
                this.designControl.Close += Design_Control_Close;
                
            }
            return this.designControl;
        }

        protected virtual void Design_Control_Close(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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

        public void Close(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        public UIControls.Rectangle GetScreenPosition()
        {
            return this.GetType().GetCustomAttributes(false).OfType<UIControls.Rectangle>().First();
        }

        public IList<object> GetPayloads()
        {
            return this.Node.GetPayloads().Cast<object>().ToList();
        }

        public List<ITicker> Tick()
        {
            return this.Node.Tick();
        }
        #endregion

        protected virtual string getDefaultEditorText()
        {
            return string.Empty;
        }

        public virtual void Compile()
        {
            // do nothing for those who did not override
        }
    }
}
