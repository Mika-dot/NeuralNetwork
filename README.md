# Intelligence
     
#### Additionally           
            
If there is a file with neural network weights, then it can be loaded with this command
```CSharp
       network.InputLayers(); // ввод нейрона
```
Save the state of the scales is done by such a command
```CSharp
        network.OutputLayers(); // вывод нейронов
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
