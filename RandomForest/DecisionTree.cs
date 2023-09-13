using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RandomForest
{
    class DecisionTree
    {
        private Node root;

        public void Train(List<double[]> features, List<int> labels, int maxDepth = 5)
        {
            root = BuildTree(features, labels, maxDepth);
        }

        public int Predict(double[] feature)
        {
            Node currentNode = root;

            while (currentNode.IsLeaf() == false)
            {
                if (feature[currentNode.FeatureIndex] <= currentNode.Threshold)
                    currentNode = currentNode.LeftChild;
                else
                    currentNode = currentNode.RightChild;
            }

            return currentNode.Label;
        }

        private Node BuildTree(List<double[]> features, List<int> labels, int maxDepth)
        {
            if (maxDepth == 0 || labels.Count == 0 || AreAllLabelsSame(labels))
                return new Node(labels[0]);

            int numFeatures = features[0].Length;
            int bestFeatureIndex = -1;
            double bestThreshold = double.MinValue;
            double bestGiniIndex = double.MaxValue;
            List<double[]> bestLeftFeatures = new List<double[]>();
            List<double[]> bestRightFeatures = new List<double[]>();
            List<int> bestLeftLabels = new List<int>();
            List<int> bestRightLabels = new List<int>();

            for (int i = 0; i < numFeatures; i++)
            {
                List<double> featureColumn = new List<double>();

                foreach (double[] feature in features)
                    featureColumn.Add(feature[i]);

                List<double> uniqueValues = GetUniqueValues(featureColumn);

                foreach (double threshold in uniqueValues)
                {
                    List<double[]> leftFeatures = new List<double[]>();
                    List<double[]> rightFeatures = new List<double[]>();
                    List<int> leftLabels = new List<int>();
                    List<int> rightLabels = new List<int>();

                    for (int j = 0; j < features.Count; j++)
                    {
                        if (features[j][i] <= threshold)
                        {
                            leftFeatures.Add(features[j]);
                            leftLabels.Add(labels[j]);
                        }
                        else
                        {
                            rightFeatures.Add(features[j]);
                            rightLabels.Add(labels[j]);
                        }
                    }

                    double giniIndex = CalculateGiniIndex(leftLabels, rightLabels);

                    if (giniIndex < bestGiniIndex)
                    {
                        bestFeatureIndex = i;
                        bestThreshold = threshold;
                        bestGiniIndex = giniIndex;
                        bestLeftFeatures = new List<double[]>(leftFeatures);
                        bestRightFeatures = new List<double[]>(rightFeatures);
                        bestLeftLabels = new List<int>(leftLabels);
                        bestRightLabels = new List<int>(rightLabels);
                    }
                }
            }

            Node leftChild = BuildTree(bestLeftFeatures, bestLeftLabels, maxDepth - 1);
            Node rightChild = BuildTree(bestRightFeatures, bestRightLabels, maxDepth - 1);

            return new Node(bestFeatureIndex, bestThreshold, leftChild, rightChild);
        }

        private bool AreAllLabelsSame(List<int> labels)
        {
            int firstLabel = labels[0];

            foreach (int label in labels)
            {
                if (label != firstLabel)
                    return false;
            }

            return true;
        }

        private double CalculateGiniIndex(List<int> leftLabels, List<int> rightLabels)
        {
            double totalItems = leftLabels.Count + rightLabels.Count;
            double giniLeft = 0;
            double giniRight = 0;

            List<int> allLabels = new List<int>(leftLabels);
            allLabels.AddRange(rightLabels);

            List<int> uniqueLabels = GetUniqueValues(allLabels);

            foreach (int label in uniqueLabels)
            {
                double count = CountOccurences(leftLabels, label);
                double probability = count / leftLabels.Count;
                giniLeft += probability * (1 - probability);

                count = CountOccurences(rightLabels, label);
                probability = count / rightLabels.Count;
                giniRight += probability * (1 - probability);
            }

            double giniIndex = (leftLabels.Count / totalItems) * giniLeft + (rightLabels.Count / totalItems) * giniRight;
            return giniIndex;
        }

        private double CountOccurences(List<int> list, int value)
        {
            int count = 0;

            foreach (int item in list)
            {
                if (item == value)
                    count++;
            }

            return count;
        }

        private List<T> GetUniqueValues<T>(List<T> list)
        {
            List<T> uniqueList = new List<T>();

            foreach (T item in list)
            {
                if (uniqueList.Contains(item) == false)
                    uniqueList.Add(item);
            }

            return uniqueList;
        }
    }
}
