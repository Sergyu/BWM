using Compiler = Common.Singleton<DesktopClient.AssemblyCompiler>;
using Core.Nodes;
using DesktopClient.SigningModel.DomainModel;
using DesktopClient.UIControls;

namespace DesktopClient.SigningModel.Nodes
{
    [UIControls.Rectangle(452, 8)]
    public partial class IsPart1Signed : BaseUINode<DecisionNode<Part>, Part, Part>
    {
        public IsPart1Signed() : base(
            "IsPart1Signed.nd.cs",
            DesignEditor.EditorType.Decision,
            new DecisionNode<Part>("IsPart1Signed")
            )
        {
            InitializeComponent();
            //this.Node.SetCondition(part => part.Signed);
        }

        public override void Compile()
        {
            var funcBody = (this.GetDesignEditor() as DesignEditor).GetFunctionBody();
            var func = Compiler.Instance.GeneratePredicate<Part>(funcBody);
            if (null != func)
            {
                this.Node.SetCondition(func);
            }
        }


        protected override string getDefaultEditorText()
        {
            return @"
{
    return inObject.Signed;
}";
        }
    }
}
