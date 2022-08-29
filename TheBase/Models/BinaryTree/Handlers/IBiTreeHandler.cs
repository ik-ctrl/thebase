using System;

namespace TheBase.Models.BinaryTree.Handlers
{
    public interface IBiTreeHandler<T,K> where T:IComparable<T>
    {
        void AddNode(BiTreeNode<T,K> node);
        BiTreeNode<T, K> FindNode(T key);
    }
}