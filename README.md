# NeuralNetworkV2
The neural network is more selective. 

This way you can create input data on the first layer
 ```CSharp
            Vector[] X = {
            new Vector(0, 0),
            new Vector(0, 1),
            new Vector(1, 0),
            new Vector(1, 1)
            };
```

This is the output data for the learning process
```CSharp
            // массив выходных обучающих векторов
            Vector[] Y = {
            new Vector(0.0, 0.0, 0.0), // 0 ^ 0 = 0
            new Vector(1.0, 0.0, 1.0), // 0 ^ 1 = 1
            new Vector(1.0, 0.0, 1.0), // 1 ^ 0 = 1
            new Vector(0.0, 0.0, 0.0) // 1 ^ 1 = 0
            };
```  
   
---

Thus, the number of layers and the number of neurons in the layer are entered
```CSharp
            Network network = new Network(new int[] { 2, 4, 3 });
```

Characteristics necessary for training a neural network
```CSharp
            double alpha = 0.5;
            double eps = 1e-4;
            int selector = 0;
```

---

Neural network training algorithm
```CSharp
            double error; // ошибка эпохи
            do
            {
                error = 0; // обнуляем ошибку
                // проходимся по всем элементам обучающего множества
                for (int i = 0; i < X.Length; i++)
                {                  
                    network.Forward(X[i], selector); // прямое распространение сигнала
                    network.Backward(Y[i], ref error); // обратное распространение ошибки
                    network.UpdateWeights(alpha); // обновление весовых коэффициентов
                }
                Console.WriteLine("error: {0}", error); // выводим в консоль номер эпохи и величину ошибку
            } while (error > eps);
```
          
Output of neural network readings on the training sample
```CSharp
            for (int i = 0; i < X.GetLength(0); i++)
            {
                Vector output = network.Forward(X[i], selector);
                Console.WriteLine("X: {0} {1}, Y: {2} {3} {4}, output: {5} {6} {7}", X[i][0], X[i][1], Y[i][0], Y[i][1], Y[i][2], output[0], output[1], output[2]);
            }
```         
            
#### Additionally           
            
If there is a file with neural network weights, then it can be loaded with this command
```CSharp
       network.InputLayers(); // ввод нейрона
```
Save the state of the scales is done by such a command
```CSharp
        network.OutputLayers(); // вывод нейронов
```     
            

