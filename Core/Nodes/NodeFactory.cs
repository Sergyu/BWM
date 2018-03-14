using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Nodes
{
    public class NodeFactory : ISingleton
    {
        private static readonly object synchronizer = new object();
        public object Sinhronizer => synchronizer;

        public NodeFactory()  { }

        public ITicker NewStartNode<T>(T payload, ITicker node) where T: class
        {
            var startNode = new ActionNode<T>();
            startNode.In(new Token<T>(payload));
            startNode.AddChild((IIn<T>)node);
            return startNode;
        }

        public ITicker NewEndNode<T>(ITicker node) where T : class
        {
            var endNode = new ActionNode<T>();
            ((IOut<T>)node).AddChild(endNode);
            return endNode;
        }
    }
}
