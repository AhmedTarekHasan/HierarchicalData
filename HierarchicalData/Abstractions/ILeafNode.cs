namespace HierarchicalData.Abstractions
{
    public interface ILeafNode : INode
    {
        Payload Payload { get; }
        new NodeType Type => NodeType.Leaf;
    }

    public interface ILeafNode<out TPayload> : ILeafNode where TPayload : Payload
    {
        new TPayload Payload { get; }
    }
}