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
            
This creates the number of layers in the neural network
```CSharp
double[,] no = new double[10, 2];// нулевой слой        

double[,] n1 = new double[5, 2];// первый слой

double[,] n2 = new double[4, 2];// второй слой        

double[,] n3 = new double[2, 2];// третий слой
```

Creating a training layer, something like a reverse neural network error
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
```CSharp
double[,] w01 = new double[10, 4];
double[,] w12 = new double[5, 3];
double[,] w23 = new double[4, 2];
```


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
              
            
---
### [Some recommendations that I found for neural networks on the Internet](https://qna.habr.com/q/1061692)
##### [book for beginners](https://drive.google.com/file/d/1YxFuQWIst20nH-c4q2x0kfUKTXXC1zH5/view?usp=sharing)

          
There is no information about the choice of the number of layers and neurons, but briefly about the rules that I use.

| Number of hidden layers | Result |

  0 - Only able to represent linear separable functions or solutions.

  1 - can approximate any function that contains continuous mapping
from one finite space to another.

  2 - can represent an arbitrary decision boundary with arbitrary precision
with rational activation functions and can approximate any smooth
display with any precision.

---

There are many practical methods for determining the correct number of neurons to use in hidden layers, such as the following:

The number of hidden neurons must be between the size of the input layer and the size of the output layer.
The number of hidden neurons should be 2/3 the size of the input layer plus the size of the output layer.
The number of hidden neurons must be less than twice the size of the input layer.

---

However, there are heuristic rules for choosing the number of neurons in hidden layers. One of these rules is the geometric pyramid rule. According to this rule, the number of neurons in the hidden layer in a 3-layer perceptron is calculated by the following formula:

K = sqrt(m * n)

where k is the number of neurons in the hidden layer,

n is the number of neurons in the input layer;

m is the number of neurons in the output layer.

---

For a 4-layer perceptron, the number of neurons is somewhat more complicated to calculate:

r = pow( n \ m , 1 \ 3)
k(1) = m * pow( r , 2)
k(2) = m * r

where is the number of neurons in the first hidden layer;

  - the number of neurons in the second hidden layer.
