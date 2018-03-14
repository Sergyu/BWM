using Compiler = Common.Singleton<DesktopClient.AssemblyCompiler>;
using Core.Nodes;
using DesktopClient.SigningModel.DomainModel;
using DesktopClient.UIControls;
using System;

namespace DesktopClient.SigningModel.Nodes
{
    [UIControls.Rectangle(309, 244)]
    public partial class PartSigning3 : BaseUINode<ActionNode<Part>, Part, Part>
    {
        public PartSigning3() : base(
            "PartSigning3.nd.cs",
            DesignEditor.EditorType.Action,
            new ActionNode<Part>("PartSigning3")
            )
        {
            InitializeComponent();
            //this.Node.SetAction(part => part.Signer.Signe(part));
        }

        public override void Compile()
        {
            var funcBody = (this.GetDesignEditor() as DesignEditor).GetFunctionBody();
            var func = Compiler.Instance.GenerateAction<Part>(funcBody);
            if (null != func)
            {
                this.Node.SetAction(func);
            }
        }


        protected override string getDefaultEditorText()
        {
            return @"
{
    inObject.Signer.Signe(inObject);
}";
        }
    }
}