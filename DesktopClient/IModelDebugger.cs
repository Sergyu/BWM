using DesktopClient.UIControls;
using System;
using System.Collections.Generic;


namespace DesktopClient
{
    public interface IModelDebugger
    {
        void Start(IUIModel model);
        void Stop();
        void Pause();
        void Continue();

        event EventHandler OnStart;
        event EventHandler OnStop;
        event EventHandler OnPause;
        event EventHandler OnContinue;

        void MoveNext(IList<IUINodeControl> currentNodes);

        void AddRemoveBreakPoint(IUINodeControl ctrl);

        void RemoveAllBreakPoints();

        bool IsRunning { get; }
    }
}
