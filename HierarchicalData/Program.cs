using System;
using HierarchicalData.Abstractions;
using HierarchicalData.Implementations;

namespace HierarchicalData
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new StructuralNode
            (
                "Root",
                "Root",
                "Root",
                null
            );

            var a = new StructuralNode
            (
                "A",
                "A",
                "A",
                null
            );

            var b = new StructuralNode
            (
                "B",
                "B",
                "B",
                null
            );

            root.AddChild(a);
            root.AddChild(b);

            var c = new StructuralNode
            (
                "C",
                "C",
                "C",
                null
            );

            a.AddChild(c);

            var d = new StructuralNode
            (
                "D",
                "D",
                "D",
                null
            );

            c.AddChild(d);

            TreeNavigator nav = new TreeNavigator();
            var fp = nav.GetNodeFullPath(d);

            var ds = nav.FindNodeDepth(root, d);

            var data = new LeafNode<Employee>("e", "Ahmed", "Ahmed", d, new Employee("Ahmed", 36));

            var x2 = root.GetFlatChildren();

            fp = nav.GetNodeFullPath(data);

            c.RemoveChild(d);

            x2 = root.GetFlatChildren();

            Console.WriteLine("Hello World!");
        }
    }

    public class Employee : Payload
    {
        public string FullName { get; }
        public int Age { get; }

        public Employee(string fullName, int age)
        {
            FullName = fullName;
            Age = age;
        }
    }
}