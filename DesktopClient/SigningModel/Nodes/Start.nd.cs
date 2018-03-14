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
    [UIControls.Rectangle(10,126)]
    public partial class Start: BaseUINode<StartNode<Document>, Document, Document>
    {
        public Start() : base(
                "Start.nd.cs",
                DesignEditor.EditorType.Start,
                new StartNode<Document>("Start")
            )
        {
            InitializeComponent();
        }

        protected override void Design_Control_Close(object sender, EventArgs e)
        {
            /*var funcBody = this.designControl.GetFunctionBody();
            var func = Compiler.Instance.GenerateFunc<Document>(funcBody);
            if (null != func)
            {
                this.Node.Initialize(func);
            }*/
        }

        public override void Compile()
        {
            var funcBody = (this.GetDesignEditor() as DesignEditor).GetFunctionBody();
            var func = Compiler.Instance.GenerateFunc<Document>(funcBody);
            if (null != func)
            {
                this.Node.Initialize(func);
            }
        }


        protected override string getDefaultEditorText()
        {
            return @"
{
    var doc = new Document();
    var part1 = new Part();
    part1.Signer = new Signer(Available.Always);
    doc.Parts.Add(part1);

    var part2 = new Part();
    part2.Signer = new Signer(Available.Rarely);
    doc.Parts.Add(part2);

    var part3 = new Part();
    part3.Signer = new Signer(Available.Sometimes);
    doc.Parts.Add(part3);

    return doc;
}";
        }
    }
}
