// using System;
// using System.Collections;
//
// namespace TheBase.Models.BinaryTree.Handlers
// {
//     internal sealed class RecorderBiTreeHandler<T, K> : IBiTreeHandler<T, K> where T : IComparable<T>
//     {
//         private BiTreeNode<T, K> _root;
//         private readonly string _separator;
//         private int _treeSize;
//
//         public RecorderBiTreeHandler()
//         {
//             _separator = new string('-', 50);
//         }
//
//         #region add_node
//
//         public void AddNode(BiTreeNode<T, K> node)
//         {
//             Console.WriteLine(_separator);
//             Console.WriteLine("~Начало операции добавления~");
//             Add(node);
//             Console.WriteLine("~Конец операции добавления~");
//             Console.WriteLine(_separator);
//         }
//
//         private void Add(BiTreeNode<T, K> node, BiTreeNode<T, K> currentNode = null)
//         {
//             if (_root == null)
//             {
//                 IdentConsoleWrite("Проверка корневого узла на null:");
//                 IdentConsoleWrite($"Вставка корневого узла [{node.Key}:{node.Value}]", 2);
//                 _root = node;
//                 _root.ParentNode = null; 
//                 _treeSize++;
//                 return;
//             }
//
//             var curNode = currentNode ?? _root;
//             IdentConsoleWrite("Сравнение ключей текущего узла и нового:");
//             var compareResult = curNode.Key.CompareTo(node.Key);
//             IdentConsoleWrite($"Результаты проверки: {compareResult}", 2);
//             switch (compareResult)
//             {
//                 case -1:
//                 {
//                     IdentConsoleWrite("Проверка левой ноды", 2);
//                     if (curNode.LeftNode == null)
//                     {
//                         IdentConsoleWrite("Левая нода пуста", 3);
//                         IdentConsoleWrite($"Производится вставка в левой узел [{node.Key}:{node.Value}]", 3);
//                         curNode.LeftNode = node;
//                         curNode.LeftNode.ParentNode = curNode;
//                         _treeSize++;
//                         return;
//                     }
//
//                     IdentConsoleWrite("Левая нода не пуста. Спускаемся ниже", 3);
//                     Add(node, curNode.LeftNode);
//                     break;
//                 }
//                 case 1:
//                 {
//                     IdentConsoleWrite("Проверка правой ноды", 2);
//                     if (curNode.RightNode == null)
//                     {
//                         IdentConsoleWrite("Правая нода пуста", 3);
//                         IdentConsoleWrite($"Производится вставка в правый узел [{node.Key}:{node.Value}]", 3);
//                         curNode.RightNode = node;
//                         curNode.RightNode.ParentNode = curNode;
//                         _treeSize++;
//                         return;
//                     }
//
//                     IdentConsoleWrite("Правая нода не пуста. Спускаемся ниже", 3);
//                     Add(node, curNode.RightNode);
//                     break;
//                 }
//                 case 0:
//                 {
//                     IdentConsoleWrite("Нода с данным ключем уже существует", 2);
//                     IdentConsoleWrite($"Добавление отменяется [{node.Key}:{node.Value}]", 3);
//                     break;
//                 }
//             }
//         }
//
//         #endregion
//
//         #region find_node
//
//         public BiTreeNode<T, K> FindNode(T key)
//         {
//             Console.WriteLine(_separator);
//             Console.WriteLine("~Начало операции поиска~");
//             var result = Find(key);
//             Console.WriteLine(result == null ? $"~Не удалось найти ноду с заданым ключом [{key}]" : $"Найдена нода [{result.Key}:{result.Value}]");
//             Console.WriteLine("~Конец операции поиска~");
//             Console.WriteLine(_separator);
//             return result;
//         }
//         
//         private BiTreeNode<T, K> Find(T key, BiTreeNode<T, K> currentNode = null)
//         {
//             if (_root == null)
//             {
//                 IdentConsoleWrite("Проверка коренвой ноды");
//                 IdentConsoleWrite("Корневая нода пуста. Поиск отменяется. Возвращено значение null", 2);
//                 return null;
//             }
//
//             var curNode = currentNode;
//             if (currentNode == null)
//                 curNode = _root;
//
//             IdentConsoleWrite("Сравнение ключа текущей ноды и искомого ключа");
//             var compResult = curNode.Key.CompareTo(key);
//             IdentConsoleWrite($"Результат проверки ключей: {compResult}", 2);
//             switch (compResult)
//             {
//                 case 0:
//                 {
//                     IdentConsoleWrite("Ключи совпадают", 3);
//                     IdentConsoleWrite($"Возвращается текущая нода  [{curNode.Key}:{curNode.Value}]", 4);
//                     return curNode;
//                 }
//                 case -1:
//                 {
//                     IdentConsoleWrite("Проверка левой ноды", 3);
//                     if (curNode.LeftNode == null)
//                     {
//                         IdentConsoleWrite("Левая нода пуста", 4);
//                         IdentConsoleWrite($"Поиск прекращен. Вернуто значение null", 4);
//                         return null;
//                     }
//
//                     IdentConsoleWrite("Левая нода не пуста.Проврерка левой ноды", 4);
//                     return Find(key, curNode.LeftNode);
//                 }
//                 case 1:
//                 {
//                     IdentConsoleWrite("Проверка правой ноды", 3);
//                     if (curNode.RightNode == null)
//                     {
//                         IdentConsoleWrite("Правая нода пуста", 4);
//                         IdentConsoleWrite("Поиск прекращен. Вернуто значение null", 4);
//                         return null;
//                     }
//
//                     IdentConsoleWrite("Правая нода не пуста.Проверка правой ноды", 4);
//                     return Find(key, curNode.RightNode);
//                 }
//                 default:
//                     return null;
//             }
//         }
//         #endregion
//         
//         #region delete_node
//         
//         public void DeleteNode(T key)
//         {
//             IdentConsoleWrite("Поиск удаляемой ноды по ключу");
//             var deletedNode = FindNode(key);
//             if (deletedNode == null)
//             {
//                 IdentConsoleWrite("Нода не найдена. Удаление отменено");
//                 return;
//             }
//             IdentConsoleWrite("Нода обнаружена. Удаление отменено");
//             Delete(deletedNode);
//         }
//
//         private void Delete(BiTreeNode<T,K> node)
//         {
//             // var parent = node.ParentNode;
//             // IdentConsoleWrite($"Родительская нода [{parent.Key}:{parent.Value}]",2);
//             // if (parent.ParentNode == null)
//             // {
//             //     IdentConsoleWrite($"Удаление рут ноды ",3);
//             //     if (parent.LeftNode == null && parent.RightNode == null)
//             //     {
//             //         IdentConsoleWrite($"Отсутствуют дочерние узлы ",4);
//             //         IdentConsoleWrite($"Удаление рутовой ноды. ",4);
//             //         _root = null;
//             //         return;
//             //     }
//             //
//             //     if (parent.LeftNode == null)
//             //     {
//             //         IdentConsoleWrite($"Отсутствует левая нода ",4);
//             //         _root.RightNode.ParentNode = null;
//             //         _root = _root.RightNode;
//             //         IdentConsoleWrite($"Замена рутовой ноды правой нодой [{_root.Key}:{_root.Value}]. ",4);
//             //         return;
//             //     }
//             //
//             //     if (parent.RightNode == null)
//             //     {
//             //         IdentConsoleWrite($"Отсутствует правая нода ",4);
//             //         _root.LeftNode.ParentNode = null;
//             //         _root = _root.LeftNode;
//             //         IdentConsoleWrite($"Замена рутовой ноды левой нодой [{_root.Key}:{_root.Value}]. ",4);
//             //         return;
//             //     }
//             //     
//             //     IdentConsoleWrite($"Имеются две дочерних ноды. ",4);
//             //     _root.RightNode.ParentNode = null;
//             //     var tmpLNode = _root.LeftNode;
//             //     var tmpRNode = _root.RightNode;
//             //     _root.RightNode.ParentNode = null;
//             //     _root = tmpRNode;
//             //     IdentConsoleWrite($"Рут заменен правой нодой.[{_root.Key}:{_root.Value}]",4);
//             //     IdentConsoleWrite($"Перестроение дерева",4);
//             //     Add(tmpLNode);
//             // }
//             //
//             // var compResult = node.ParentNode.Key.CompareTo(node.Key);
//             // if (node.LeftNode == null && node.RightNode == null)
//             // {
//             //     if (compResult == -1)
//             //     {
//             //         node.ParentNode.LeftNode = null;
//             //         return;
//             //     }
//             //     else
//             //     {
//             //         node.ParentNode.RightNode = null;
//             //         return; 
//             //     }
//             // }
//             //
//             // if (node.LeftNode == null)
//             // {
//             //     
//             // }
//             
//             IdentConsoleWrite("Пока не удаляю ничего",5);
//             
//             
//
//
//
//
//
//         }
//         
//
//
//         #endregion
//
//         private void IdentConsoleWrite(string message, int tabCount = 1)
//             => Console.WriteLine($"{new string('\t', tabCount)}{message}");
//     }
// }