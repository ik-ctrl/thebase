using System;

namespace TheBase.Models.BinaryTree
{
    /// <summary>
    /// Бинарное дерево поиска
    /// </summary>
    public sealed class BiTree<T> where T : IComparable<T>
    {
        private BiTreeNode<T> _root;
        private int _size;

        public void AddNode(T value)
        {
            var node = new BiTreeNode<T>() { Value = value };
            Add(node);
        }
        public int TreeSize => _size;

        private void Add(BiTreeNode<T> newNode, BiTreeNode<T> currentNode = null)
        {
            if (_root == null)
            {
                _root = newNode;
                return;
            }

            var curNode = currentNode ?? _root;
            var compareResult = curNode.Value.CompareTo(newNode.Value);
            switch (compareResult)
            {
                case 1:
                {
                    if (curNode.LeftNode == null)
                    {
                        curNode.LeftNode = newNode;
                        curNode.LeftNode.ParentNode = curNode;
                        _size++;
                        return;
                    }

                    Add(newNode, curNode.LeftNode);
                    break;
                }
                case -1:
                {
                    if (curNode.RightNode == null)
                    {
                        curNode.RightNode = newNode;
                        curNode.RightNode.ParentNode = curNode;
                        _size++;
                        return;
                    }

                    Add(newNode, curNode.RightNode);
                    break;
                }
                case 0:
                {
                    return;
                }
            }
        }


        #region draw_tree

        public void DrawTree(WalkType walk = WalkType.LeftWalk)
        {
            if (_root == null)
            {
                Console.WriteLine("Отсутствует корень. Рисовка отменена");
            }

            if (walk == WalkType.LeftWalk)
                DrawLeftWalkType(_root);
            else
                DrawRightWalkType(_root);
        }

        private void DrawLeftWalkType(BiTreeNode<T> node, int levelNode = 0)
        {
            var currentLevel = levelNode++;
            if (node.LeftNode != null)
                DrawLeftWalkType(node.LeftNode, levelNode);
            DrawTreeNode(node, currentLevel);
            if (node.RightNode != null)
                DrawLeftWalkType(node.RightNode, levelNode);
        }

        private void DrawRightWalkType(BiTreeNode<T> node, int levelNode = 0)
        {
            var currentLevel = levelNode++;
            if (node.RightNode != null)
                DrawRightWalkType(node.RightNode, levelNode);
            DrawTreeNode(node, currentLevel);
            if (node.LeftNode != null)
                DrawRightWalkType(node.LeftNode, levelNode);
        }

        private void DrawTreeNode(BiTreeNode<T> node, int levelNode)
            => Console.WriteLine($"{new string('\t', levelNode)}[{node.Value}]");

        #endregion
    }
}