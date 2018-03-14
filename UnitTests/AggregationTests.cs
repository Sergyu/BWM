using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Core.Tree;
using Core.Nodes;

namespace UnitTests
{
    [TestClass]
    public class AggregationTests
    {
        [TestMethod]
        public void Test_3SplitsSameVelocity()
        {
            var availabilities = new List<Signer.Available>();
            availabilities.Add(Signer.Available.Always);
            availabilities.Add(Signer.Available.Always);
            availabilities.Add(Signer.Available.Always);
            var document = buildDocument(3, availabilities);
            TreeModel model = newModel(document);

            model.Run(document);
        }

        [TestMethod]
        public void Test_3SplitsDiffVelocity()
        {
            var availabilities = new List<Signer.Available>();
            availabilities.Add(Signer.Available.Always);
            availabilities.Add(Signer.Available.Rarely);
            availabilities.Add(Signer.Available.Sometimes);
            var document = buildDocument(3, availabilities);
            TreeModel model = newModel(document);

            model.Run(document);
        }

        private TreeModel newModel(Document document)
        {
            var model = new TreeModel(100);
            var documentSplit = new SplitterNode<Document, Part>("FromDocToPart");
            var part1Signing = new ActionNode<Part>("Part0");
            var part1IsValid = new DecisionNode<Part>("DecisionPar0");
            var part2Signing = new ActionNode<Part>("Part1");
            var part2IsValid = new DecisionNode<Part>("DecisionPar1");
            var part3Signing = new ActionNode<Part>("Part2");
            var part3IsValid = new DecisionNode<Part>("DecisionPar2");
            var partAggregation = new AggregatorNode<Part, Document>("AssembleDocument");
            var isDocumentSigned = new DecisionNode<Document>("IsDocSigned");
            var stop = new ActionNode<Document>("Stop");

            documentSplit.AddChild(part1Signing);
            documentSplit.AddChild(part2Signing);
            documentSplit.AddChild(part3Signing);

            part1Signing.AddChild(part1IsValid);
            part1IsValid.SetYesChild(partAggregation);
            part1IsValid.SetNoChild(part1Signing);

            part2Signing.AddChild(part2IsValid);
            part2IsValid.SetYesChild(partAggregation);
            part2IsValid.SetNoChild(part2Signing);

            part3Signing.AddChild(part3IsValid);
            part3IsValid.SetYesChild(partAggregation);
            part3IsValid.SetNoChild(part3Signing);

            partAggregation.AddChild(isDocumentSigned);

            isDocumentSigned.SetNoChild(documentSplit);
            isDocumentSigned.SetYesChild(stop);


            documentSplit.SetSplitter((doc, node) =>
            {
                for (int i=0; i< doc.Parts.Count; i++)
                {
                    if (node.Name.Contains(i.ToString())) return doc.Parts[i];
                }
                throw null;
            });

            part1Signing.SetAction(part => part.Signer.Signe(part));
            part2Signing.SetAction(part => part.Signer.Signe(part));
            part3Signing.SetAction(part => part.Signer.Signe(part));

            part1IsValid.SetCondition(part => part.Signed);
            part2IsValid.SetCondition(part => part.Signed);
            part3IsValid.SetCondition(part => part.Signed);

            partAggregation.SetAggregator((part, doc) => { });

            isDocumentSigned.SetCondition(doc => {
                return doc.IsSigned;
            });

            model.SetStartNode(documentSplit);
            model.SetEndNode(stop);

            return model;
        }

        private Document buildDocument(int partNr, List<Signer.Available> availabilities)
        {
            var doc = new Document();
            
            for (int i=0; i<partNr; i++)
            {
                var part = new Part();
                part.Signer = new Signer(availabilities[i]);
                doc.Parts.Add(part);
            }
            return doc;
        }

        public class Part
        {
            public bool Signed { get; set; }
            public Signer Signer;
        }

        public class Signer
        {
            public enum Available { Always = 1, Sometimes = 3, Rarely = 7}

            private int _attempts;
            private Available _type;

            public Signer(Available type)
            {
                this._type = type;
            }

            public void Signe(Part part)
            {
                this._attempts++;
                if (this._attempts >= (int)this._type)
                {
                    part.Signed = true;
                }
            }
        }

        public class Document
        {
            public List<Part> Parts = new List<Part>();
            public bool IsSigned
            {
                get
                {
                    return this.Parts.All(p => p.Signed);
                }
            }
        }
    }
}
