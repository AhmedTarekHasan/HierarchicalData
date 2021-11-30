using HierarchicalData.Abstractions;

namespace HierarchicalData.Implementations
{
    public class LeafNode<TPayload> : Node, ILeafNode<TPayload> where TPayload : Payload
    {
        public LeafNode(
            string id,
            string name,
            string pathPart,
            IStructuralNode parent,
            TPayload payload) : base(id, name, pathPart, NodeType.Leaf, parent)
        {
            Payload = payload;
            parent?.AddChild(this);
        }

        Payload ILeafNode.Payload => Payload;

        public TPayload Payload { get; }
    }
}