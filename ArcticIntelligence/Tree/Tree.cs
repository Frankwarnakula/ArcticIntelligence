namespace ArcticIntelligence.Tree
{
    public class Tree<T> : TreeNode<T>
        where T : new()
    {
        public Tree() { }

        public Tree(int RootValue)
        {
            Value = RootValue;
        }
    }
}
