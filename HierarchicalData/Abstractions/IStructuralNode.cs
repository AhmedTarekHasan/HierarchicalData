using System.Collections.Generic;

namespace HierarchicalData.Abstractions
{
    public delegate void ChildNodeAddedEventHandler(object sender, IStructuralNode node, INode added);

    public delegate void ChildNodeRemovedEventHandler(object sender, IStructuralNode node, INode removed);

    public interface IStructuralNode : INode
    {
        event ChildNodeAddedEventHandler ChildNodeAdded;
        event ChildNodeRemovedEventHandler ChildNodeRemoved;

        IReadOnlyCollection<INode> Children { get; }
        new NodeType Type => NodeType.Structural;

        void AddChild(INode child);
        void RemoveChild(INode child);
        IReadOnlyDictionary<string, INode> GetFlatChildren();
    }
}