Inorder	    DFS	Left → Root → Right
Preorder	DFS	Root → Left → Right
Postorder	DFS	Left → Right → Root
Level Order	BFS	Level by level (uses a queue)

        1
       / \
      2   3
     / \   \
    4   5   6

C# Code: DFS Traversals (Recursive)

class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

void Inorder(TreeNode root)
{
    if (root == null) return;
    Inorder(root.left);
    Console.Write(root.val + " ");
    Inorder(root.right);
}

void Preorder(TreeNode root)
{
    if (root == null) return;
    Console.Write(root.val + " ");
    Preorder(root.left);
    Preorder(root.right);
}

void Postorder(TreeNode root)
{
    if (root == null) return;
    Postorder(root.left);
    Postorder(root.right);
    Console.Write(root.val + " ");
}


✅ C# Code: BFS Traversal (Level Order)

void LevelOrder(TreeNode root)
{
    if (root == null) return;

    Queue<TreeNode> queue = new Queue<TreeNode>();
    queue.Enqueue(root);

    while (queue.Count > 0)
    {
        TreeNode node = queue.Dequeue();
        Console.Write(node.val + " ");

        if (node.left != null) queue.Enqueue(node.left);
        if (node.right != null) queue.Enqueue(node.right);
    }
}
