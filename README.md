# NeuralNetworkV3
The neural network is more selective. 

This way you can create input data on the first layer
 ```CSharp
double[][] trainingData = new double[][]
{
                new double[] { 0, 0},
                new double[] { 0, 1},
                new double[] { 1, 0},
                new double[] { 1, 1},
};
```

This is the output data for the learning process
```CSharp
double[][] trainingLabels = new double[][]
{
                new double[] { 0.0, 0.0, 0.0 },
                new double[] { 1.0, 0.0, 1.0 },
                new double[] { 1.0, 0.0, 1.0 },
                new double[] { 0.0, 0.0, 0.0 }
};
```  
   
---

Thus, the number of layers and the number of neurons in the layer are entered
```CSharp
int[] layerDimensions = new int[] { trainingData[0].Length, 3, 3, trainingLabels[0].Length };
neuralnetwork = new Network(layerDimensions);
```

---

Neural network training algorithm
```CSharp
    for (int i = 0; i < 4; i++)
    {
        neuralnetwork.TrainEpoch(trainingData[i], trainingLabels[i]);
    }
```
          
Output of neural network readings on the training sample
```CSharp
double[] output;
output = neuralnetwork.FeedForward(trainingData[0]);
output = neuralnetwork.FeedForward(trainingData[1]);
output = neuralnetwork.FeedForward(trainingData[2]);
output = neuralnetwork.FeedForward(trainingData[3]);
```         
         
#### Additionally           
            
If there is a file with neural network weights, then it can be loaded with this command
```CSharp
neuralnetwork.InputLayers(); // ввод нейрона
```
Save the state of the scales is done by such a command
```CSharp
neuralnetwork.OutputLayers(); // вывод нейронов
``` 
            

