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
    [UIControls.Rectangle(881, 125)]
    public partial class End : BaseUINode<ActionNode<Document>, Document, Document>
    {

        public End() : base(
             "End.nd.cs",
             DesignEditor.EditorType.Action,
             new ActionNode<Document>("End")
            )
        {
            InitializeComponent();
        }

        
    }
}
