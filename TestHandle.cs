using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{

    //Sets up and runs the test program
    public static class TestHandle
    {
        //Create 26 nodes named after the letters of the alphabet
        static BinaryTreeNode<Char> test = new BinaryTreeNode<Char>('A');
        static BinaryTreeNode<Char> test2 = new BinaryTreeNode<Char>('B');
        static BinaryTreeNode<Char> test3 = new BinaryTreeNode<Char>('C');
        static BinaryTreeNode<Char> test4 = new BinaryTreeNode<Char>('D');
        static BinaryTreeNode<Char> test5 = new BinaryTreeNode<Char>('E');
        static BinaryTreeNode<Char> test6 = new BinaryTreeNode<Char>('F');
        static BinaryTreeNode<Char> test7 = new BinaryTreeNode<Char>('G');
        static BinaryTreeNode<Char> test8 = new BinaryTreeNode<Char>('H');
        static BinaryTreeNode<Char> test9 = new BinaryTreeNode<Char>('I');
        static BinaryTreeNode<Char> test10 = new BinaryTreeNode<Char>('J');
        static BinaryTreeNode<Char> test11 = new BinaryTreeNode<Char>('K');
        static BinaryTreeNode<Char> test12 = new BinaryTreeNode<Char>('L');
        static BinaryTreeNode<Char> test13 = new BinaryTreeNode<Char>('M');
        static BinaryTreeNode<Char> test14 = new BinaryTreeNode<Char>('N');
        static BinaryTreeNode<Char> test15 = new BinaryTreeNode<Char>('O');
        static BinaryTreeNode<Char> test16 = new BinaryTreeNode<Char>('P');
        static BinaryTreeNode<Char> test17 = new BinaryTreeNode<Char>('Q');
        static BinaryTreeNode<Char> test18 = new BinaryTreeNode<Char>('R');
        static BinaryTreeNode<Char> test19 = new BinaryTreeNode<Char>('S');
        static BinaryTreeNode<Char> test20 = new BinaryTreeNode<Char>('T');
        static BinaryTreeNode<Char> test21 = new BinaryTreeNode<Char>('U');
        static BinaryTreeNode<Char> test22 = new BinaryTreeNode<Char>('V');
        static BinaryTreeNode<Char> test23 = new BinaryTreeNode<Char>('W');
        static BinaryTreeNode<Char> test24 = new BinaryTreeNode<Char>('X');
        static BinaryTreeNode<Char> test25 = new BinaryTreeNode<Char>('Y');
        static BinaryTreeNode<Char> test26 = new BinaryTreeNode<Char>('Z');
        static BinaryTreeNode<Char> test27 = new BinaryTreeNode<Char>('1');
        static BinaryTreeNode<Char> test28 = new BinaryTreeNode<Char>('2');
        static BinaryTreeNode<Char> test29 = new BinaryTreeNode<Char>('3');
        static BinaryTreeNode<Char> test30 = new BinaryTreeNode<Char>('4');
        static BinaryTreeNode<Char> test31 = new BinaryTreeNode<Char>('5');
        //Builds the binary tree used for testing
        static void setupTree()
        {

            test.Left = test2;
            test.Right = test3;
            test2.Left = test4;
            test2.Right = test5;
            test3.Left = test6;
            test3.Right = test7;
            test4.Left = test8;
            test4.Right = test9;
            test5.Left = test10;
            test5.Right = test11;
            test6.Left = test12;
            test6.Right = test13;
            test7.Left = test14;
            test7.Right = test15;
            test8.Left = test16;
            test8.Right = test17;
            test9.Left = test18;
            test9.Right = test19;
            test10.Left = test20;
            test10.Right = test21;
            test11.Left = test22;
            test11.Right = test23;
            test12.Left = test24;
            test12.Right = test25;
            test13.Left = test26;
            test13.Right = test27;
            test14.Left = test28;
            test14.Right = test29;
            test15.Left = test30;
            test15.Right = test31;
        }

        //Runs the test program
        public static void testRun()
        {
            setupTree();
            var finder = new Pathfinder<char>(test);
            while (true)
            {
                finder.printTreeWithParents();
                bool oneFound = false;
                bool twoFound = false;
                char one = ' ';
                char two = ' ';
                while (true)
                {
                    Console.WriteLine("Enter the letters of the two nodes to find parent of.");
                    var input = Console.ReadLine();
                    input = input.ToUpper();
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (!oneFound && ((input[i] >= 'A' && input[i] <= 'Z') || (input[i] >= '1' && input[i] <= '5')))
                        {
                            oneFound = true;
                            one = input[i];
                        }
                        else if (oneFound && ((input[i] >= 'A' && input[i] <= 'Z') || (input[i] >= '1' && input[i] <= '5')))
                        {
                            two = input[i];
                            twoFound = true;
                            break;
                        }
                    }
                    if (oneFound && twoFound)
                        break;
                    Console.WriteLine("Please enter the names of two valid nodes!!!");
                }
                var newOne = finder.getNodeFromValue(one);
                var newTwo = finder.getNodeFromValue(two);
                finder.printTreeWithParents(newOne, newTwo);
                Console.WriteLine();
                Console.WriteLine("First selected node is represented by '1'");
                Console.WriteLine("Seconds selected node is represented by '2'");
                Console.WriteLine("Common parent represented by 'P'");
                Console.WriteLine("All other nodes represented by '0'");
                Console.WriteLine();
                Console.WriteLine("Type 'no' to stop or 'yes' to continue");
                String cont = Console.ReadLine();
                cont = cont.ToUpper();
                if (cont == "NO")
                    break;

            }
        }
    }
}
