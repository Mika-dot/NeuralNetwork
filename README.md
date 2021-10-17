# NeuralNetworkV2
The neural network is more selective. 
An example of working with code.

            Vector[] X = {
            new Vector(0, 0),
            new Vector(0, 1),
            new Vector(1, 0),
            new Vector(1, 1)
            };

            // массив выходных обучающих векторов
            Vector[] Y = {
            new Vector(0.0, 0.0, 0.0), // 0 ^ 0 = 0
            new Vector(1.0, 0.0, 1.0), // 0 ^ 1 = 1
            new Vector(1.0, 0.0, 1.0), // 1 ^ 0 = 1
            new Vector(0.0, 0.0, 0.0) // 1 ^ 1 = 0
            };
            
            
            
            Network network = new Network(new int[] { 2, 4, 3 });

            double alpha = 0.5;
            double eps = 1e-4;
            int selector = 0;

            
            //network.InputLayers(); // ввод нейрона

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

            network.OutputLayers(); // вывод нейронов

            for (int i = 0; i < X.GetLength(0); i++)
            {
                Vector output = network.Forward(X[i], selector);
                Console.WriteLine("X: {0} {1}, Y: {2} {3} {4}, output: {5} {6} {7}", X[i][0], X[i][1], Y[i][0], Y[i][1], Y[i][2], output[0], output[1], output[2]);
            }

            Console.ReadKey();
            
            
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



https://qna.habr.com/q/1061692
