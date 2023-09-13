namespace RandomForest
{
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
}