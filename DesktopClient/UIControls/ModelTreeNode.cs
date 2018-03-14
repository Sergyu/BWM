using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.UIControls
{
    public class ModelTreeNode : TreeNode
    {
        private IUINodeControl nodeControl;


        public ModelTreeNode(string txt, IUINodeControl ctrl): base(txt)
        {
            this.nodeControl = ctrl;
        }

        public IUINodeControl Renderer
        {
            get
            {
                return this.nodeControl;
            }
        }
    }
}
