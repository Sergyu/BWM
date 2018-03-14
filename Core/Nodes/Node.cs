using System;
using Core;
using System.Collections.Generic;
using IDGenerator = Common.Singleton<Common.IdentityGenerator>;
using System.Linq;
using Common;

namespace Core.Nodes
{
    public abstract class Node<T>: IIn<T>, IOut<T>, IIdentity
         where T : class
    {
        private readonly string nodeIdentity;
        protected List<IIn<T>> children;
        protected Token<T> _inToken;

        public Node(string name)
        {
            this.Id = IDGenerator.Instance.GetNewId();
            this.nodeIdentity = this.GetType() + "_" + this.Id;
            this.children = new List<IIn<T>>();
            this.Name = name;
        }

        public virtual void In(Token<T> input)
        {
            this._inToken = input;
        }

        protected virtual void Out()
        {
            this._inToken = null;
        }

        public string Name { get; set; }


        public int Id { get; }

        public override string ToString()
        {
            return this.Name + "_" + this.nodeIdentity;
        }

        public virtual void AddChild(IIn<T> node)
        {
            this.children.Add(node);
        }

        public virtual List<ITicker> Tick()
        {
            var token = this._inToken;
            this.children.ForEach(n => n.In(token));
            this.Out();
            return this.children.Cast<ITicker>().ToList();
        }

        public IList<T> GetPayloads()
        {
            return this._inToken.Payload.ToList<T>();
        }
    }
}
