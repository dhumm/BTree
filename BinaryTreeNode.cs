using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    //A binary tree node object, used generic type so that anything could be applied to a node
    public class BinaryTreeNode<T>
    {
        //has properties of Left and Right children as well as a value
        private BinaryTreeNode<T> left;
        private BinaryTreeNode<T> right;
        private T val;

        public BinaryTreeNode(T val, BinaryTreeNode<T> left = null, BinaryTreeNode<T> right = null)
        {
            this.left = left;
            this.right = right;
            this.val = val;
        }

        public BinaryTreeNode<T> Left
        {
            get { return left; }
            set { left = value; }
        }

        public BinaryTreeNode<T> Right
        {
            get { return right; }
            set { right = value; }
        }

        public T Val
        {
            get { return val; }
            set { val = value; }
        }

        public String ToString()
        {
            return val.ToString();
        }
    }
}
