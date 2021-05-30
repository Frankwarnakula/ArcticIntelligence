using ArcticIntelligence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcticIntelligence.Tree
{
    public class TreeNode<T> : ObservableObject, ITreeNode<T>, IDisposable
        where T : new()
    {

        private ITreeNode<T> _parent;

        private string _name;

        private int _value;

        private TreeNodeList<T> _childNodes;

        public TreeNode()
        {
            this.createTreeNode(0, null, null);
        }

        public TreeNode(int value)
        {
            this.createTreeNode(value, null, null);
        }

        public TreeNode(int value, string name)
        {
            this.createTreeNode(value, name, null);
        }

        public TreeNode(int value, TreeNode<T> parent)
        {
            this.createTreeNode(value, null, parent);
        }

        public TreeNode(int value, string name, TreeNode<T> parent)
        {
            this.createTreeNode(value, name, parent);
        }

        private void createTreeNode(int value, string name, TreeNode<T> parent)
        {
            this.Value = value;
            this.Parent = parent;
            _name = name;
            _childNodes = new TreeNodeList<T>(this);
        }

        public ITreeNode ParentNode
        {
            get { return _parent; }
        }


        public ITreeNode<T> Parent
        {
            get { return _parent; }
            set { SetParent(value, true); }
        }

        public void SetParent(ITreeNode<T> node, bool updateChildNodes = true)
        {
            if (node == Parent)
                return;

            var oldParent = Parent;
            var oldParentHeight = Parent != null ? Parent.Height : 0;
            var oldDepth = Depth;

            // if oldParent isn't null
            // remove this node from its newly ex-parent's children
            if (oldParent != null && oldParent.Children.Contains(this))
                oldParent.Children.Remove(this, updateParent: false);

            // update the backing field
            _parent = node;

            // add this node to its new parent's children
            if (_parent != null && updateChildNodes)
                _parent.Children.Add(this, updateParent: false);

            // signal the old parent that it has lost this child
            if (oldParent != null)
                oldParent.OnDescendantChanged(NodeChangeType.NodeRemoved, this);

            if (oldDepth != Depth)
                OnDepthChanged();

            // if this operation has changed the height of any parent, initiate the bubble-up height changed event
            if (Parent != null)
            {
                var newParentHeight = Parent != null ? Parent.Height : 0;
                if (newParentHeight != oldParentHeight)
                    Parent.OnHeightChanged();

                Parent.OnDescendantChanged(NodeChangeType.NodeAdded, this);
            }

            OnParentChanged(oldParent, Parent);
        }

        protected virtual void OnParentChanged(ITreeNode<T> oldValue, ITreeNode<T> newValue)
        {
            OnPropertyChanged("Parent");
        }

        // TODO: add property and event notifications that are missing from this set: DescendentsChanged, AnscestorsChanged, ChildrenChanged, ParentChanged

        public ITreeNode<T> Root
        {
            get { return (Parent == null) ? this : Parent.Root; }
        }


        public TreeNodeList<T> Children
        {
            get { return _childNodes; }
        }

        public IEnumerable<ITreeNode> ChildNodes
        {
            get
            {
                foreach (ITreeNode node in Children)
                    yield return node;

                yield break;
            }
        }

        public IEnumerable<ITreeNode> Descendants
        {
            get
            {
                foreach (ITreeNode node in ChildNodes)
                {
                    yield return node;

                    foreach (ITreeNode descendant in node.Descendants)
                        yield return descendant;
                }

                yield break;
            }
        }

        public int SumValue
        {
            get
            {
                var sum = this.Value;
                foreach (ITreeNode treeNode in Descendants)
                {
                    sum += treeNode.Value;
                }
                return sum;
            }
        }

        public IEnumerable<ITreeNode> Subtree
        {
            get
            {
                yield return this;

                foreach (ITreeNode node in Descendants)
                    yield return node;

                yield break;
            }
        }

        public IEnumerable<ITreeNode> Ancestors
        {
            get
            {
                if (Parent == null)
                    yield break;

                yield return Parent;

                foreach (ITreeNode node in Parent.Ancestors)
                    yield return node;

                yield break;
            }
        }

        public event Action<NodeChangeType, ITreeNode> AncestorChanged;
        public virtual void OnAncestorChanged(NodeChangeType changeType, ITreeNode node)
        {
            if (AncestorChanged != null)
                AncestorChanged(changeType, node);

            foreach (ITreeNode<T> child in Children)
                child.OnAncestorChanged(changeType, node);
        }

        public event Action<NodeChangeType, ITreeNode> DescendantChanged;
        public virtual void OnDescendantChanged(NodeChangeType changeType, ITreeNode node)
        {
            if (DescendantChanged != null)
                DescendantChanged(changeType, node);

            if (Parent != null)
                Parent.OnDescendantChanged(changeType, node);
        }

        public int Height
        {
            get { return Children != null ? Children.Count == 0 ? 0 : Children.Max(n => n.Height) + 1 : 0; }
        }


        public virtual void OnHeightChanged()
        {

            if (Children == null)
            {
                return;
            }

            OnPropertyChanged("Height");

            foreach (ITreeNode<T> child in Children)
                child.OnHeightChanged();
        }


        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Depth
        {
            get { return (Parent == null ? 0 : Parent.Depth + 1); }
        }

        public virtual void OnDepthChanged()
        {
            OnPropertyChanged("Depth");

            if (Parent != null)
                Parent.OnDepthChanged();
        }

        private UpDownTraversalType _DisposeTraversal = UpDownTraversalType.BottomUp;
        public UpDownTraversalType DisposeTraversal
        {
            get { return _DisposeTraversal; }
            set { _DisposeTraversal = value; }
        }

        private bool _IsDisposed;
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        public Action ExpandAction { get; set; }

        public virtual void Dispose()
        {
            CheckDisposed();
            OnDisposing();

                if (DisposeTraversal == UpDownTraversalType.BottomUp)
                    foreach (TreeNode<T> node in Children)
                        node.Dispose();

                if (DisposeTraversal == UpDownTraversalType.TopDown)
                    foreach (TreeNode<T> node in Children)
                        node.Dispose();

            _IsDisposed = true;
        }

        public event EventHandler Disposing;

        protected void OnDisposing()
        {
            if (Disposing != null)
                Disposing(this, EventArgs.Empty);
        }

        public void CheckDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public override string ToString()
        {
            return string.Format("Node Name : {0}, Value : {1}, Depth : {2}, Height : {3}, Children Count : {4} ", Name, Value, Depth, Height, Children.Count);
        }
    }
}
