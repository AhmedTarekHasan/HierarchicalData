using System.Collections.Generic;
using System.Linq;
using HierarchicalData.Abstractions;

namespace HierarchicalData.Implementations
{
    public class StructuralNode : Node, IStructuralNode
    {
        private readonly List<INode> m_Children;
        private readonly Dictionary<string, INode> m_Catalog;

        public event ChildNodeAddedEventHandler ChildNodeAdded;
        public event ChildNodeRemovedEventHandler ChildNodeRemoved;

        public StructuralNode(
            string id,
            string name,
            string pathPart,
            IStructuralNode parent = null,
            List<INode> children = null) : base(id, name, pathPart, NodeType.Structural, parent)
        {
            m_Children = new List<INode>();
            m_Catalog = new Dictionary<string, INode>();

            if (children != null && children.Any())
            {
                foreach (var child in children)
                {
                    AddChild(child);
                }
            }

            parent?.AddChild(this);
        }

        public IReadOnlyCollection<INode> Children => m_Children;

        public void AddChild(INode child)
        {
            if (child != null && Children.All(c => c.Id != child.Id))
            {
                child.Parent?.RemoveChild(child);

                m_Catalog.Add(child.Id, child);
                m_Children.Add(child);

                OnDirectChildNodeAdded(child);

                if (child.Type == NodeType.Structural && child is IStructuralNode structuralNode)
                {
                    foreach (var keyValuePair in structuralNode.GetFlatChildren())
                    {
                        m_Catalog.Add(keyValuePair.Key, keyValuePair.Value);
                        OnNestedChildNodeAdded(keyValuePair.Value.Parent, keyValuePair.Value);
                    }

                    structuralNode.ChildNodeAdded += AddHandler;
                    structuralNode.ChildNodeRemoved += RemoveHandler;
                }

                child.Parent = this;
            }
        }

        public void RemoveChild(INode child)
        {
            if (child != null && Children.Any(c => c.Id == child.Id))
            {
                m_Catalog.Remove(child.Id);
                m_Children.Remove(child);

                OnDirectChildNodeRemoved(child);

                if (child.Type == NodeType.Structural && child is IStructuralNode structuralNode)
                {
                    foreach (var keyValuePair in structuralNode.GetFlatChildren())
                    {
                        m_Catalog.Remove(keyValuePair.Key);
                        OnNestedChildNodeRemoved(keyValuePair.Value.Parent, keyValuePair.Value);
                    }

                    structuralNode.ChildNodeAdded -= AddHandler;
                    structuralNode.ChildNodeRemoved -= RemoveHandler;
                }

                child.Parent = null;
            }
        }

        public IReadOnlyDictionary<string, INode> GetFlatChildren()
        {
            return m_Catalog;
        }

        protected void OnDirectChildNodeAdded(INode added)
        {
            ChildNodeAdded?.Invoke(this, this, added);
        }

        protected void OnNestedChildNodeAdded(IStructuralNode node, INode added)
        {
            ChildNodeAdded?.Invoke(this, node, added);
        }

        protected void OnDirectChildNodeRemoved(INode removed)
        {
            ChildNodeRemoved?.Invoke(this, this, removed);
        }

        protected void OnNestedChildNodeRemoved(IStructuralNode node, INode removed)
        {
            ChildNodeRemoved?.Invoke(this, node, removed);
        }

        private void AddHandler(object sender, IStructuralNode node, INode added)
        {
            m_Catalog.Add(added.Id, added);
            OnNestedChildNodeAdded(node, added);
        }

        private void RemoveHandler(object sender, IStructuralNode node, INode removed)
        {
            m_Catalog.Remove(removed.Id);
            OnNestedChildNodeRemoved(node, removed);
        }
    }
}