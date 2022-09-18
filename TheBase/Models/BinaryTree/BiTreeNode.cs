using System;
using TheBase.Models.BinaryTree.Enums;

namespace TheBase.Models.BinaryTree
{
    public class BiTreeNode <T> where T :IComparable<T>
    {
        public  T Value { get; set; }
        
        public BiTreeNode<T> ParentNode { get; set; }
        
        public BiTreeNode<T> LeftNode { get; set; }
        
        public BiTreeNode<T> RightNode { get; set; }
        
        public Position Position { get; set; }

        public override string ToString() 
            => Value.ToString();
    }
}