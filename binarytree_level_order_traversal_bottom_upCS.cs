using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 For example:
Given binary tree [3,9,20,null,null,15,7],
    3
   / \
  9  20
    /  \
   15   7
return its bottom-up level order traversal as:
[
  [15,7],
  [9,20],
  [3]
]
 */
namespace binarytree_level_order_traversal_bottom_upCS
{

    class treenode
    {
        // data members [value , left-referance , right-referance] 
        public int value;
        public treenode left;
        public treenode right;

        // constructor to initialize the tree-node values 
        public treenode(int value)
        {
            this.value = value;
            left = null;
            right = null;
        }
    }
    class Program
    {
        static treenode build_tree(int[] level_order_collection, int start_index, int end_index)
        {
            //  a tree is always built up in bottom-up structure 
            treenode root = null;
            // initial referance set to null 

            if (start_index <= end_index)
            {
                // node to be created has not over-shooted the available range 

                root = new treenode(level_order_collection[start_index]);

                root.left = build_tree(level_order_collection, (2 * start_index) + 1, end_index);
                root.right = build_tree(level_order_collection, (2 * start_index) + 2, end_index);
                // recursively call the same tree build function to create left sub-tree and right sub tree 

            }
            return root;
        }

        static void tree_traversal_levelorder(treenode root)
        {
            if (!(root == null))
            {
                // level order traversal uses a queue data structure to visit the node level by level 

                Queue<treenode> levelorder = new Queue<treenode>();
                // initialize a queue to store the referances 

                levelorder.Enqueue(root);
                // enque the front element to the queue.

                while (levelorder.Count != 0)
                {
                    // iterate the queue until all the nodes not visited[queue is not empty]

                    treenode queue_front = levelorder.Peek();
                    // get the front elelemnt in the queue.

                    // add the left referance and the right referance of the current front treenode if it exists 
                    if (queue_front.left != null)
                    {
                        levelorder.Enqueue(queue_front.left);
                    }

                    if (queue_front.right != null)
                    {
                        // add the left child of the current tree-node
                        levelorder.Enqueue(queue_front.right);
                    }

                    Console.Write((int)queue_front.value + " ");
                    // print the current node value 

                    levelorder.Dequeue();
                    // remove the front element referance 
                }


            }
        }

        static void tree_traversal_levelorder_bottom_up_level_by_level(treenode root)
        {
            if (root != null)
            {
                Queue<treenode> level_order_bottom_up_Q = new Queue<treenode>();
                Stack<treenode> level_order_bottom_up_stack = new Stack<treenode>();
                // use the Q for level order traversal [BFS] and Stack for  bottom up in  level order 

                treenode nullreferance = null;
                // define a null referance and insert into the Q to separate the levels 

                level_order_bottom_up_Q.Enqueue(root);
                level_order_bottom_up_Q.Enqueue(nullreferance);
                // fill the stack for the traversal , visit every node and push the node into the stack 
                while ((level_order_bottom_up_Q.Count != 0) && (level_order_bottom_up_Q.Peek() != null))
                {
                    // iterate the queue until it is empty or  it contains a single null referance delimitter
                    /*
                     * take the front element from Q.
                     * push the right chile and the left child in the stack.
                     * check if the end of the level reached , shift the delimitter to the rear of the queue and to the top of the stack
                     */

                    treenode current_treenode = level_order_bottom_up_Q.Peek();


                    // if the left and right sub-tree referances are not null ,put the items into the rear of the queue.
                    if (current_treenode.left != null)
                    {
                        level_order_bottom_up_Q.Enqueue(current_treenode.left);
                    }

                    if (current_treenode.right != null)
                    {
                        level_order_bottom_up_Q.Enqueue(current_treenode.right);
                    }

                    // deque the front element from the Q and push the referance to the stack 
                    current_treenode = level_order_bottom_up_Q.Dequeue();
                    level_order_bottom_up_stack.Push(current_treenode);

                    // check if we reached the end of the level in the queue. [if the front is a null referance]
                    if (level_order_bottom_up_Q.Peek() == null)
                    {
                        // if the end of the level reached :1. deque the null referance and enque the refernce to the back , 2. push the referance to the stack top 
                        nullreferance = level_order_bottom_up_Q.Dequeue();

                        // we need to add the referance node back to the queue and the stack only if the another level exists 
                        if (level_order_bottom_up_Q.Count > 1)
                        {
                            level_order_bottom_up_Q.Enqueue(nullreferance);
                            level_order_bottom_up_stack.Push(nullreferance);
                        }
                    }
                }

                // the level_order_stack_contains all the tree nodes separated by the delimitters 
                // iterate the stack and print the stack elements 

                if (level_order_bottom_up_stack.Peek() == null)
                {
                    level_order_bottom_up_stack.Pop();
                }

                while ((level_order_bottom_up_stack.Count != 0) && (level_order_bottom_up_stack.Peek() != null))
                {
                    // iterate the stack to pop the top referance and print the value 
                    Console.Write((level_order_bottom_up_stack.Pop()).value + " ");

                    // check if the level is exhausted 
                    if ((level_order_bottom_up_stack.Peek() == null) && (level_order_bottom_up_stack.Count > 1))
                    {
                        // stack undeflow condition : pop element only when an element i present 
                        if (level_order_bottom_up_stack.Count() > 0)
                        {
                            level_order_bottom_up_stack.Pop();
                            Console.WriteLine();
                        }
                        
                    }
                }

            }
        }


        public static void Main(string[] args)
        {
            // given an array containing the tree node elements 
            int[] level_order = new int[] { 3, 9, 20 };

            treenode root = null;
            // create a referance to point to the root node of the tree 

            root = build_tree(level_order, level_order.GetLowerBound(0), level_order.GetUpperBound(0));
            // call the node sequence to build the tree  from the level order 
            // returns the root referance of the tree .

            Console.WriteLine("level order traversal of the tree :");
            tree_traversal_levelorder(root);
            Console.WriteLine();
            // traverse the newly created tree using level-order traversl 

            Console.WriteLine("bottom up level by level  order traversal of the tree :");
            tree_traversal_levelorder_bottom_up_level_by_level(root);
            // traverse the tree node in bottom up level by level order 

            Console.ReadLine();
        }
    }
}
