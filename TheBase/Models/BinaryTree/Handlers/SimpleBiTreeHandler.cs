using System;

namespace TheBase.Models.BinaryTree.Handlers
{
    internal sealed class SimpleBiTreeHandler<T, K> : IBiTreeHandler<T, K> where T : IComparable<T>
    {
        private BiTreeNode<T, K> _root;
        private int _treeSize;

        #region add_node
        public void AddNode(BiTreeNode<T, K> node)
            => Add(node);
        
        private void Add(BiTreeNode<T, K> node, BiTreeNode<T, K> currentNode = null)
        {
            if (_root == null)
            {
                _root = node;
                return;
            }

            var curNode = currentNode ?? _root;
            var compareResult = curNode.Key.CompareTo(node.Key);
            switch (compareResult)
            {
                case -1:
                {
                    if (curNode.LeftNode == null)
                    {
                        curNode.LeftNode = node;
                        _treeSize++;
                        return;
                    }
                    Add(node, curNode.LeftNode);
                    break;
                }
                case 1:
                {
                    if (curNode.RightNode == null)
                    {
                        curNode.RightNode = node;
                        _treeSize++;
                        return;
                    }
                    Add(node, curNode.RightNode);
                    break;
                }
            }
        }
        #endregion
        
        #region find_node

        public BiTreeNode<T, K> FindNode(T key) =>Find(key);
        
        private BiTreeNode<T, K> Find(T key, BiTreeNode<T, K> currentNode = null)
        {
            if (_root == null)
                return null;
            
            var curNode = currentNode;
            if (currentNode == null)
                curNode = _root;
            var compResult = curNode.Key.CompareTo(key);
            switch (compResult)
            {
                case 0:
                    return curNode;
                case -1:
                {
                    
                    if (curNode.LeftNode == null)
                    {
                        return null;
                    }
                    return Find(key, curNode.LeftNode);
                }
                case 1:
                {
                    if (curNode.LeftNode == null)
                    {
                        return null;
                    }
                    return Find(key, curNode.LeftNode);
                }
                default:
                    return null;
            }
        }

        #endregion
    }
}