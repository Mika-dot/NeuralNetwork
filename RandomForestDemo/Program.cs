using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RandomForest
{
    class Program
    {
        static void Main(string[] args)
        {


            List<List<string>> valuesList = ReadFileAndSplitValues("C:\\Users\\...\\123.txt");
            
            valuesList.RemoveAt(0);

            List<double[]> features = ConvertData(AssignNumbers(valuesList), out List<int> labels);


            RandomForest randomForest = new RandomForest();
            randomForest.Train(features, labels);

            int predictedLabel = randomForest.Predict(features[0]);
            string temp = intToString(predictedLabel);

            Console.WriteLine("predictions: " + predictedLabel + " " + temp);
            ;
        }

        static string intToString(int predictedLabel)
        {
            string word = null;

            foreach (var kvp in wordNumbers)
            {
                if (kvp.Value == predictedLabel)
                {
                    word = kvp.Key;
                    break;
                }
            }
            return word;
        }

        static List<double[]> ConvertData(List<List<int>> values, out List<int> labels)
        {
            labels = values.Select(row => row[0]).ToList();

            List<double[]> features = new List<double[]>();

            foreach (List<int> row in values)
            {
                double[] feature = row.Skip(1).Select(value => (double)value).ToArray();
                features.Add(feature);
            }

            return features;
        }

        static Dictionary<string, int> wordNumbers = new Dictionary<string, int>();

        static List<List<int>> AssignNumbers(List<List<string>> valuesList)
        {
            
            int number = 1;

            List<List<int>> numberList = new List<List<int>>();

            foreach (List<string> innerList in valuesList)
            {
                List<int> innerNumberList = new List<int>();

                foreach (string word in innerList)
                {
                    int wordNumber;

                    if (!wordNumbers.ContainsKey(word))
                    {
                        wordNumbers[word] = number;
                        wordNumber = number;
                        number++;
                    }
                    else
                    {
                        wordNumber = wordNumbers[word];
                    }

                    innerNumberList.Add(wordNumber);
                }

                numberList.Add(innerNumberList);
            }

            return numberList;
        }


        static List<List<string>> ReadFileAndSplitValues(string filePath)
        {
            List<List<string>> result = new List<List<string>>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        List<string> values = new List<string>();

                        string[] splitValues = line.Split('\t');

                        foreach (string value in splitValues)
                        {
                            values.Add(value);
                        }

                        result.Add(values);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while reading the file: " + e.Message);
            }

            return result;
        }
    }

    public class RandomForest
    {
        private List<DecisionTree> trees;

        public void Train(List<double[]> features, List<int> labels, int numTrees = 10, int maxDepth = 5)
        {
            trees = new List<DecisionTree>();

            for (int i = 0; i < numTrees; i++)
            {
                DecisionTree tree = new DecisionTree();
                tree.Train(features, labels, maxDepth);
                trees.Add(tree);
            }
        }

        public int Predict(double[] feature)
        {
            Dictionary<int, int> labelCounts = new Dictionary<int, int>();

            foreach (DecisionTree tree in trees)
            {
                int label = tree.Predict(feature);

                if (labelCounts.ContainsKey(label))
                    labelCounts[label]++;
                else
                    labelCounts[label] = 1;
            }

            int maxCount = 0;
            int predictedLabel = -1;

            foreach (var kvp in labelCounts)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    predictedLabel = kvp.Key;
                }
            }

            return predictedLabel;
        }
    }

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