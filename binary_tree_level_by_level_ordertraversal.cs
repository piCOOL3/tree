using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binarytree_level_by_level_order
{
    // class to represent a single node structure 
    class treenode
    {
        public int value;
        public treenode left;
        public treenode right;

        // constructor to initialize the tree node 
        public treenode(int value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
        }
    }
    class Program
    {
        // node-creation method , creates a node for the particular referance of the treenode 
        static treenode build_tree(char[] level_order_collection, int start_index, int end_index)
        {
            treenode root = null;

            if (start_index <= end_index)
            {
                root = new treenode(level_order_collection[start_index]);
                // create a node and refer it by root 

                root.left = build_tree(level_order_collection, (2 * start_index) + 1, end_index);
                root.right = build_tree(level_order_collection, (2 * start_index) + 2, end_index);
                // recursively call the same method to create the left sub-tree and the right sub tree 
            }
            return root;
        }

        static void tree_traversal_preorder(treenode root)
        {
            if (!(root == null))
            {
                Console.Write((char)root.value + " ");
                tree_traversal_preorder(root.left);
                tree_traversal_preorder(root.right);
            }
        }

        static void tree_traversal_postorder(treenode root)
        {
            if (!(root == null))
            {

                tree_traversal_postorder(root.left);
                tree_traversal_postorder(root.right);
                Console.Write((char)root.value + " ");
            }
        }
        static void tree_traversal_inorder(treenode root)
        {
            if (!(root == null))
            {
                tree_traversal_inorder(root.left);
                Console.Write((char)root.value + " ");
                tree_traversal_inorder(root.right);
            }
        }

        static void tree_traversal_levelorder(treenode root)
        {
            if (!(root == null))
            {
                Queue<treenode> levelorder = new Queue<treenode>();
                // initialize a queue to store the referances 

                levelorder.Enqueue(root);
                // push the root referance to the queue.

                while (levelorder.Count != 0)
                {
                    // while there is atleast one node referance left in the queue , continue 

                    treenode current_tree_node = levelorder.Peek();
                    // set the front referance element to current node 


                    if (!(current_tree_node.left == null))
                    {
                        // add the left child of the current tree-node
                        levelorder.Enqueue(current_tree_node.left);
                    }

                    if (!(current_tree_node.right == null))
                    {
                        // add the left child of the current tree-node
                        levelorder.Enqueue(current_tree_node.right);
                    }

                    Console.Write((char)current_tree_node.value + " ");
                    // print the current node value 

                    levelorder.Dequeue();
                    // remove the front element referance 
                }



            }
        }

        static void tree_traversal_level_by_level(treenode root)
        {
            // if the root referance is not  null
            if (!(root == null))
            {
                // declare a queue to hold the root referances 
                Queue<treenode> level_by_level = new Queue<treenode>();

                // enque the root node and the null referance 
                treenode null_referance = null;
                level_by_level.Enqueue(root);
                level_by_level.Enqueue(null_referance);

                // iterate the queue until it is empty 
                while ((level_by_level.Count() != 0 ) && (level_by_level.Peek() != null))
                {
                    treenode current_treenode =  level_by_level.Dequeue();

                    // print the tree node value in the level order 
                  
                    Console.Write((char)current_treenode.value + " ");
                   

                    if (current_treenode.left != null)
                    {
                        level_by_level.Enqueue(current_treenode.left);
                    }

                    if (current_treenode.right != null)
                    {
                        level_by_level.Enqueue(current_treenode.right);
                    }

                    if (level_by_level.Peek() == null)
                    {
                        // one level completely exhausted , need to change the level 
                        Console.WriteLine();
                        null_referance = level_by_level.Dequeue();
                        level_by_level.Enqueue(null_referance);
                    }

                }
            }

        }
        static void Main(string[] args)
        {
            // given a level order traversal of the nodes 
            char[] level_order = new char[] { 'D', 'B', 'E', 'A', 'F', 'C' };

            treenode root = null;
            // create a referance to point to the root node of the tree 

            root = build_tree(level_order, level_order.GetLowerBound(0), level_order.GetUpperBound(0));
            // call the node sequence to build the tree  from the level order 

            Console.WriteLine("Enter the preferred mode of traversal : ");
            Console.WriteLine("1-->Inorder    traversal");
            Console.WriteLine("2-->Preorder   traversal");
            Console.WriteLine("3-->Postorder  traversal");
            Console.WriteLine("4-->Levelorder traversal");
            Console.WriteLine("5-->Level_by_level_order traversal");
            Console.WriteLine("enter the choice in form of an integer :");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // inorder traversal
                    Console.WriteLine(" tree node elements in the  In-order traversal are : ");
                    tree_traversal_inorder(root);
                    break;
                case 2:
                    // preorder traversal 
                    Console.WriteLine(" tree node elements in the  Pre-order traversal are : ");
                    tree_traversal_preorder(root);
                    break;
                case 3:
                    // postorder traversal 
                    Console.WriteLine(" tree node elements in the  Post-order traversal are : ");
                    tree_traversal_postorder(root);
                    break;
                case 4:
                    // level order traversal 
                    Console.WriteLine(" tree node elements in the level order traversal are : ");
                    tree_traversal_levelorder(root);
                    break;

                case 5:
                    // level by level traversal for the tree 
                    Console.WriteLine("level by level traversal for the binary tree:");
                    tree_traversal_level_by_level(root);
                    break;
                default:
                    // wrong choice , ask for the rerun :
                    Console.WriteLine(" wrong choice of the menu : ");
                    break;

            }

            Console.ReadLine();
        }
    }
}