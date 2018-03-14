using Core.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.UIControls
{
    public interface IUINodeControl 
    {
        Control GetRawEditor();

        string Name { get; }

        string FileName { get; }

        Control GetDesignEditor();

        int ImageIndex { get; }

        Rectangle GetScreenPosition();

        IList<object> GetPayloads();

        IList<Note> Notes { get; }
    }
}
