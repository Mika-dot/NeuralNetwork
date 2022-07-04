# convolution

```c#
 Network convolution = new Network();

            int[,] picture = convolution.ImageMatrix("C:\\Users\\akimp\\OneDrive\\Рабочий стол\\1.png");

            int[,] ConvolutionOneLayer = convolution.NeuronConvolution(picture, new int[] { 5, 0 }, new int[,] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 }, });
```

## An example of using convolution -> creating a CNN. ##


```c#
Network convolution = new Network();
            string way = "C:\\Users\\akimp\\OneDrive\\Рабочий стол\\data\\";
            int namber = Directory.GetDirectories(way).Length;
            int namber1 = 0;
            for (int i = 1; i < namber + 1; i++)
            {
                namber1 += Directory.GetFiles(way + i + "\\").Where(x => x.EndsWith("bmp") || x.EndsWith("jpg") || x.EndsWith("png")).Count();
            }

            Vector[] X = new Vector[namber1];
            Vector[] Y = new Vector[namber1];

            int nem = 0;
            int nem2 = 0;
            int min = int.MinValue;

            for (int i = 1; i < namber + 1; i++)
            {
                int chislofailov = Directory.GetFiles(way + i + "\\").Where(x => x.EndsWith("bmp") || x.EndsWith("jpg") || x.EndsWith("png")).Count();

                for (int j = 1; j < chislofailov+1; j++)
                {
                    int[,] picture1 = transformation.ImageMatrix(way + i + "\\" + i + " (" + j + ").jpg");

                    int[,] ConvolutionOneLayer1 = convolution.NeuronConvolution(picture1, new int[] { 10, 0 }, new int[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 }, });

                    int[,] ConvolutionOneLayer2 = convolution.NeuronConvolution(ConvolutionOneLayer1, new int[] { 4, 0 }, new int[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 }, });


                    X[nem] = new Vector(transformation.Vector(ConvolutionOneLayer2));
                    Y[nem] = new Vector(transformation.VectoroOutput(i, namber));

                    nem++;

                    if (min < transformation.Vector(ConvolutionOneLayer2).Length)
                    {
                        min = transformation.Vector(ConvolutionOneLayer2).Length;
                        nem2 = min;
                    }

                    Console.WriteLine(nem + " " + i + " " + j);
                }
            }
  
            Network network = new Network(new int[] { nem2, 400, 300, 100, 50, namber });

            double alpha = 0.9;
            double eps = 1e-10;
            int selector = 0;
            int output = 10;

            int indextIR = 0; // записей
            int iteration = 0; // вывод при этирации
            double error; // ошибка эпохи

            network.InputLayers(); // ввод нейрона

            do
            {
                error = 0; // обнуляем ошибку
                Random rand = new Random();
                for (int i = 0; i < X.Length; i++)
                {
                    int j = rand.Next(0, X.Length);
                    network.Forward(X[j], selector); // прямое распространение сигнала
                    network.Backward(Y[j], ref error); // обратное распространение ошибки
                    network.UpdateWeights(alpha); // обновление весовых коэффициентов
                }

                iteration++;

                Console.SetCursorPosition(1, 0);
                Console.WriteLine("error: {0}", error); // выводим в консоль номер эпохи и величину ошибку
                Console.WriteLine((iteration * 100) / output + "%");
                Console.WriteLine(indextIR + " кол. этир.");

                if (iteration == output)
                {
                    network.OutputLayers(); // сохранение нейронов
                    indextIR++;
                    iteration = 0;
                }

            } while (error > eps);

            Console.ReadKey();
            
```
