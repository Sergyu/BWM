using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Nodes;
using Debugger = Common.Singleton<DesktopClient.ModelDebugger>;



namespace DesktopClient.UIControls
{
    public class ModelRenderer : IUINodeControl
    {
        private SplitContainer rawRenderer;
        private PictureBox selectedNode;
        
        private PictureBox picture;
        private ObjectViewer debuggerVisualizer;

        public PictureBox ModelImage
        {
            get
            {
                if (null == picture)
                {
                    picture = new PictureBox();
                    picture.Image = global::DesktopClient.Properties.Resources.SigningM;
                }
                return picture;
            }
        }

        public ModelRenderer(string name, string fileName, int idx)
        {
            this.Name = name;
            this.FileName = fileName;
            this.ImageIndex = idx;
            Debugger.Instance.OnStart += Debugger_OnStart;
            Debugger.Instance.OnStop += Debugger_OnStop;
            Debugger.Instance.OnActiveNodeClick += Debug_NodeClick;
            Debugger.Instance.OnContinue += Debugger_Continue;
        }

        private void Debugger_Continue(object sender, EventArgs e)
        {
            this.rawRenderer.Panel2.Controls.Clear();
        }

        private Delegate clearDebugViewerArea = new Action<Control>(parent => parent.Controls.Clear()); 
        private void Debugger_OnStop(object sender, EventArgs e)
        {
            this.rawRenderer.Invoke(clearDebugViewerArea, this.rawRenderer.Panel2);
        }

        private void Debugger_OnStart(object sender, EventArgs e)
        {
            
        }

        private void Debug_NodeClick(IUINodeControl node)
        {
            var payloads = node.GetPayloads();
            this.rawRenderer.Panel2.Controls.Clear();
            if (null == this.debuggerVisualizer)
            {
                this.debuggerVisualizer = new ObjectViewer();
            }
            this.debuggerVisualizer.BindTo(payloads, node.Name);
            this.rawRenderer.Panel2.Controls.Add(this.debuggerVisualizer);
        }

        public string Name { get; private set; }

        public string FileName { get; private set; }

        public int ImageIndex { get; private set; }

        public IList<Note> Notes => throw new NotImplementedException();

        public Control GetRawEditor()
        {
            if (null == rawRenderer)
            {
                this.rawRenderer = new SplitContainer();
                this.rawRenderer.Orientation = Orientation.Horizontal;
                this.rawRenderer.SplitterDistance = 60;
                this.rawRenderer.BackColor = Color.White;
                this.rawRenderer.Dock = DockStyle.Fill;

               
                picture.Location = new System.Drawing.Point(15, 20);
                picture.Name = "model";
                picture.Size = new System.Drawing.Size(1390, 500);
                picture.MouseClick += CtrlToRender_Click;
                

                this.selectedNode = new PictureBox();
                this.selectedNode.Image = global::DesktopClient.Properties.Resources.SelectedCircle;
                this.selectedNode.Visible = false;
                this.selectedNode.Size = new Size(120, 120);


                this.rawRenderer.Panel1.Controls.Add(picture);
                picture.Controls.Add(selectedNode);

                drawNotes(picture);
            }
                

            return rawRenderer;
        }

        private void drawNotes(PictureBox parentPicture)
        {
            foreach (var node in MainForm.Model.Nodes.Where(n => n.Notes != null && n.Notes.Count > 0))
            {
                var pos = node.GetScreenPosition();
                foreach (var notePic in node.Notes)
                {
                    notePic.Image.Location = new Point(pos.X-5, pos.Y-10);
                    parentPicture.Controls.Add(notePic.Image);
                }
            }
        }

        private void CtrlToRender_Click(object sender, MouseEventArgs e)
        {
            var clickedNode = getClickedNode(e);
            if (null != clickedNode)
            {
                if (e.Button == MouseButtons.Left && !Debugger.Instance.IsRunning)
                {
                    designRender(clickedNode, clickedNode.GetScreenPosition());
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Debugger.Instance.AddRemoveBreakPoint(clickedNode);
                }
            }
        }

        private void designRender(IUINodeControl renderer, Rectangle clickArea)
        {
            selectedNode.Visible = false;
            this.rawRenderer.Panel2.Controls.Clear();
            var designControl = renderer.GetDesignEditor();
            ((DesignEditor)designControl).Close += (s, evt) => selectedNode.Visible = false;
            designControl.Dock = DockStyle.Fill;
            this.rawRenderer.Panel2.Controls.Add(designControl);
            designControl.Visible = true;
            selectedNode.Location = new Point(clickArea.X - 10, clickArea.Y - 10);
            selectedNode.BackColor = Color.Transparent;
            selectedNode.BringToFront();
            selectedNode.Visible = true;
        }



        private IUINodeControl getClickedNode(MouseEventArgs e)
        {
            return MainForm.Model.Nodes.Where(n=> n is IIdentity)
                                       .FirstOrDefault(node => node.GetScreenPosition().IsIn(e.X, e.Y));
        }

      

        public Control GetDesignEditor()
        {
            throw new NotImplementedException();
        }

        public Rectangle GetScreenPosition()
        {
            throw new NotImplementedException();
        }

        public IList<object> GetPayloads()
        {
            throw new NotImplementedException();
        }

        public void Close(object sender, EventArgs evt)
        {
            throw new NotImplementedException();
        }

        public List<ITicker> Tick()
        {
            throw new NotImplementedException();
        }
    }

    public class Rectangle : Attribute
    {
        private int _endX;
        private int _endY;

        private int _startX;
        private int _startY;

        public Rectangle(int x, int y)
        {
            this._startX = x;
            this._startY = y;
            this._endX = this._startX + 90;
            this._endY = this._startY + 90;
        }

        public int X { get { return _startX;  } }
        public int Y { get { return _startY; } }

        public bool IsIn(int x, int y)
        {
            return     this._startX <= x
                    && this._endX >= x
                    && this._startY <= y
                    && this._endY >= y;
        }
    }
}
