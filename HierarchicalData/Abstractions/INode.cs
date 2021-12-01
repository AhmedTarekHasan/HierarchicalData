namespace HierarchicalData.Abstractions
{
    public interface INode
    {
        string Id { get; }
        string Name { get; }
        string PathPart { get; }
        NodeType Type { get; }
        IStructuralNode Parent { get; set; }
    }
}