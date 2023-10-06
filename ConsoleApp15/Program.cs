using ConsoleApp15;

namespace ConsoleApp15
{
    class AVLNode
    {
        public int key;
        public AVLNode left;
        public AVLNode right;
        public int height;

        public AVLNode(int key)
        {
            this.key = key;
            this.left = null;
            this.right = null;
            this.height = 1;
        }
    }

    class AVLTree
    {
        AVLNode root;

        public AVLTree()
        {
            this.root = null;
        }

        public void insert(int key)
        {
            root = insertHelper(root, key);
        }


        private AVLNode insertHelper(AVLNode root, int key)
        {

            if (root == null)
            {
                return new AVLNode(key);
            }

            if (key < root.key)
            {
                root.left = insertHelper(root.left, key);
            }
            else if (key > root.key)
            {
                root.right = insertHelper(root.right, key);
            }

            root.height = 1 + Math.Max(getHeight(root.left), getHeight(root.right));

            int balanceFactor = getBalance(root);

            if (balanceFactor > 1)
            {
                if (key < root.left.key)
                {
                    return rightRotate(root);
                }
                else
                {
                    root.left = leftRotate(root.left);
                    return rightRotate(root);
                }
            }

            if (balanceFactor < -1)
            {
                if (key > root.right.key)
                {
                    return leftRotate(root);
                }
                else
                {
                    root.right = rightRotate(root.right);
                    return leftRotate(root);
                }
            }

            return root;
        }

        public AVLNode delete(AVLNode root, int key)
        {
            if (root == null)
            {
                return root;
            }
            else if (key < root.key)
            {
                root.left = delete(root.left, key);
            }
            else if (key > root.key)
            {
                root.right = delete(root.right, key);
            }
            else
            {
                if (root.left == null)
                {
                    AVLNode temp = root.right;
                    root = null;
                    return temp;
                }
                else if (root.right == null)
                {
                    AVLNode temp = root.left;
                    root = null;
                    return temp;
                }
                AVLNode tempS = getMinValueNode(root.right);
                root.key = tempS.key;
                root.right = delete(root.right, tempS.key);
            }

            if (root == null)
            {
                return root;
            }

            root.height = 1 + Math.Max(getHeight(root.left), getHeight(root.right));

            int balanceFactor = getBalance(root);

            if (balanceFactor > 1)
            {
                if (getBalance(root.left) >= 0)
                {
                    return rightRotate(root);
                }
                else
                {
                    root.left = leftRotate(root.left);
                    return rightRotate(root);
                }
            }

            if (balanceFactor < -1)
            {
                if (getBalance(root.right) <= 0)
                {
                    return leftRotate(root);
                }
                else
                {
                    root.right = rightRotate(root.right);
                    return leftRotate(root);
                }
            }

            return root;
        }

        public int getHeight(AVLNode root)
        {
            if (root == null)
            {
                return 0;
            }
            return root.height;
        }

        public int getBalance(AVLNode root)
        {
            if (root == null)
            {
                return 0;
            }
            return getHeight(root.left) - getHeight(root.right);
        }

        public AVLNode leftRotate(AVLNode z)
        {
            AVLNode y = z.right;
            AVLNode T2 = y.left;

            y.left = z;
            z.right = T2;

            z.height = 1 + Math.Max(getHeight(z.left), getHeight(z.right));
            y.height = 1 + Math.Max(getHeight(y.left), getHeight(y.right));

            return y;
        }

        public AVLNode rightRotate(AVLNode z)
        {
            AVLNode y = z.left;
            AVLNode T3 = y.right;

            y.right = z;
            z.left = T3;

            z.height = 1 + Math.Max(getHeight(z.left), getHeight(z.right));
            y.height = 1 + Math.Max(getHeight(y.left), getHeight(y.right));

            return y;
        }

        public AVLNode getMinValueNode(AVLNode root)
        {
            if (root == null || root.left == null)
            {
                return root;
            }
            return getMinValueNode(root.left);
        }

        public void visualize()
        {
            _visualize_helper(this.root, "", true);
        }

        private void _visualize_helper(AVLNode node, String prefix, bool isLeft)
        {
            if (node == null)
            {
                return;
            }

            String nodeStr = node.key.ToString();
            String line = prefix + (isLeft ? "├── " : "└── ");
            Console.WriteLine(line + nodeStr);

            String childPrefix = prefix + (isLeft ? "│   " : "    ");
            _visualize_helper(node.left, childPrefix, true);
            _visualize_helper(node.right, childPrefix, false);
        }

        public void inOrderTraversal()
        {
            inOrderTraversalHelper(root);
            //  System.out.println();
            Console.WriteLine();
        }

        private void inOrderTraversalHelper(AVLNode node)
        {
            if (node != null)
            {
                inOrderTraversalHelper(node.left);
                Console.WriteLine(node.key + " ");
                inOrderTraversalHelper(node.right);
            }
        }
    }
}
    internal class Program
    {
        static void Main(string[] args)
        {
        AVLTree avlTree = new AVLTree();
        // Insert nodes into the BST
        int[] anArrayNodes = {
            17, 6, 5, 20, 19, 18, 11, 14, 12, 13, 2, 4, 10
        };
        foreach (int node in anArrayNodes)
        {
            avlTree.insert(node);
        }

        // Visualize the BST
        avlTree.visualize();
        // In-order traversal of BST
        Console.Write("In-order Traversal: ");
        avlTree.inOrderTraversal();

        Console.ReadLine();
    }
 }
