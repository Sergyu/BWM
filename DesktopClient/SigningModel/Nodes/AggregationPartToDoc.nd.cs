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

namespace DesktopClient.SigningModel.Nodes
{
    [UIControls.Rectangle(619, 126)]
    public partial class AggregationPartToDoc : BaseUINode<AggregatorNode<Part, Document>, Part, Document>
    {
        public AggregationPartToDoc() : 
            base(
                "AggregationPartToDoc.nd.cs", 
                DesignEditor.EditorType.Aggregate,
                new AggregatorNode<Part, Document>("AggregationPartToDoc"))
        {
            InitializeComponent();
            this.Notes.Add(new Note("AggregationPartToDoc.nd.cs"));
        }
    }
}
