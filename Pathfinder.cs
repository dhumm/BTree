using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    //Main workforce of this design
    class Pathfinder<T>
    {
        
        BinaryTreeNode<T> tree;

        public Pathfinder(BinaryTreeNode<T> tree)
        {
            this.tree = tree;
        }

        //returns a node given a value
        public BinaryTreeNode<T> getNodeFromValue(T val)
        {
            var directions = getPathFromValue(val);
            return getNodeFromPath(directions);
        }

        //returns a path given a value
        private Path getPathFromValue(T val)
        {
            Stack<BinaryTreeNode<T>> nodes = new Stack<BinaryTreeNode<T>>();
            Path retVal = new Path();
            nodes.Push(tree);
            do
            {
                while (null != nodes.Peek())
                {
                    if (nodes.Peek().Val.Equals(val))
                    {
                        retVal.reverse();
                        return retVal;
                    }
                    nodes.Push(nodes.Peek().Left);
                    retVal.push_left();
                }
                if (retVal.size > 0)
                {
                    while (retVal.size > 0 && retVal.peek() == 'r')
                    {
                        retVal.pop();
                        nodes.Pop();
                    }
                    if (retVal.size > 0 && retVal.peek() == 'l')
                    {
                        retVal.pop();
                        retVal.push_right();
                        nodes.Pop();
                        nodes.Push(nodes.Peek().Right);
                    }
                }
            } while (retVal.size > 0);
            retVal.reverse();
            return retVal;
        }

        //returns a path given a node
        private Path getPathFromNode(BinaryTreeNode<T> val)
        {
            Stack<BinaryTreeNode<T>> nodes = new Stack<BinaryTreeNode<T>>();
            Path retVal = new Path();
            nodes.Push(tree);
            do
            {
                while (null != nodes.Peek())
                {
                    if (nodes.Peek() == val)
                    {
                        retVal.reverse();
                        return retVal;
                    }
                    nodes.Push(nodes.Peek().Left);
                    retVal.push_left();
                }
                if (retVal.size > 0) {
                    while (retVal.size > 0 && retVal.peek() == 'r')
                    {
                        retVal.pop();
                        nodes.Pop();
                    }
                    if (retVal.size > 0 && retVal.peek() == 'l')
                    {
                        retVal.pop();
                        retVal.push_right();
                        nodes.Pop();
                        nodes.Push(nodes.Peek().Right);
                    }
                }
            } while (retVal.size > 0);
            retVal.reverse();
            return retVal;
        }

        //returns a node given a path
        private BinaryTreeNode<T> getNodeFromPath(Path find)
        {
            if (find.size == 0)
                return tree;
            Path copy = find;
            BinaryTreeNode<T> currentNode = tree;
            while (copy.size > 0)
            {
                char instruction = find.pop();
                if (instruction == 'l')
                    currentNode = currentNode.Left;
                else
                    currentNode = currentNode.Right;
            }
            return currentNode;
        }

        //Finds paths to two nodes and then runs down each to find the common parent
        private BinaryTreeNode<T> getCommonParent(BinaryTreeNode<T> val1, BinaryTreeNode<T> val2)
        {
            Path path1 = getPathFromNode(val1);
            Path path2 = getPathFromNode(val2);
            Path pathToNode = new Path();
            if (path1.size > 0 && path2.size > 0)
            {
                while (path1.size > 1 && path2.size > 1 && path1.peek() == (path2.peek()))
                {
                    if (path1.peek() == 'l')
                        pathToNode.push_left();
                    else
                        pathToNode.push_right();
                    path1.pop();
                    path2.pop();
                }
                pathToNode.reverse();
            }
            return getNodeFromPath(pathToNode);
        }

        //Added because of code reuse, this method matches the current node with the values of the nodes that we are looking for to add the correct
        //thing to the bfs tree
        private String BFSHelper(BinaryTreeNode<T> one, BinaryTreeNode<T> two, BinaryTreeNode<T> parent, BinaryTreeNode<T> node, bool isSearched)
        {
            if (isSearched)
                return node.Val.ToString();
            else
            {
                if (node == one)
                    return "1";
                else if (node == two)
                    return "2";
                else if (node == parent)
                    return "P";
                else
                    return "0";
            }
        }

        //Informs the print method of what lies where (had to padd out the non existant nodes for the sake of printing sparse trees)
        private Stack<String> getBFSTree(BinaryTreeNode<T> one = null, BinaryTreeNode<T> two = null, BinaryTreeNode<T> parent = null)
        {
            var isSearched = false;
            if (null == one || null == two || null == parent)
                isSearched = true;
            var parentQueue = new Queue<BinaryTreeNode<T>>();
            var childQueue = new Queue<BinaryTreeNode<T>>();
            var retVal = new Stack<String>();
            var hasRealNodes = false;
            if (null != tree)
            {
                parentQueue.Enqueue(tree);
                hasRealNodes = true;
                retVal.Push(BFSHelper(one, two, parent, tree, isSearched));
            }
            while (hasRealNodes)
            {
                var level = "";
                hasRealNodes = false;
                while (parentQueue.Count > 0)
                {
                    var currentNode = parentQueue.Dequeue();
                    if (null == currentNode)
                    {
                        childQueue.Enqueue(null);
                        childQueue.Enqueue(null);
                        level += "  ";
                    }
                    else
                    {
                        if (null != currentNode.Left)
                        {
                            hasRealNodes = true;
                            childQueue.Enqueue(currentNode.Left);
                            level += BFSHelper(one, two, parent, currentNode.Left, isSearched);
                        }
                        else
                        {
                            childQueue.Enqueue(null);
                            level += " ";
                        }
                        if (null != currentNode.Right)
                        {
                            hasRealNodes = true;
                            childQueue.Enqueue(currentNode.Right);
                            level += BFSHelper(one, two, parent, currentNode.Right, isSearched);
                        }
                        else
                        {
                            childQueue.Enqueue(null);
                            level += " ";
                        }
                    }
                }
                while (childQueue.Count > 0)
                    parentQueue.Enqueue(childQueue.Dequeue());
                retVal.Push(level);
            }
            return retVal;
        }

        //Prints a physical interpretation of the binary tree
        //if null is passed for either parameter will print tree with node values
        //if nodes are passed, the tree will print indicating the found nodes and their common parent
        public void printTreeWithParents(BinaryTreeNode<T> one = null, BinaryTreeNode<T> two = null)
        {
            var BFSTree = new Stack<String>();
            BinaryTreeNode<T> parent = null;
            if (one != null && two != null)
            {
                parent = getCommonParent(one, two);
            }
            BFSTree = getBFSTree(one, two, parent);
            var stringStack = new Stack<String>();
            String startSpace = "";
            String middleSpace = " ";
            int addedMiddleSize = 1;
            int layerDistance = 1;
            BFSTree.Pop();
            while (BFSTree.Count > 0)
            {
                String currString = "";
                currString += startSpace;
                var str = BFSTree.Pop();
                foreach (var charcter in str)
                {
                    currString += charcter + middleSpace;
                }

                if (stringStack.Count > 0)
                {
                    String prevString = stringStack.Peek();
                    Queue<Tuple<int, char>> indeces = new Queue<Tuple<int, char>>();
                    for (int i = 0; i < prevString.Length; i++)
                    {
                        buildInstructions(prevString, i, currString, layerDistance, indeces);
                    }
                    for (int i = 0; i < layerDistance; i++)
                    {
                        String layer = "";
                        var holder = new Queue<Tuple<int, char>>();
                        layer = lineBuilder(prevString, indeces, holder);
                        while (holder.Count > 0)
                            indeces.Enqueue(holder.Dequeue());
                        stringStack.Push(layer);
                    }
                    layerDistance *= 2;
                }

                stringStack.Push(currString);
                startSpace = middleSpace;
                addedMiddleSize *= 2;
                for (int i = 0; i < addedMiddleSize; i++)
                    middleSpace += " ";
            }
            while (stringStack.Count > 0)
                Console.WriteLine(stringStack.Pop());
        }

        //Builds up intructions that are used to communitcate which direction the paths need to flow
        private void buildInstructions(string prevString, int i, string currString, int layerDistance, Queue<Tuple<int, char>> indeces)
        {
            if (prevString[i] != ' ')
            {
                char direction = 'r';
                if (i + layerDistance < currString.Length)
                {
                    if (currString[i + layerDistance] != ' ')
                        direction = 'l';
                }

                var instruction = new Tuple<int, char>(i, direction);
                indeces.Enqueue(instruction);

            }
        }

        //Helps to build up the lines in between the nodes
        private string lineBuilder(String prevString, Queue<Tuple<int, char>> indeces, Queue<Tuple<int, char>> holder)
        {
            String layer = "";
            Tuple<int, char> currentInstruction = new Tuple<int, char>(0, ' ');
            if (indeces.Count > 0)
                currentInstruction = indeces.Dequeue();
            for (int j = 0; j < prevString.Length; j++)
            {
                if (j == currentInstruction.Item1)
                {
                    if (currentInstruction.Item2 == 'l')
                    {
                        currentInstruction = new Tuple<int, char>(currentInstruction.Item1 + 1, currentInstruction.Item2);
                        layer += '/';
                    }
                    else if (currentInstruction.Item2 == 'r')
                    {
                        currentInstruction = new Tuple<int, char>(currentInstruction.Item1 - 1, currentInstruction.Item2);
                        layer += '\\';
                    }
                    else
                        layer += ' ';
                    holder.Enqueue(currentInstruction);
                    if (indeces.Count > 0)
                        currentInstruction = indeces.Dequeue();
                    else break;
                }
                else
                    layer += ' ';
            }
            return layer;
        }
    }
}
