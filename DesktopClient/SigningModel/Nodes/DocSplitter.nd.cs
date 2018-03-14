using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Nodes;
using DesktopClient.SigningModel.DomainModel;
using DesktopClient.UIControls;
using Compiler = Common.Singleton<DesktopClient.AssemblyCompiler>;

namespace DesktopClient.SigningModel.Nodes
{
    [UIControls.Rectangle(142, 126)]
    public partial class DocSplitter : BaseUINode<SplitterNode<Document, Part>, Document, Part>
    {
        
        public DocSplitter(): base(
            "DocSplitter.nd.cs",
            DesignEditor.EditorType.Split,
            new SplitterNode<Document, Part>("DocSplitter")
            )
        {
            InitializeComponent();
            /*this.Node.SetSplitter((doc, node) =>
            {
                for (int i = 1; i <= doc.Parts.Count; i++)
                {
                    if (node.Name.Contains(i.ToString())) return doc.Parts[i-1];
                }
                return null;
            });*/
        }

        
        public override void Compile()
        {
            var funcBody = (this.GetDesignEditor() as DesignEditor).GetFunctionBody();
            var func = Compiler.Instance.GenerateFunc<Document, IIdentity, Part>(funcBody);
            if (null != func)
            {
                this.Node.SetSplitter(func);
            }
        }

        protected override string getDefaultEditorText()
        {
            return @"
{
    for (int i = 1; i <= inObject.Parts.Count; i++)
    {
        if (childNode.Name.Contains(i.ToString())) return inObject.Parts[i-1];
    }
    return null;
}";
        }
    }
}
