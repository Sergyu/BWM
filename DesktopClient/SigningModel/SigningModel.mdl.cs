using Core.Tree;
using DesktopClient.SigningModel.Nodes;
using DesktopClient.UIControls;
using System.Windows.Forms;
using System.Collections.Generic;
using Common;
using System;
using DesktopClient.SigningModel.DomainModel;
using Core.Nodes;
using System.Linq;

namespace DesktopClient.SigningModel
{
    public partial class SigningModel : UserControl, IUIModel, ISingleton
    {
        //static
        private static readonly IEqualityComparer<IIdentity> identityComparer = new LambdaComparer<IIdentity>((n1, n2) => n1.Id == n2.Id);

        //declare UI Nodes
        private TreeModel model;
        private PayloadDescriptor payload;
        private Start modelStart;
        private DocSplitter documentSplit;
        private PartSigning1 part1Signing;
        private PartSigning2 part2Signing;
        private PartSigning3 part3Signing;
        private IsPart1Signed part1IsValid;
        private IsPart2Signed part2IsValid;
        private IsPart3Signed part3IsValid;
        private AggregationPartToDoc partAggregation;
        private IsDocumentSigned isDocumentSigned;
        private End stop;

        
        // private vars
        private static readonly object synchronizer = new object();
        private ModelRenderer _renderer;

        public SigningModel()
        {
            
            InitializeComponent();
            InitializeDrawingBoard();
            this._renderer = new ModelRenderer("SigningModel", "SigningModel.mdl.cs", 2);
        }

        #region IUINodeControl
        private ModelRenderer modelRender;

        public string FileName => this._renderer.FileName;

        public Control GetDesignEditor()
        {
            return this._renderer.GetDesignEditor();
        }

        public Control GetRawEditor()
        {
            return this._renderer.GetRawEditor();
        }

        public int ImageIndex => this._renderer.ImageIndex;
        #endregion

        public void Run(IModelDebugger debugger)
        {
            this.Nodes.OfType<ICompilable>()
                      .ToList()
                      .ForEach(n => n.Compile());

            var currentNodes = new List<ITicker>();
            currentNodes.Add(this.modelStart);
            debugger.MoveNext(currentNodes.OfType<IUINodeControl>().ToList());
            while (!gotToEnd(currentNodes))
            {
                var nodes = currentNodes.Distinct().ToList();
                currentNodes = new List<ITicker>();
                foreach (var node in nodes)
                {
                    currentNodes.AddRange(node.Tick());
                }
                debugger.MoveNext(getUINodesByModelNode(currentNodes));
            }
            debugger.Stop();
        }

        private IList<IUINodeControl> getUINodesByModelNode(List<ITicker> currentNodes)
        {
            var identifiers = currentNodes.OfType<IIdentity>();

            return this.Nodes.OfType<IIdentity>()
                             .Where(n => identifiers.Any(id => id.Id == n.Id))
                             .OfType<IUINodeControl>()
                             .ToList();
        }

        private bool gotToEnd(List<ITicker>  currentNodes)
        {
            return     currentNodes.Count == 0 
                    || currentNodes.Count == 1 
                    && currentNodes.Contains(this.stop.Node);
        }

        public List<IUINodeControl> Nodes
        {
            get;
            private set;
        }

        public PictureBox ModelImage
        {
            get
            {
                return this._renderer.ModelImage;
            }
        }

        public object Sinhronizer => synchronizer;

        public IList<Note> Notes => null;

        private void InitializeDrawingBoard()
        {
            this.model = new TreeModel(100);
            this.payload = new PayloadDescriptor();
            this.modelStart = new Start();
            this.documentSplit = new DocSplitter();
            this.part1Signing = new PartSigning1();
            this.part1IsValid = new IsPart1Signed();
            this.part2Signing = new PartSigning2();
            this.part2IsValid = new IsPart2Signed();
            this.part3Signing = new PartSigning3();
            this.part3IsValid = new IsPart3Signed();
            this.partAggregation = new AggregationPartToDoc();
            this.isDocumentSigned = new IsDocumentSigned();
            this.stop = new End();

            this.Nodes = new List<IUINodeControl>();
           

            this.Nodes.AddMany(
                modelStart, 
                documentSplit, 
                part1Signing, 
                part1IsValid, 
                part2Signing, 
                part2IsValid, 
                part3Signing, 
                part3IsValid, 
                partAggregation, 
                isDocumentSigned, 
                stop,
                this,
                this.payload);

            modelStart.Node.AddChild(documentSplit.Node);

            documentSplit.Node.AddChild(part1Signing.Node);
            documentSplit.Node.AddChild(part2Signing.Node);
            documentSplit.Node.AddChild(part3Signing.Node);

            part1Signing.Node.AddChild(part1IsValid.Node);
            part1IsValid.Node.SetYesChild(partAggregation.Node);
            part1IsValid.Node.SetNoChild(part1Signing.Node);

            part2Signing.Node.AddChild(part2IsValid.Node);
            part2IsValid.Node.SetYesChild(partAggregation.Node);
            part2IsValid.Node.SetNoChild(part2Signing.Node);

            part3Signing.Node.AddChild(part3IsValid.Node);
            part3IsValid.Node.SetYesChild(partAggregation.Node);
            part3IsValid.Node.SetNoChild(part3Signing.Node);

            partAggregation.Node.AddChild(isDocumentSigned.Node);

            isDocumentSigned.Node.SetNoChild(documentSplit.Node);
            isDocumentSigned.Node.SetYesChild(stop.Node);

            model.SetStartNode(modelStart.Node);
            model.SetEndNode(stop.Node);
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
    }
}
