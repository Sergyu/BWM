using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.UIControls
{
    public interface IUIModel : IUINodeControl
    {
        List<IUINodeControl> Nodes { get; }

        void Run(IModelDebugger debugger);

        PictureBox ModelImage { get; }
    }
}
