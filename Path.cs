using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    //Essentially a limited stack that can be used to find directions to a specific node in a binary tree
    public class Path
    {
        Stack<char> directions;

        public Path()
        {
            directions = new Stack<char>();
        }

        //Two push methods to prevent directly modifying the internal stack
        //Limits the values that can be pushed to two
        public void push_left()
        {
            directions.Push('l');
        }
        public void push_right()
        {
            directions.Push('r');
        }

        //Directly wraps the normal stack methods
        public char pop()
        {
            return directions.Pop();
        }
        public char peek()
        {
            return directions.Peek();
        }
        public void reverse()
        {
            Stack<char> temp = new Stack<char>();
            while (directions.Count > 0)
            {
                temp.Push(directions.Pop());
            }
            directions = temp;
        }

        public int size 
        {
            get { return directions.Count; }
        }

        //Custom toString (mostly used for debugging)
        public string toString
        {
            get
            {
                string s = "";
                Stack<char> temp = this.directions;
                while (temp.Count > 0)
                    s += temp.Pop();
                return s;
            }
        }
    }
}
