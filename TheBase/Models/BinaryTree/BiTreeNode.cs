using System;

namespace TheBase.Models.BinaryTree
{
    /// <summary>
    /// Узел бинарного дерева
    /// </summary>
    public class BiTreeNode <T> where T :IComparable<T>
    {
        /// <summary>
        /// Данные узла
        /// </summary>
        public  T Value { get; set; }

        /// <summary>
        /// Левый узел
        /// </summary>
        public BiTreeNode<T> ParentNode { get; set; }
        
        /// <summary>
        /// Левый узел
        /// </summary>
        public BiTreeNode<T> LeftNode { get; set; }
        
        /// <summary>
        /// Правый узел
        /// </summary>
        public BiTreeNode<T> RightNode { get; set; }

    }
}