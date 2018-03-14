using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Nodes
{
    public class StartNode<T> : Node<T>, IInitializer<T>
        where T : class
    {
        public StartNode(string name) : base(name)
        {
        }

        public override void In(Token<T> input)
        {
            throw new NotSupportedException("The StartNode type doesn't support In operation.");
        }

        
        public void Initialize(Func<T> initializer)
        {
            this._inToken = new Token<T>(initializer());
        }
    }
}
