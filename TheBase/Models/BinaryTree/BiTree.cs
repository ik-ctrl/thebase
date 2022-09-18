using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using Microsoft.VisualBasic.FileIO;
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

        public int TreeSize => _size;

        public void AddNode(T value)
        {
            var node = new BiTreeNode<T>() { Value = value };
            Add(node);
        }

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
                _root.Position = Position.Root;
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
                        newNode.Position = Position.LeftSide;
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
                        newNode.Position = Position.RightSide;
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

            if (searchedNode.LeftNode == null && searchedNode.RightNode == null)
            {
                DeleteLeafNode(searchedNode);
                return;
            }

            var maxNode = SearchMaxNode(searchedNode);

            switch (searchedNode.Position)
            {
                case Position.LeftSide:
                {
                    maxNode.ParentNode = searchedNode.ParentNode;
                    maxNode.Position = Position.LeftSide;
                    if (maxNode.Value.CompareTo(searchedNode.LeftNode.Value) != 0)
                        maxNode.LeftNode = searchedNode.LeftNode;
                    maxNode.RightNode = searchedNode.RightNode;
                    if (maxNode.LeftNode != null)
                        maxNode.LeftNode.ParentNode = maxNode;
                    if (maxNode.RightNode != null)
                        maxNode.RightNode.ParentNode = maxNode;
                    maxNode.ParentNode.LeftNode = maxNode;
                    break;
                }
                case Position.RightSide:
                {
                    maxNode.ParentNode = searchedNode.ParentNode;
                    maxNode.Position = Position.RightSide;
                    // maxNode.RightNode = searchedNode.RightNode;
                    if (maxNode.Value.CompareTo(searchedNode.RightNode.Value) != 0)
                        maxNode.RightNode = searchedNode.RightNode;
                    if (maxNode.LeftNode != null)
                        maxNode.LeftNode.ParentNode = maxNode;
                    if (maxNode.RightNode != null)
                        maxNode.RightNode.ParentNode = maxNode;
                    maxNode.ParentNode.RightNode = maxNode;
                    break;
                }
                default:
                {
                    maxNode.Position = Position.Root;
                    if (maxNode.Value.CompareTo(searchedNode.LeftNode.Value) != 0)
                        maxNode.LeftNode = searchedNode.LeftNode;
                    maxNode.RightNode = searchedNode.RightNode;
                    if (maxNode.LeftNode != null)
                        maxNode.LeftNode.ParentNode = maxNode;
                    if (maxNode.RightNode != null)
                        maxNode.RightNode.ParentNode = maxNode;
                    if (maxNode.Position == Position.LeftSide)
                        maxNode.ParentNode.LeftNode = null;
                    else
                        maxNode.ParentNode.RightNode = null;
                    maxNode.ParentNode = null;
                    _root = maxNode;
                    break;
                }
            }

            _size--;
        }

        private BiTreeNode<T> SearchMaxNode(BiTreeNode<T> searchedNode)
        {
            var maxNode = new BiTreeNode<T>();
            if (searchedNode.LeftNode != null && searchedNode.RightNode != null)
                maxNode = FindMax(searchedNode.LeftNode);
            if (searchedNode.LeftNode == null)
                maxNode = searchedNode.RightNode;
            if (searchedNode.RightNode == null)
                maxNode = searchedNode.LeftNode;
            return maxNode;
        }

        private void DeleteLeafNode(BiTreeNode<T> searchedNode)
        {
            switch (@searchedNode.Position)
            {
                case Position.LeftSide:
                {
                    searchedNode.ParentNode.LeftNode = null;
                    return;
                }
                case Position.RightSide:
                {
                    searchedNode.ParentNode.RightNode = null;
                    return;
                }
                case Position.Root:
                {
                    _root = null;
                    return;
                }
                default:
                {
                    Console.WriteLine("kak tak?!");
                    return;
                }
            }
        }

        public BiTreeNode<T> FindNode(T value) => Find(new BiTreeNode<T>() { Value = value });

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

        private BiTreeNode<T> FindMax(BiTreeNode<T> currentNode)
            => currentNode.RightNode == null ? currentNode : FindMax(currentNode.RightNode);

        #region draw_tree

        public void DrawTree(WalkType walk = WalkType.RightWalk)
        {
            if (_root == null)
            {
                Console.WriteLine("Отсутствует корень. Рисовка отменена");
                return;
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