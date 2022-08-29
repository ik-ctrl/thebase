using System;
using TheBase.Models.BinaryTree;

namespace TheBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var tree = new BiTree<int, string>(true);
            tree.AddNode(10, "Привет");
            tree.AddNode(11, "Мир");
            tree.AddNode(6, "Как");
            tree.AddNode(7, "дела");
            tree.AddNode(13, "это");
            tree.AddNode(12, "новый");
            tree.AddNode(6, "тыц тыц");
            var k = 100;

            tree.FindNode(6);
            k = 200;
        }
    }
}