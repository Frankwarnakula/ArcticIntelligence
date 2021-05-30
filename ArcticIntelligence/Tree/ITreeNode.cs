using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ArcticIntelligence.Tree
{
    public interface ITreeNode<T> : ITreeNode
    {
        ITreeNode<T> Root { get; }

        ITreeNode<T> Parent { get; set; }

        void SetParent(ITreeNode<T> Node, bool UpdateChildNodes = true);

        TreeNodeList<T> Children { get; }
    }

    public interface ITreeNode : INotifyPropertyChanged
    {
        Action ExpandAction { get; set; }

        IEnumerable<ITreeNode> Ancestors { get; }

        ITreeNode ParentNode { get; }

        IEnumerable<ITreeNode> ChildNodes { get; }

        IEnumerable<ITreeNode> Descendants { get; }

        void OnAncestorChanged(NodeChangeType changeType, ITreeNode node);

        void OnDescendantChanged(NodeChangeType changeType, ITreeNode node);

        int Depth { get; }

        void OnDepthChanged();

        int Height { get; }

        void OnHeightChanged();

        int Value { get; set; }

        string Name { get; set; }
    }
}
