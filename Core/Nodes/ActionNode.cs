using Common;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Nodes
{
    public class ActionNode<T> : Node<T> 
        where T : class 
    {
        private Action<T> payloadExecute;

        public ActionNode(string name) : base(name)
        {
            this.payloadExecute = t => { };
        }

        public ActionNode() : this(string.Empty)
        {
        }

        public override void AddChild(IIn<T> node)
        {
            if (!this.children.IsNullOrEmpty())
            {
                throw new TreeBuilderException("ActionNode can have only one child", this);
            }
            base.AddChild(node);
        }

        public void SetAction(Action<T> payloadExecute)
        {
            this.payloadExecute = payloadExecute;
        }

        public override List<ITicker> Tick()
        {
            this.payloadExecute(this._inToken.Payload);
            return base.Tick();
        }
    }
}
