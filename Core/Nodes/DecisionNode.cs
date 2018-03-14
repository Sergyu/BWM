using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Nodes
{
    public class DecisionNode<T> : Node<T>, IIdentity
        where T : class
    {
        private Predicate<T> condition;
        private IIn<T> noChild = null;
        private IIn<T> yesChild = null;

        public DecisionNode(string name) : base(name)
        {
            this.condition = t => true;
        }

        public DecisionNode() : this(string.Empty) { }

        public void SetCondition(Predicate<T> cond)
        {
            this.condition = cond;
        }

        public override void AddChild(IIn<T> node)
        {
            throw new NotSupportedException("AddChild method is forbiden for a DecisionNode, instead use SetNoChild or SetYesChild.");
        }

        public void SetNoChild(IIn<T> node)
        {
            this.noChild = node;
        }

        public void SetYesChild(IIn<T> node)
        {
            this.yesChild = node;
        }

        public override List<ITicker> Tick()
        {
            var nextNode = this.condition(this._inToken.Payload) ? this.yesChild : this.noChild;
            nextNode.In(this._inToken);
            this.Out();
            return ((ITicker)nextNode).ToList();
        }
    }
}
