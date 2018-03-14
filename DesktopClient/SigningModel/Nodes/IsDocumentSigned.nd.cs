using DesktopClient.SigningModel.DomainModel;
using Core.Nodes;
using DesktopClient.UIControls;
using Compiler = Common.Singleton<DesktopClient.AssemblyCompiler>;

namespace DesktopClient.SigningModel.Nodes
{
    [UIControls.Rectangle(751, 126)]
    public partial class IsDocumentSigned : BaseUINode<DecisionNode<Document>, Document, Document>
    {
        public IsDocumentSigned() : base(
            "IsDocumentSigned.nd.cs",
            DesignEditor.EditorType.Decision,
            new DecisionNode<Document>("IsDocumentSigned")
            )
        {
            InitializeComponent();
           /* this.Node.SetCondition(doc =>
            {
                return doc.IsSigned;
            });*/
        }

        public override void Compile()
        {
            var funcBody = (this.GetDesignEditor() as DesignEditor).GetFunctionBody();
            var func = Compiler.Instance.GeneratePredicate<Document>(funcBody);
            if (null != func)
            {
                this.Node.SetCondition(func);
            }
        }


        protected override string getDefaultEditorText()
        {
            return @"
{
    return inObject.IsSigned;
}";
        }
    }
}
