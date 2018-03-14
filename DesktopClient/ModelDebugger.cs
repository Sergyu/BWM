using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.UIControls;
using Common;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using Core.Nodes;

namespace DesktopClient
{
    public class ModelDebugger : IModelDebugger, ISingleton
    {
        private static readonly object synchronizer = new object();
        private int delayStep;
        private List<ActiveNodePicture> prevPics = new List<ActiveNodePicture>();
        private Control parent;
        private IUIModel _model;
        private CancellationTokenSource _modelRunTask;
        private bool _isStopping;
        private bool _isStopped;
        private bool _isPaused;
        private ManualResetEvent _debugSemaphore;
        private List<ActiveNodePicture> _breakPoints;

        public object Sinhronizer => synchronizer;

        public event EventHandler OnStart;
        public event EventHandler OnStop;
        public event EventHandler OnPause;
        public event EventHandler OnContinue;



        public delegate void DebugNodeClickHandler(IUINodeControl clickedNode);
        public event DebugNodeClickHandler OnActiveNodeClick;

        public ModelDebugger()
        {
            this.delayStep = 600; //ms
            this._isStopping = false;
            this._isStopped = true;
            this._debugSemaphore = new ManualResetEvent(true);
            this._breakPoints = new List<ActiveNodePicture>();
        }

        public void SetParent(Control parent)
        {
            this.parent = parent;
        }

        public void MoveNext(IList<IUINodeControl> currentNodes)
        {
            drawActiveNodes(currentNodes);
            stopOnActiveBreakPoint(currentNodes);
            this._debugSemaphore.WaitOne();
            Thread.Sleep(delayStep);
        }

        private Delegate invokeBreakPointPause = new Action<ModelDebugger,ActiveNodePicture>((debugger, ctrl) => {
            debugger.Pause();
            debugger.ActiveNode_MouseClick(ctrl, null);
        });
        private void stopOnActiveBreakPoint(IList<IUINodeControl> currentNodes)
        {
            var firstActiveBreakPoint = this._breakPoints.FirstOrDefault(b => null != currentNodes.FirstOrDefault(node => b.UINode == node));
            if (null != firstActiveBreakPoint)
            {
                this.parent.Invoke(invokeBreakPointPause,this, firstActiveBreakPoint);
            }
        }

        public void AddRemoveBreakPoint(IUINodeControl ctrl)
        {
            var breakPoint = this._breakPoints.FirstOrDefault(b => b.UINode == ctrl);
            if (null != breakPoint)
            {
                breakPoint.Dispose();
                this._breakPoints.Remove(breakPoint);
            }
            else
            {
                breakPoint = new ActiveNodePicture(ctrl, this.parent, true);
                this._breakPoints.Add(breakPoint);
            }
        }

        public void RemoveAllBreakPoints()
        {
            this._breakPoints.ForEach(pic => pic.Dispose());
            this._breakPoints.Clear();
        }

        private void drawActiveNodes(IList<IUINodeControl> nodes)
        {
            var currentPics = new List<ActiveNodePicture>();
            foreach(var node in nodes)
            {
                var prevImage = this.prevPics.FirstOrDefault(pic => pic.UINode == node);
                if (null == prevImage)
                {
                    prevImage = new ActiveNodePicture(node, this.parent);
                    prevImage.MouseClick += ActiveNode_MouseClick;
                }
               
                currentPics.Add(prevImage);
                this.prevPics.Remove(prevImage);
            }

            dispozePreviousNodeImages();
            this.prevPics = currentPics;
        }
        
        private void dispozePreviousNodeImages()
        {
            this.prevPics.ForEach(pic => pic.Dispose());
            this.prevPics.Clear();
        }


        public bool IsRunning
        {
            get
            {
                return !this._isStopped;
            }
        }

        public void Pause()
        {
            this._isPaused = true;
            this._debugSemaphore.Reset();
            this.OnPause?.Invoke(this, EventArgs.Empty);
        }

        private void ActiveNode_MouseClick(object sender, MouseEventArgs e)
        {
            if (this._isPaused)
            {
                var node = ((ActiveNodePicture)sender).UINode;
                this.OnActiveNodeClick ? .Invoke(node);
            }
        }

        public void Continue()
        {
            this._isPaused = false;
            this._debugSemaphore.Set();
            this.OnContinue?.Invoke(this, EventArgs.Empty);
        }

        public void Start(IUIModel model)
        {
            lock (synchronizer)
            {
                if (!this._isStopped) return;
                this.initialize();
                this.initialize();
                this._model = model;
                this.OnStart?.Invoke(this, EventArgs.Empty);
                Action modelRunAction = () => this._model.Run(this);
                this._modelRunTask = new CancellationTokenSource();// Task.Run(modelRunAction);
                Task.Run(modelRunAction, this._modelRunTask.Token);
                this._isStopped = false;
            }
        }

        private Delegate stopDebugging = new Action<ModelDebugger>(debugeer => debugeer.OnStop?.Invoke(debugeer, EventArgs.Empty));
        public void Stop()
        {
            lock (synchronizer)
            {
                if (this._isStopping || this._isStopped) return;
                this._isStopping = true;
                this._modelRunTask.Cancel();
                dispozePreviousNodeImages();
                this.parent.Invoke(stopDebugging, this);
                this.dispose();
            }
        }


        private void initialize()
        {
            this._isStopping = false;
            this._isStopped = true;
            this._debugSemaphore = new ManualResetEvent(true);
        }

        private void dispose()
        {
            this._isStopped = true;
            this._isStopping = false;
            this._model = null;
            this._modelRunTask.Dispose();
            this._modelRunTask = null;
            this._debugSemaphore.Close();
            this._debugSemaphore = null;
        }
    }

    public class ActiveNodePicture : PictureBox
    {
        public IUINodeControl UINode
        {
            get;
            private set;
        }

        public bool IsBreakpoint
        {
            get;
            private set;
        }

        public ActiveNodePicture(IUINodeControl control, Control parent, bool isBreakpoint = false) : base()
        {
            this.UINode = control;
            this.IsBreakpoint = isBreakpoint;
            this.Name = (control as IIdentity).Id.ToString();
            
            SetScreenPosition(control.GetScreenPosition());
            this.Image = this.IsBreakpoint
                ? global::DesktopClient.Properties.Resources.breakpoint
                : global::DesktopClient.Properties.Resources.ActiveNode;
            parent.Invoke(addImageToParent, parent, this);
        }

        public void SetScreenPosition(UIControls.Rectangle pos)
        {
            this.Location = this.IsBreakpoint 
                ? new Point(pos.X - 2, pos.Y - 5)
                : new Point(pos.X + 15, pos.Y + 15); ;
        }

        public new void Dispose()
        {
            this.Parent.Invoke(dispoSeImageOnParentThread, this.Parent, this);
        }

        private Delegate addImageToParent = new Action<Control, PictureBox>((parent, pict) =>
        {
            parent.Controls.Add(pict);
            pict.BackColor = Color.Transparent;
            pict.BringToFront();
            pict.Visible = true;
        });

        private Delegate dispoSeImageOnParentThread = new Action<Control, PictureBox>((parent, pct) =>
        {
            pct.Visible = false;
            parent.Controls.Remove(pct);
            pct.Dispose();
        });

    }
}
