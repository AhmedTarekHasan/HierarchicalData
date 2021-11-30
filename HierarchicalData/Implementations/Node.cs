using HierarchicalData.Abstractions;

namespace HierarchicalData.Implementations
{
    public abstract class Node : INode
    {
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
        public IStructuralNode Parent { get; internal set; }
    }
}