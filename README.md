# NeuralNetworkV2
The neural network is more selective. 

This way you can create input data on the first layer
 ```CSharp
double[,] test_questions = new double[,]
{
            { 1, 1, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 0, 1, 0, 0, 1, 0 },
            { 1, 1, 1, 0, 0, 1, 0, 0, 1 },
            { 1, 0, 0, 1, 0, 0, 1, 1, 1 },
            { 0, 0, 0, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 1, 0, 0, 1 },
            { 1, 0, 0, 1, 1, 1, 1, 0, 0 },
            { 0, 1, 0, 0, 1, 0, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 1 },
            { 1, 1, 1, 1, 0, 0, 1, 0, 0 },
            { 0, 1, 0, 1, 1, 1, 0, 1, 0 },
            { 0, 0, 1, 0, 0, 1, 1, 1, 1 },
            { 1, 0, 0, 1, 0, 0, 1, 0, 0 },
            { 1, 1, 1, 0, 1, 0, 0, 1, 0 },
            { 0, 0, 1, 1, 1, 1, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
};
```

This is the output data for the learning process
```CSharp
double[,] test_answers = new double[,]
{
            { 1, 0 },
            { 0, 1 },
            { 1, 1 },
            { 1, 1 },
            { 1, 0 },
            { 0, 1 },
            { 1, 1 },
            { 1, 1 },
            { 1, 0 },
            { 1, 1 },
            { 1, 1 },
            { 1, 1 },
            { 0, 1 },
            { 1, 1 },
            { 1, 1 },
            { 0, 0 },
};
```  
      
     --- 
      
This creates the number of layers in the neural network
> [ number of neurons, necessarily 2 ]
```CSharp
double[,] no = new double[10, 2];// нулевой слой        

double[,] n1 = new double[5, 2];// первый слой

double[,] n2 = new double[4, 2];// второй слой        

double[,] n3 = new double[2, 2];// третий слой
```

Creating a training layer, something like a reverse neural network error
> [ number of neurons on the last layers ]
```CSharp
double[,] idl = new double[2, 1];// нужно получить
```

Creating a Network Learning Factor
```CSharp
double k = 0.5;
```

creating weights for the neural network, they are related to the number of neurons in each layer of the network.
```CSharp
Network.fillW(w01); // рандом весов
Network.fillW(w12);
Network.fillW(w23);
```

Necessary requirement for randomization of layer weights
> [ number of neutrons per layer, the number of neutrons in the next layers - 1 ]
```CSharp
double[,] w01 = new double[10, 4];
double[,] w12 = new double[5, 3];
double[,] w23 = new double[4, 2];
```

--- 

Neural network training algorithm
```CSharp
for (int i = 0; i < 100000; i++)
{
    Console.WriteLine(i);
    for (int j = 0; j < test_questions.GetLength(0); j++)
    {
        Network.getTask(no, test_questions, test_answers, idl, j); // новый пример для нейросите

        Network.forWards(no, w01, n1); // проход по слою
        Network.forWards(n1, w12, n2);
        Network.forWards(n2, w23, n3);

        Network.fixOutError(idl, n3); // нахождение отклонений
        Network.findError(n2, w23, n3); // нахождение ошибок слоя
        Network.findError(n1, w12, n2);

        Network.backWards(n2, w23, n3, k); // изменение весов
        Network.backWards(n1, w12, n2, k);
        Network.backWards(no, w01, n1, k);
    }

}
```
          
Output of neural network readings on the training sample
```CSharp
    int i = Convert.ToInt32(Console.ReadLine());

    Network.getTask(no, test_questions, test_answers, idl, i);
    Network.forWards(no, w01, n1);
    Network.forWards(n1, w12, n2);
    Network.forWards(n2, w23, n3);

    Network.riad(test_questions, test_answers, i);
    Console.WriteLine();
    Network.write(n3);
```         
             
