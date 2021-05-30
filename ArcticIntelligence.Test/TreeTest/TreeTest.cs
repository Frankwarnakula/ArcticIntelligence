using ArcticIntelligence.Tree;
using NUnit.Framework;
using System.Linq;

namespace ArcticIntelligence.Test.TreeTest
{
    [TestFixture]
    public class TreeTests
    {
        [Test]
        public void JustRootNode()
        {
            // (root, 40)
            var root = new TreeNode<int>(40, "root");

            Assert.AreEqual(0, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(0, root.Descendants.Count());
            Assert.AreEqual(0, root.ChildNodes.Count());
            Assert.AreEqual(40, root.SumValue);
            Assert.IsNull(root.Parent);
        }

        [Test]
        public void RootNode_With_Three_Children()
        {
            //         (root, 40)
            // (child_1, 21) (child_1, 18)
            var root = new TreeNode<int>(40, "root");
            Assert.AreEqual(0, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(0, root.Descendants.Count());
            Assert.AreEqual(0, root.ChildNodes.Count());
            Assert.AreEqual(40, root.SumValue);
            Assert.IsNull(root.Parent);

            var child_1 = new TreeNode<int>(21, "child_1", root);
            Assert.AreEqual(1, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(1, root.Descendants.Count());
            Assert.AreEqual(1, root.ChildNodes.Count());
            Assert.AreEqual(61, root.SumValue);
            Assert.AreEqual(0, child_1.Height);
            Assert.AreEqual(1, child_1.Depth);
            Assert.AreEqual(1, child_1.Ancestors.Count());
            Assert.AreEqual(0, child_1.Descendants.Count());
            Assert.AreEqual(0, child_1.ChildNodes.Count());
            Assert.AreEqual(21, child_1.SumValue);

            var child_2 = new TreeNode<int>(18, "child_2", root);
            Assert.AreEqual(1, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(2, root.Descendants.Count());
            Assert.AreEqual(2, root.ChildNodes.Count());
            Assert.AreEqual(79, root.SumValue);
            Assert.AreEqual(0, child_2.Height);
            Assert.AreEqual(1, child_2.Depth);
            Assert.AreEqual(1, child_2.Ancestors.Count());
            Assert.AreEqual(0, child_2.Descendants.Count());
            Assert.AreEqual(0, child_2.ChildNodes.Count());
            Assert.AreEqual(18, child_2.SumValue);

            var child_3 = new TreeNode<int>(15, "child_3", root);
            Assert.AreEqual(1, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(3, root.Descendants.Count());
            Assert.AreEqual(3, root.ChildNodes.Count());
            Assert.AreEqual(94, root.SumValue);
            Assert.AreEqual(0, child_3.Height);
            Assert.AreEqual(1, child_3.Depth);
            Assert.AreEqual(1, child_3.Ancestors.Count());
            Assert.AreEqual(0, child_3.Descendants.Count());
            Assert.AreEqual(0, child_3.ChildNodes.Count());
            Assert.AreEqual(15, child_3.SumValue);
        }

        [Test]
        public void RootNode_With_Parent_One_And_Parent_Two_In_Which_Parent_One_with_Three_Children()
        {
            //                                                  (root, 40)
            //               (parent1, 36)                                                  (parent2, 34)
            // (child_1, 21) (child_2, 18) (child_2, 15)

            var root = new TreeNode<int>(40, "root");
            Assert.AreEqual(0, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(0, root.Descendants.Count());
            Assert.AreEqual(0, root.ChildNodes.Count());
            Assert.AreEqual(40, root.SumValue);
            Assert.IsNull(root.Parent);

            var parent1 = new TreeNode<int>(36, "parent1", root);
            Assert.AreEqual(1, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(1, root.Descendants.Count());
            Assert.AreEqual(1, root.ChildNodes.Count());
            Assert.AreEqual(76, root.SumValue);
            Assert.AreEqual(0, parent1.Height);
            Assert.AreEqual(1, parent1.Depth);
            Assert.AreEqual(1, parent1.Ancestors.Count());
            Assert.AreEqual(0, parent1.Descendants.Count());
            Assert.AreEqual(0, parent1.ChildNodes.Count());
            Assert.AreEqual(36, parent1.SumValue);

            var parent2 = new TreeNode<int>(34, "parent2", root);
            Assert.AreEqual(1, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(2, root.Descendants.Count());
            Assert.AreEqual(2, root.ChildNodes.Count());
            Assert.AreEqual(110, root.SumValue);
            Assert.AreEqual(0, parent1.Height);
            Assert.AreEqual(1, parent1.Depth);
            Assert.AreEqual(1, parent1.Ancestors.Count());
            Assert.AreEqual(0, parent1.Descendants.Count());
            Assert.AreEqual(0, parent1.ChildNodes.Count());
            Assert.AreEqual(36, parent1.SumValue);
            Assert.AreEqual(0, parent2.Height);
            Assert.AreEqual(1, parent2.Depth);
            Assert.AreEqual(1, parent2.Ancestors.Count());
            Assert.AreEqual(0, parent2.Descendants.Count());
            Assert.AreEqual(0, parent2.ChildNodes.Count());
            Assert.AreEqual(34, parent2.SumValue);

            var child_1 = new TreeNode<int>(21, "child_1", parent1);
            Assert.AreEqual(2, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(3, root.Descendants.Count());
            Assert.AreEqual(2, root.ChildNodes.Count());
            Assert.AreEqual(131, root.SumValue);
            Assert.AreEqual(1, parent1.Height);
            Assert.AreEqual(1, parent1.Depth);
            Assert.AreEqual(1, parent1.Ancestors.Count());
            Assert.AreEqual(1, parent1.Descendants.Count());
            Assert.AreEqual(1, parent1.ChildNodes.Count());
            Assert.AreEqual(57, parent1.SumValue);
            Assert.AreEqual(0, parent2.Height);
            Assert.AreEqual(1, parent2.Depth);
            Assert.AreEqual(1, parent2.Ancestors.Count());
            Assert.AreEqual(0, parent2.Descendants.Count());
            Assert.AreEqual(0, parent2.ChildNodes.Count());
            Assert.AreEqual(34, parent2.SumValue);
            Assert.AreEqual(0, child_1.Height);
            Assert.AreEqual(2, child_1.Depth);
            Assert.AreEqual(2, child_1.Ancestors.Count());
            Assert.AreEqual(0, child_1.Descendants.Count());
            Assert.AreEqual(0, child_1.ChildNodes.Count());
            Assert.AreEqual(21, child_1.SumValue);

            var child_2 = new TreeNode<int>(18, "child_2", parent1);
            Assert.AreEqual(2, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(4, root.Descendants.Count());
            Assert.AreEqual(2, root.ChildNodes.Count());
            Assert.AreEqual(149, root.SumValue);
            Assert.AreEqual(1, parent1.Height);
            Assert.AreEqual(1, parent1.Depth);
            Assert.AreEqual(1, parent1.Ancestors.Count());
            Assert.AreEqual(2, parent1.Descendants.Count());
            Assert.AreEqual(2, parent1.ChildNodes.Count());
            Assert.AreEqual(75, parent1.SumValue);
            Assert.AreEqual(0, parent2.Height);
            Assert.AreEqual(1, parent2.Depth);
            Assert.AreEqual(1, parent2.Ancestors.Count());
            Assert.AreEqual(0, parent2.Descendants.Count());
            Assert.AreEqual(0, parent2.ChildNodes.Count());
            Assert.AreEqual(34, parent2.SumValue);
            Assert.AreEqual(0, child_1.Height);
            Assert.AreEqual(2, child_1.Depth);
            Assert.AreEqual(2, child_1.Ancestors.Count());
            Assert.AreEqual(0, child_1.Descendants.Count());
            Assert.AreEqual(0, child_1.ChildNodes.Count());
            Assert.AreEqual(21, child_1.SumValue);
            Assert.AreEqual(0, child_2.Height);
            Assert.AreEqual(2, child_2.Depth);
            Assert.AreEqual(2, child_2.Ancestors.Count());
            Assert.AreEqual(0, child_2.Descendants.Count());
            Assert.AreEqual(0, child_2.ChildNodes.Count());
            Assert.AreEqual(18, child_2.SumValue);

            var child_3 = new TreeNode<int>(15, "child_3", parent1);
            Assert.AreEqual(2, root.Height);
            Assert.AreEqual(0, root.Depth);
            Assert.AreEqual(0, root.Ancestors.Count());
            Assert.AreEqual(5, root.Descendants.Count());
            Assert.AreEqual(2, root.ChildNodes.Count());
            Assert.AreEqual(164, root.SumValue);
            Assert.AreEqual(1, parent1.Height);
            Assert.AreEqual(1, parent1.Depth);
            Assert.AreEqual(1, parent1.Ancestors.Count());
            Assert.AreEqual(3, parent1.Descendants.Count());
            Assert.AreEqual(3, parent1.ChildNodes.Count());
            Assert.AreEqual(90, parent1.SumValue);
            Assert.AreEqual(0, parent2.Height);
            Assert.AreEqual(1, parent2.Depth);
            Assert.AreEqual(1, parent2.Ancestors.Count());
            Assert.AreEqual(0, parent2.Descendants.Count());
            Assert.AreEqual(0, parent2.ChildNodes.Count());
            Assert.AreEqual(34, parent2.SumValue);
            Assert.AreEqual(0, child_1.Height);
            Assert.AreEqual(2, child_1.Depth);
            Assert.AreEqual(2, child_1.Ancestors.Count());
            Assert.AreEqual(0, child_1.Descendants.Count());
            Assert.AreEqual(0, child_1.ChildNodes.Count());
            Assert.AreEqual(21, child_1.SumValue);
            Assert.AreEqual(0, child_2.Height);
            Assert.AreEqual(2, child_2.Depth);
            Assert.AreEqual(2, child_2.Ancestors.Count());
            Assert.AreEqual(0, child_2.Descendants.Count());
            Assert.AreEqual(0, child_2.ChildNodes.Count());
            Assert.AreEqual(18, child_2.SumValue);
            Assert.AreEqual(0, child_3.Height);
            Assert.AreEqual(2, child_3.Depth);
            Assert.AreEqual(2, child_3.Ancestors.Count());
            Assert.AreEqual(0, child_3.Descendants.Count());
            Assert.AreEqual(0, child_3.ChildNodes.Count());
            Assert.AreEqual(15, child_3.SumValue);
        }

        [Test]
        public void RootNode_With_Parent_One_And_Parent_Two_In_Which_Parent_One_with_Three_Children_Remove_Node()
        {
            //                                                  (root, 40)
            //               (parent1, 36)                                                  (parent2, 34)
            // (child_1, 21) (child_2, 18) (child_2, 15)
            var root = new TreeNode<int>(40, "root");
            var parent1 = new TreeNode<int>(36, "parent1", root);
            var parent2 = new TreeNode<int>(34, "parent2", root);
            var child_1 = new TreeNode<int>(21, "child_1", parent1);
            var child_2 = new TreeNode<int>(18, "child_2", parent1);
            var child_3 = new TreeNode<int>(15, "child_3", parent1);
            Assert.AreEqual(164, root.SumValue);
            Assert.AreEqual(90, parent1.SumValue);
            Assert.AreEqual(34, parent2.SumValue);
            Assert.AreEqual(21, child_1.SumValue);
            Assert.AreEqual(18, child_2.SumValue);
            Assert.AreEqual(15, child_3.SumValue);

            //                                       (root, 40)
            //          (parent1, 36)                                             (parent2, 34)
            // (child_1, 21)  (child_2, 15)

            parent1.Children.Remove(child_2);
            Assert.AreEqual(146, root.SumValue);
            Assert.AreEqual(72, parent1.SumValue);
            Assert.AreEqual(34, parent2.SumValue);
            Assert.AreEqual(21, child_1.SumValue);
            Assert.AreEqual(15, child_3.SumValue);
        }

    }
}
