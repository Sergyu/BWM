using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using IDGenerator = Common.Singleton<Common.IdentityGenerator>;

namespace Core.Nodes
{
    public class TransformationNode<T,C> : IIn<T>, IOut<C>, IIdentity
        where T : class 
        where C : class
    {
        private Func<T, C> translate;
        private readonly string nodeIdentity;
        protected List<IIn<C>> children;
        protected Token<T> _inToken;
        

        public TransformationNode(string name)
        {
            this.Id = IDGenerator.Instance.GetNewId();
            this.nodeIdentity = this.GetType() + "_" + this.Id;
            this.children = new List<IIn<C>>();
            this.Name = name;
            this.translate = t => default(C);
        }

        public void SetTranslator(Func<T, C> translator)
        {
            this.translate = translator;
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
            return this.Name + "_" + this.nodeIdentity;
        }

        public virtual void AddChild(IIn<C> node)
        {
            this.children.Add(node);
        }

        public virtual List<ITicker> Tick()
        {
            foreach(IIn<C> kid in this.children)
            {
                var newPayload = this.translate(this._inToken.Payload);
                var newToken = new Token<C>(newPayload);
                kid.In(newToken);
            }
            this.Out();
            return this.children.Cast<ITicker>().ToList();
        }

        public IList<T> GetPayloads()
        {
            return this._inToken.Payload.ToList<T>();
        }
    }
}
