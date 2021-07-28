using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petri_Nets.Tree
{
    public delegate void TreeVisitor<T>(T nodeData);

    public class NTree<T>
    {
        public T data;
        private LinkedList<NTree<T>> children;

        public NTree(T data)
        {
            this.data = data;
            children = new LinkedList<NTree<T>>();
        }

        public void AddChild(NTree<T> data)
        {
            children.AddFirst(data);
        }

        //public NTree<T> GetChild(int i)
        //{
        //    foreach (NTree<T> n in children)
        //        if (--i == 0)
        //            return n;
        //    return null;
        //}

        //public void Traverse(NTree<T> node, TreeVisitor<T> visitor)
        //{
        //    visitor(node.data);
        //    foreach (NTree<T> kid in node.children)
        //        Traverse(kid, visitor);
        //}
    }
}
