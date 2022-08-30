using System;

namespace TheBase.Models.BinaryTree
{
    /// <summary>
    /// Узел бинарного дерева
    /// </summary>
    public class BiTreeNode <T,K> where T :IComparable<T>
    {
        /// <summary>
        /// Данные узла
        /// </summary>
        public  T Key { get; set; }
        
        /// <summary>
        /// Значение ноды
        /// </summary>
        public K Value { get; set; }
        
        /// <summary>
        /// Левый узел
        /// </summary>
        public BiTreeNode<T,K> ParentNode { get; set; }
        
        /// <summary>
        /// Левый узел
        /// </summary>
        public BiTreeNode<T,K> LeftNode { get; set; }
        
        /// <summary>
        /// Правый узел
        /// </summary>
        public BiTreeNode<T,K> RightNode { get; set; }

    }
}