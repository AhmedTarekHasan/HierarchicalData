using System;
using System.Collections.Generic;
using System.Linq;
using HierarchicalData.Abstractions;

namespace HierarchicalData.Implementations
{
    public class TreeNavigator
    {
        public string[] GetNodeFullPath(INode node)
        {
            var result = new List<string> { node.PathPart };

            var parent = node.Parent;

            while (parent != null)
            {
                result.Insert(0, parent.PathPart);
                parent = parent.Parent;
            }

            return result.ToArray();
        }

        public INode SearchById(IStructuralNode root, string id)
        {
            if (root?.GetFlatChildren().ContainsKey(id) ?? false)
            {
                return root.GetFlatChildren()[id];
            }

            return null;
        }

        public IList<INode> Search(IStructuralNode root, Func<INode, bool> predicate)
        {
            var flat = root?.GetFlatChildren();

            if (flat != null)
            {
                return flat
                       .Where(entry => predicate(entry.Value))
                       .Select(entry => entry.Value)
                       .ToList();
            }

            return new List<INode>();
        }

        public int FindNodeDepth(IStructuralNode root, INode node)
        {
            var foundNode = SearchById(root, node.Id);

            if (foundNode != null)
            {
                var depth = 0;
                var parent = node.Parent;

                while (parent != null)
                {
                    depth++;

                    if (parent.Id == root.Id)
                    {
                        break;
                    }

                    parent = parent.Parent;
                }

                return depth;
            }

            return 0;
        }
    }
}