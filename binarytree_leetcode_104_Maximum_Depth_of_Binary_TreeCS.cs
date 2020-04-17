/*
 * https://leetcode.com/problems/maximum-depth-of-binary-tree/
Given a binary tree, find its maximum depth.

The maximum depth is the number of nodes along the longest path from the root node down to the farthest leaf node.

Note: A leaf is a node with no children.

Example:

Given binary tree[3, 9, 20, null, null, 15, 7],

    3
   / \
  9  20
    /  \
   15   7
return its depth = 3.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binarytree_leetcode_104_Maximum_Depth_of_Binary_TreeCS
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

                if (level_order_collection[start_index] != -999)
                {
                    root = new treenode(level_order_collection[start_index]);
                    root.left = build_tree(level_order_collection, (2 * start_index) + 1, end_index);
                    root.right = build_tree(level_order_collection, (2 * start_index) + 2, end_index);
                    // recursively call the same tree build function to create left sub-tree and right sub tree 
                }
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

        public static int findmaxdepth(treenode root)
        {
            int MaxDepth = 0;
            // max depth of a null node should be 0 
            //  depth of a single or leaf node = 1 

            if (root != null)
            {
                // visit the tree in a depth first traversal to find the maximum depth 
                int left_MaxDepth = findmaxdepth(root.left);
                int right_Maxdepth = findmaxdepth(root.right);
                // when traversing from the child to the parent , path will increase , so path = path + 1 .
                MaxDepth = 1 + find_maximum_between_left_and_rightdepth(left_MaxDepth, right_Maxdepth);
            }

            return MaxDepth;
        }
        static int find_maximum_between_left_and_rightdepth(int left_MaxDepth, int right_Maxdepth)
        {
            if (left_MaxDepth <= right_Maxdepth)
            {
                return right_Maxdepth;
            }
            else
            {
                return left_MaxDepth;
            }
        }

        static void Main(string[] args)
        {
            // given an array containing the tree node elements 
            // -999 is used as a delimitter .
           

            int[] level_order = new int[] { 3, 9, 20, -999, -999, 15, 7 };
            // debug case 

            treenode root = null;
            // create a referance to point to the root node of the tree 

            root = build_tree(level_order, level_order.GetLowerBound(0), level_order.GetUpperBound(0));
            // call the node sequence to build the tree  from the level order 
            // returns the root referance of the tree .

            Console.WriteLine("level order traversal of the tree :");
            tree_traversal_levelorder(root);
            Console.WriteLine();
            // traverse the newly created tree using level-order traversl 

            int tree_MaxDepth = findmaxdepth(root);
            // calculate the height  of the tree 
            Console.WriteLine("the minimum depth of the tree : " + tree_MaxDepth);
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
