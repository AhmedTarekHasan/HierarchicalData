using System.Linq;
using HierarchicalData.Abstractions;

namespace HierarchicalData.Implementations
{
    public abstract class Node : INode
    {
        private IStructuralNode m_Parent;

        protected Node(
            string id,
            string name,
            string pathPart,
            NodeType type,
            IStructuralNode parent)
        {
            Id = id;
            Name = name;
            PathPart = pathPart;
            Type = type;
            Parent = parent;
        }

        public string Id { get; }
        public string Name { get; }
        public string PathPart { get; }
        public NodeType Type { get; }

        public IStructuralNode Parent
        {
            get => m_Parent;
            set
            {
                if (m_Parent != value)
                {
                    if (value != null && value.Children.All(c => c.Id != Id))
                    {
                        value.AddChild(this);
                    }
                    else if (value == null)
                    {
                        m_Parent.RemoveChild(this);
                    }

                    m_Parent = value;
                }
            }
        }
    }
}