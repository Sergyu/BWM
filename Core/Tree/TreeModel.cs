using Core.Nodes;
using System.Collections.Generic;
using System;
using System.Linq;


namespace Core.Tree
{
    public class TreeModel
    {
        private ITicker root;
        private ITicker end;
        private List<ITicker> currentNodes;
        private int stepRunDelay;

        public TreeModel(int stepRunDelay)
        {
            this.currentNodes = new List<ITicker>();
            this.stepRunDelay = stepRunDelay;
        }

        public void SetStartNode<T>(IIn<T> startNode) where T : class
        {
            this.root = startNode;
        }

        public void SetEndNode<T>(IIn<T> node) where T : class
        {
            this.end = node;
        }

        public void Run<T>(IInitializer<T> startNode)
        {
            this.currentNodes.Add(startNode);
            while (!gotToEnd())
            {
                var nodes = this.currentNodes.Distinct();
                this.currentNodes = new List<ITicker>();
                foreach (var node in nodes)
                {
                    this.currentNodes.AddRange(node.Tick());
                }
            }
        }

        public void Run<T>(T payload) where T : class
        {
            var token = new Token<T>(payload);
            ((IIn<T>)this.root).In(token);
            this.currentNodes = this.root.Tick();
            while (!gotToEnd())
            {
                var nodes = this.currentNodes.Distinct();
                this.currentNodes = new List<ITicker>();
                foreach (var node in nodes)
                {
                    this.currentNodes.AddRange(node.Tick());
                }
            }
        }

        private bool gotToEnd()
        {
            return this.currentNodes.Count == 0 || this.currentNodes.Count == 1 && this.currentNodes.Contains(this.end);
        }
    }
}
