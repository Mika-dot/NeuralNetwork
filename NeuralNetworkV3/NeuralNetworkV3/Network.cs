namespace NeuralNetworkV3
{
    public class Network
    {
        public int[] layers;
        public double[][] neurons;
        public double[][] biases;
        public double[][][] weights;
        public double[][][] gradient;
        private readonly double learningRate = 0.5;

        private double[][] error;
        public double MeanSquaredError;
        public Network(int[] layerDimensions, double learningRate = 0.5)
        {
            this.learningRate = learningRate;
            InitLayers(layerDimensions);
            InitNeurons();
            InitBiases();
            InitWeights();
            InitWeightAdjustments();
            InitDerivTable();
            biases[0] = new double[layerDimensions[0] + 1];
            biases[1] = new double[layerDimensions[layerDimensions.Length - 1]];
        }
        private void InitLayers(int[] layerDimensions)
        {
            layers = new int[layerDimensions.Length];
            Array.Copy(layerDimensions, layers, layerDimensions.Length);
        }
        private void InitWeights()
        {
            // Рандомизация весов
            Random rand = new Random();
            weights = new double[layers.Length][][];

            // Первый слой весов не нужен, потому что веса относятся к предыдущему слою.
            for (int layer = 1; layer < layers.Length; layer++)
            {
                weights[layer] = new double[neurons[layer].Length][];
                for (int neuron = 0; neuron < neurons[layer].Length; neuron++)
                {
                    // Количество весов для каждого нейрона равно количеству нейронов в предыдущем слое
                    int numNeuronsPrevLayer = neurons[layer - 1].Length;
                    weights[layer][neuron] = new double[numNeuronsPrevLayer];
                    //Console.WriteLine("Слой" + слой + " num weights = " + numNeuronsPrevLayer);
                    for (int weight = 0; weight < numNeuronsPrevLayer; weight++)
                    {
                        weights[layer][neuron][weight] = rand.NextDouble() / (numNeuronsPrevLayer);
                        //Console.WriteLine("Слой " + слой + " " + веса[слой][нейрон][вес]);
                    }
                }
            }
        }
        private void InitWeightAdjustments()
        {
            // Рандомизация весов
            Random rand = new Random();
            gradient = new double[layers.Length][][];

            // Первый слой весов не нужен, потому что веса относятся к предыдущему слою.
            for (int layer = 1; layer < layers.Length; layer++)
            {
                gradient[layer] = new double[neurons[layer].Length][];
                for (int neuron = 0; neuron < neurons[layer].Length; neuron++)
                {
                    //Console.WriteLine("Новый нейрон " + нейрон);
                    // Количество весов для каждого нейрона равно количеству нейронов в предыдущем слое
                    int numNeuronsPrevLayer = neurons[layer - 1].Length;
                    //Console.WriteLine("Слой {0}, NumWeights {1}", layer, numNeuronsPrevLayer);
                    gradient[layer][neuron] = new double[numNeuronsPrevLayer];

                    for (int weight = 0; weight < numNeuronsPrevLayer; weight++)
                    {
                        gradient[layer][neuron][weight] = 2 * rand.NextDouble() - 1;
                    }
                }
            }
        }
        private void InitNeurons()
        {
            neurons = new double[layers.Length][];
            for (int layer = 0; layer < layers.Length; layer++)
            {
                neurons[layer] = new double[layers[layer]];
            }
        }
        private void InitBiases()
        {
            Random rand = new Random();
            biases = new double[layers.Length][];
            // Слой 0 будет смещением для входного слоя, который не имеет смещения.
            for (int layer = 1; layer < layers.Length; layer++)
            {
                biases[layer] = new double[neurons[layer].Length];
                for (int neuron = 0; neuron < biases[layer].Length; neuron++)
                {
                    biases[layer][neuron] = rand.NextDouble() / neurons[layer - 1].Length;
                }
            }
        }
        private void InitDerivTable()
        {
            error = new double[layers.Length][];
            for (int layer = 0; layer < layers.Length; layer++)
            {
                error[layer] = new double[layers[layer]];
                for (int neuron = 0; neuron < layers[layer]; neuron++)
                {
                    error[layer][neuron] = 0;
                }
            }
        }
        public double[] FeedForward(double[] input)
        {
            // Установка первого слоя для ввода значений
            neurons[0] = input;

            for (int layer = 1; layer < layers.Length; layer++)
            {
                for (int neuron = 0; neuron < neurons[layer].Length; neuron++)
                {
                    double sum = 0;
                    for (int weight = 0; weight < weights[layer][neuron].Length; weight++)
                    {
                        sum += neurons[layer - 1][weight] * weights[layer][neuron][weight];
                    }
                    sum += biases[layer][neuron];
                    neurons[layer][neuron] = ActivationFunction(sum);
                }
            }
            return neurons[neurons.Length - 1];
        }
        public void TrainEpoch(double[] input, double[] desiredOutput)
        {
            double[][] inputs = new double[1][];
            inputs[0] = input;
            double[][] desiredOutputs = new double[1][];
            desiredOutputs[0] = desiredOutput;
            Train(inputs, desiredOutputs);
        }
        // Принимает массив входных массивов и соответствующие желаемые выходные массивы для каждого входа.
        public void Train(double[][] inputs, double[][] desiredOutputs)
        {
            //Console.WriteLine("ПОЕЗД");
            double[] outputAverage = new double[neurons[layers.Length - 1].Length];
            double[] targetAverage = new double[neurons[layers.Length - 1].Length];
            double[] inputAverage = new double[inputs[0].Length];

            for (int input = 0; input < inputs.GetLength(0); input++)
            {
                for (int j = 0; j < inputs[input].Length; j++)
                {
                    inputAverage[j] += inputs[input][j];
                }
                double[] output = FeedForward(inputs[input]);
                //Console.WriteLine("Вход " + входы[вход][0] + " " + входы[вход][1] + " = " + выход[0]);
                for (int j = 0; j < output.Length; j++)
                {
                    //Console.WriteLine("Вывод {0:0.000} требуемый {1}", output[j], requiredOutputs[input][j]);
                    outputAverage[j] += output[j];
                    targetAverage[j] += desiredOutputs[input][j];
                }
            }

            for (int i = 0; i < outputAverage.Length; i++)
            {
                outputAverage[i] /= inputs.GetLength(0);
                targetAverage[i] /= inputs.GetLength(0);
                //Console.WriteLine("Выходное среднее: {0} целевое среднее: {1}", outputAverage[i], targetAverage[i]);
            }
            for (int i = 0; i < inputAverage.Length; i++)
            {
                inputAverage[i] /= inputs.GetLength(0);
            }
            FeedForward(inputAverage);

            Backpropagate(outputAverage, targetAverage);
        }

        public double[] CalculateError(double[] feedForwardOutput, double[] desiredOutputs)
        {
            double[] cost = new double[feedForwardOutput.Length];
            for (int i = 0; i < feedForwardOutput.Length; i++)
            {
                cost[i] = CostFunction(feedForwardOutput[i], desiredOutputs[i]);
            }
            return cost;
        }
        public double CalculateMeanSquaredError(double[] errors)
        {
            double sum = 0;
            for (int i = 0; i < errors.Length; i++)
            {
                sum += errors[i];
            }
            sum /= errors.Length;
            return sum;
        }
        private double CostFunction(double GeneratedOutput, double TargetOutput)
        {
            return Math.Pow(TargetOutput - GeneratedOutput, 2) / 2;
        }
        private void Backpropagate(double[] GeneratedOutput, double[] TargetOutput)
        {
            double[] errors = CalculateError(GeneratedOutput, TargetOutput);
            MeanSquaredError = CalculateMeanSquaredError(errors);
            //Console.WriteLine("MSE: {0:0.0000}",MeanSquaredError);

            CalculateOutputLayerWeightErrors(GeneratedOutput, TargetOutput);
            CalculateHiddenLayerWeightErrors();
            UpdateWeights();
        }
        public void CalculateOutputLayerWeightErrors(double[] GeneratedOutput, double[] TargetOutput)
        {
            int outputLayer = layers.Length - 1;
            for (int neuron = 0; neuron < neurons[outputLayer].Length; neuron++)
            {
                double dAdZ = ActivationFunctionDerivative(GeneratedOutput[neuron]);
                double dCdA = CostFunctionDerivative(GeneratedOutput[neuron], TargetOutput[neuron]);
                error[outputLayer][neuron] = dAdZ * dCdA;
                for (int weight = 0; weight < weights[outputLayer][neuron].Length; weight++)
                {
                    double dZdW = neurons[outputLayer - 1][weight];
                    gradient[outputLayer][neuron][weight] = dZdW * error[outputLayer][neuron];
                }
            }
        }
        private void CalculateHiddenLayerWeightErrors()
        {
            for (int layer = layers.Length - 2; layer > 0; layer--)
            {
                for (int neuron = 0; neuron < neurons[layer].Length; neuron++)
                {
                    double dAdZ = ActivationFunctionDerivative(neurons[layer][neuron]);
                    double dCdA = Calculate_dCdA(layer, neuron);
                    error[layer][neuron] = dCdA * dAdZ;
                    for (int weight = 0; weight < weights[layer][neuron].Length; weight++)
                    {
                        // Изменить производной стоимости по отношению к заданному весу
                        double dZdW = Calculate_dZdW(layer, neuron, weight);
                        gradient[layer][neuron][weight] = dZdW * error[layer][neuron];
                    }
                }
            }
        }
        private void UpdateWeights()
        {
            for (int layer = 1; layer < layers.Length; layer++)
            {
                for (int neuron = 0; neuron < neurons[layer].Length; neuron++)
                {
                    for (int weight = 0; weight < weights[layer][neuron].Length; weight++)
                    {
                        weights[layer][neuron][weight] -= gradient[layer][neuron][weight] * learningRate;
                    }
                }
            }
        }

        private void InputLayers(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                for (int layer = 1; layer < layers.Length; layer++)
                {
                    for (int neuron = 0; neuron < neurons[layer].Length; neuron++)
                    {
                        for (int weight = 0; weight < weights[layer][neuron].Length; weight++)
                        {
                            weights[layer][neuron][weight] = Convert.ToDouble(sr.ReadLine());
                        }
                    }
                }
            }
        }
        public void OutputLayers(string file)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                for (int layer = 1; layer < layers.Length; layer++)
                {
                    for (int neuron = 0; neuron < neurons[layer].Length; neuron++)
                    {
                        for (int weight = 0; weight < weights[layer][neuron].Length; weight++)
                        {
                            sw.WriteLine(weights[layer][neuron][weight]);
                        }
                    }
                }
            }
        }

        public double Calculate_dZdW(int layer, int neuron, int weight)
        {
            return neurons[layer - 1][weight];
        }
        public double Calculate_dAdZ(int layer, int neuron)
        {
            //Console.WriteLine("dOut/dNet = " + ActivationFunctionDerivative(neurons[layer][neuron]));
            return ActivationFunctionDerivative(neurons[layer][neuron]);
        }
        public double Calculate_dCdA(int layer, int neuron)
        {
            double dCdA = 0;
            for (int weight = 0; weight < neurons[layer + 1].Length; weight++)
            {
                dCdA += error[layer + 1][weight] * weights[layer + 1][weight][neuron];
            }
            return dCdA;
        }
        private double CostFunctionDerivative(double GeneratedOutput, double TargetOutput)
        {
            return -(TargetOutput - GeneratedOutput);
        }
        public double ActivationFunction(double x)
        {
            //return 1 / (1 + Math.Pow(Math.E, -x));
            return Math.Max(0.01 * x, x);
        }
        public double ActivationFunctionDerivative(double x)
        {
            //return x * (1 - x);
            if (x > 0)
            {
                return 1;
            }
            else
            {
                return 0.01;
            }
        }
    }
}