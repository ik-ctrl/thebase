using System;
using TheBase.Models.BinaryTree.Handlers;

namespace TheBase.Models.BinaryTree
{
    /// <summary>
    /// Бинарное дерево поиска
    /// </summary>
    public sealed class BiTree<T,K> where T : IComparable<T>
    {
        private IBiTreeHandler<T, K> _handler;


        public BiTree (bool useRecord = false)
            => _handler = useRecord ? new RecorderBiTreeHandler<T,K>() : new SimpleBiTreeHandler<T, K>();


        public void AddNode(T key, K Value)
        {
            var node = new BiTreeNode<T, K>() { Key = key, Value = Value };
            _handler.AddNode(node);
        }

        public BiTreeNode<T, K> FindNode(T key) => _handler.FindNode(key);

    }
}