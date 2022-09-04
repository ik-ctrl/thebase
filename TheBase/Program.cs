using System;
using TheBase.Models.BinaryTree;

namespace TheBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var separator = new string('-', 20);
            var tree = new BiTree<int>();
            Console.WriteLine(separator);
            tree.AddNode(10);
            tree.DrawTree(WalkType.RightWalk);
            Console.WriteLine(separator);
            tree.AddNode(20);
            tree.DrawTree(WalkType.RightWalk);
            Console.WriteLine(separator);
            tree.AddNode(30);
            tree.DrawTree(WalkType.RightWalk);
            Console.WriteLine(separator);
            tree.AddNode(5);
            tree.DrawTree(WalkType.RightWalk);
            Console.WriteLine(separator);
            tree.AddNode(4);
            tree.DrawTree(WalkType.RightWalk);
            Console.WriteLine(separator);
            tree.AddNode(8);
            tree.DrawTree(WalkType.RightWalk);
            Console.WriteLine(separator);
        }
    }
}