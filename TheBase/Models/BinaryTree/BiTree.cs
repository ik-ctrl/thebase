using System;

namespace TheBase.Models.BinaryTree
{
    /// <summary>
    /// Бинарное дерево поиска
    /// </summary>
    public sealed class BiTree<T> where T : IComparable<T>
    {
        private BiTreeNode<T> _root;

        public void AddNode(T value)
        {
            var node = new BiTreeNode<T>() { Value = value };
            if (_root == null)
            {
                _root = node;
                return;
            }

            var compareResult = _root.Value.CompareTo(node.Value);
            if (_root.Value.CompareTo(node.Value) == 0)
                return;

            if (compareResult < 0)
            {
                if (_root.LeftNode == null)
                {
                    _root.LeftNode = node;
                    return;
                }

                Add(node, _root.LeftNode);
            }
            else
            {
                if (_root.RightNode == null)
                {
                    _root.RightNode = node;
                    return;
                }

                Add(node, _root.RightNode);
            }
        }

        private void Add(BiTreeNode<T> newNode, BiTreeNode<T> currentNode)
        {
            var compResult = currentNode.Value.CompareTo(newNode.Value);

            if (compResult == 0)
                return;

            if (compResult > 0)
            {
                if (currentNode.LeftNode == null)
                {
                    currentNode.LeftNode = newNode;
                    return;
                }

                Add(newNode, currentNode.LeftNode);
            }
            else
            {
                if (currentNode.RightNode == null)
                {
                    currentNode.RightNode = newNode;
                    return;
                }
                Add(newNode, currentNode.RightNode);
            }
        }
    }
}