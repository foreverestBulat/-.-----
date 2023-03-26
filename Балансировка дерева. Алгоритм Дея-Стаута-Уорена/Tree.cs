using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Balancingthetree
{
    public class Knot<T> where T : IComparable<T>
    {
        public T Value;
        public Knot<T> Left;
        public Knot<T> Right;
    }

    public class Tree<T> where T : IComparable<T>
    {
        public Knot<T> Root;
        public int Count = 0;
        public int Depth = 0;

        public int iterations = 0;

        public Tree() { }

        public Tree(T[] ms)
        {
            CreateTree(ms);
        }


        // Создание дерева по массиву
        private void CreateTree(T[] ms)
        {
            for (int i = 0; i < ms.Length; i++)
            {
                Add(ms[i]);
            }
        }


        private void CreateTree(T item)
        {
            Root = new Knot<T>() { Value = item, Left = null, Right = null};
            Count++;
            Depth++;
        }

        // Добавление элемента в дерево рекурсивно
        public void Add(T item)
        {
            if (Root == null)
            {
                CreateTree(item);
                return;
            }

            AddRecursion(Root, item);
            Count++;
        }

        private void AddRecursion(Knot<T> knot, T item, int curDepth = 1)
        {
            if (knot.Value.CompareTo(item) == 1)
            {
                if (knot.Left == null)
                {
                    knot.Left = new Knot<T>() { Value = item, Right = null, Left = null };

                    if (curDepth + 1 >= Depth)
                    {
                        Depth = curDepth + 1;
                    }

                    return;
                }

                AddRecursion(knot.Left, item, ++curDepth);
            }
            else
            {
                if (knot.Right == null)
                {
                    knot.Right = new Knot<T>() { Value = item };

                    if (curDepth + 1 >= Depth)
                    {
                        Depth = curDepth + 1;
                    }

                    return;
                }

                AddRecursion(knot.Right, item, ++curDepth);
            }
        }


        // Алгоритм Дея-Стаута-Уоррена
        public void Rebalance()
        {
            // превращает из дерева в сортированный список
            TreeToVine();
            // делает из списка сбалансированное дерево
            VineToTree();
        }


        public void VineToTree()
        {
            int h = (int)Math.Ceiling(Math.Log2(Count));
            VineToBalancedTree(Root, 0, Count, h);
            Depth = h;
        }

        private void VineToBalancedTree(Knot<T> knot, int l, int r, int h)
        {
            if (knot == null) return;

            for (int i = 1; knot.Right != null; i++)
            {
                if (r == h - 1) return;
                r--;
                RotateLeft(ref knot);
                if (knot.Right == null) break;
                knot = knot.Right;
                iterations++;
            }

            iterations++;
            if (l < r + 1) VineToBalancedTree(Root, l + 1, r, h);
        }

        private void TreeToVine()
        {
            Knot<T> knot = Root;

            while (knot != null)
            {
                while (knot.Left != null)
                {
                    RotateRight(ref knot);
                    iterations++;
                }
                knot = knot.Right;
                iterations++;
            }

            Depth = Count;
        }

        private void RotateRight(ref Knot<T> parent)
        {
            var pivot = parent.Left;
            var v = parent.Value;
            parent.Value = pivot.Value;
            parent.Left = pivot.Left;
            var r = pivot.Right;
            pivot.Right = parent.Right;
            parent.Right = pivot;
            pivot.Value = v;
            pivot.Left = r;
        }

        private void RotateLeft(ref Knot<T> parent)
        {
            var pivot = parent.Right;
            var v = parent.Value;
            parent.Value = pivot.Value;
            parent.Right = pivot.Right;
            var r = pivot.Left;
            pivot.Left = parent.Left;
            parent.Left = pivot;
            pivot.Value = v;
            pivot.Right = r;
        }

        public T GetMax(Knot<T> knot)
        {
            if (knot.Right == null) return knot.Value;
            else return GetMax(knot.Right);
        }
    }
}