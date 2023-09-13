using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomForest
{
    class Node
    {
        public int FeatureIndex { get; }
        public double Threshold { get; }
        public int Label { get; }
        public Node LeftChild { get; }
        public Node RightChild { get; }

        public Node(int label)
        {
            FeatureIndex = -1;
            Threshold = double.MinValue;
            Label = label;
            LeftChild = null;
            RightChild = null;
        }

        public Node(int featureIndex, double threshold, Node leftChild, Node rightChild)
        {
            FeatureIndex = featureIndex;
            Threshold = threshold;
            Label = -1;
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        public bool IsLeaf()
        {
            return LeftChild == null && RightChild == null;
        }
    }
}
