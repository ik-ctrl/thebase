using System;
using TheBase.Models.BinaryTree.Enums;

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
        
        public void DeleteNode(T value)
        {
            var deletedNode = new BiTreeNode<T>() { Value = value };
            Delete(deletedNode);
        }
        
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
                        newNode.Side = NodeSide.LeftSide;
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
                        newNode.Side = NodeSide.RightSide;
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

        private void Delete(BiTreeNode<T> deletedNode, BiTreeNode<T> currentNode = null)
        {
            if (_root == null)
            {
                Console.WriteLine("Дерево пусто. Удаление отменено.");
                return;
            }

            var searchedNode = Find(deletedNode);
            if (searchedNode == null)
            {
                Console.WriteLine("Не удалось найти удаляемый корень. Удаление отменено.");
                return;
            }

            var maxNode = FindMax(searchedNode.LeftNode);
            if (maxNode.LeftNode != null)
            {
                maxNode.ParentNode.RightNode = maxNode.LeftNode;
                maxNode.LeftNode = null;
            }

            maxNode.ParentNode = searchedNode.ParentNode;
            if (searchedNode.Side == NodeSide.LeftSide)
            {
                searchedNode.ParentNode.LeftNode = maxNode;
                maxNode.Side = NodeSide.LeftSide;
                maxNode.LeftNode = searchedNode.LeftNode;
                maxNode.RightNode = searchedNode.RightNode;
                maxNode.LeftNode.ParentNode = maxNode;
                maxNode.RightNode.ParentNode = maxNode;
            }
            else
            {
                searchedNode.ParentNode.RightNode = maxNode;
                maxNode.Side = NodeSide.RightSide;
                maxNode.LeftNode = searchedNode.LeftNode;
                maxNode.RightNode = searchedNode.RightNode;
                maxNode.LeftNode.ParentNode = maxNode;
                maxNode.RightNode.ParentNode = maxNode;
            }
            _size--;
            searchedNode = null;
        }

        private BiTreeNode<T> FindMax(BiTreeNode<T> currentNode)
            => currentNode.RightNode != null ? FindMax(currentNode.RightNode) : currentNode;

        public BiTreeNode<T> FindNode(T value) => Find(new BiTreeNode<T>(){Value = value});
        
        private BiTreeNode<T> Find(BiTreeNode<T> searchedNode, BiTreeNode<T> currentNode = null)
        {
            if (_root == null)
            {
                Console.WriteLine("Корень пуст. поиск отменен");
                return null;
            }

            var curNode = currentNode ?? _root;
            var compResult = curNode.Value.CompareTo(searchedNode.Value);
            return compResult switch
            {
                0 => curNode,
                1 => Find(searchedNode, curNode.LeftNode),
                -1 => Find(searchedNode, curNode.RightNode),
                _ => null
            };
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
            => Console.WriteLine($"{new string('\t', levelNode)}[{node}]");

        #endregion
    }
}