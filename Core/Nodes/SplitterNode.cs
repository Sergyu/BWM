using Common;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using IDGenerator = Common.Singleton<Common.IdentityGenerator>;

namespace Core.Nodes
{
    public class SplitterNode<T,C> : IIn<T>, IOut<C>, IIdentity
        where T : class 
        where C : class
    {
        private Func<T, IIdentity, C> _splitter;
        private readonly string _nodeIdentity;
        protected List<IIn<C>> _children;
        protected Token<T> _inToken;


        public SplitterNode(string name)
        {
            this.Id = IDGenerator.Instance.GetNewId();
            this._nodeIdentity = this.GetType() + "_" + this.Id;
            this._children = new List<IIn<C>>();
            this.Name = name;
            this._splitter = (t, idx) => default(C);
        }

        public void SetSplitter(Func<T, IIdentity, C> splitter)
        {
            this._splitter = splitter;
        }


        public void In(Token<T> token)
        {
            this._inToken = token;
        }

        protected virtual void Out()
        {
            this._inToken = null;
        }

        public string Name { get; set; }

        public int Id { get; }

        public override string ToString()
        {
            return this.Name + "_" + this._nodeIdentity;
        }

        public virtual void AddChild(IIn<C> node)
        {
            this._children.Add(node);
        }

        public virtual List<ITicker> Tick()
        {
            var token = this._inToken;
            var predicate = this._splitter;
            var nextChildren = new List<ITicker>();
            foreach (IIn<C> kid in this._children)
            {
                var newPayload = this._splitter(this._inToken.Payload, (IIdentity)kid);
                if (null != newPayload)
                {
                    var newToken = this._inToken.NewChildToken<C>(newPayload);
                    kid.In(newToken);
                    nextChildren.Add((ITicker)kid);
                }
            }
            this.Out();
            return nextChildren;
        }

        public IList<T> GetPayloads()
        {
            return this._inToken.Payload.ToList<T>();
        }
    }
}
