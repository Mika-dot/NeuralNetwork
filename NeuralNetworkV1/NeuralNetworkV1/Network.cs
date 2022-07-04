namespace NeuralNetworkV1
{
    public class Network
    {
        public static void forWards(double[,] li, double[,] w, double[,] lo)
        {
            int wt = w.GetLength(0);
            int ht = w.GetLength(1);

            for (int y = 0; y < ht; y++)
            {
                lo[y, 0] = 0;
                for (int x = 0; x < wt; x++)
                {
                    lo[y, 0] = lo[y, 0] + li[x, 0] * w[x, y];
                }
                lo[y, 0] = 1 / (1 + Math.Exp(-1 * lo[y, 0]));
            }
        } // проход по слою
        public static void findError(double[,] li, double[,] w, double[,] lo)
        {
            int wt = w.GetLength(0) - 1;
            int ht = w.GetLength(1);

            for (int x = 0; x < wt; x++)
            {
                li[x, 1] = 0;
                for (int y = 0; y < ht; y++)
                {
                    li[x, 1] = li[x, 1] + w[x, y] * lo[y, 1];

                }
                li[x, 1] = li[x, 1] * li[x, 0] * (1 - li[x, 0]);
            }
        } // нахождение ошибок слоя
        public static void backWards(double[,] li, double[,] w, double[,] lo, double k)
        {
            int wt = w.GetLength(0);
            int ht = w.GetLength(1);

            for (int y = 0; y < ht; y++)
            {
                for (int x = 0; x < wt; x++)
                {
                    w[x, y] = w[x, y] + k * lo[y, 1] * li[x, 0] * lo[y, 0] * (1 - lo[y, 0]);
                }
            }
        } // изменение весов
        public static void getTask(double[,] li, double[,] test_questionsdouble, double[,] test_answers, double[,] idl, int i)
        {
            for (int j = 0; j < test_questionsdouble.GetLength(1); j++)
            {
                li[j, 0] = test_questionsdouble[i, j];
            }

            for (int j = 0; j < test_answers.GetLength(1); j++)
            {
                idl[j, 0] = test_answers[i, j];
            }
        } // новый пример для нейросите
        public static double fixOutError(double[,] idl, double[,] n3)
        {
            double i = 0;
            double j = 0;
            int wt = idl.GetLength(0);
            for (int x = 0; x < wt; x++)
            {
                i = idl[x, 0] - n3[x, 0];
                j = j + Math.Abs(i);
                n3[x, 1] = i;
            }
            return j;
        } // нахождение отклонений

        static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        } // рандом для fillW
        public static void fillW(double[,] w)
        {
            int wt = w.GetLength(0);
            int ht = w.GetLength(1);

            for (int x = 0; x < wt; x++)
            {
                for (int y = 0; y < ht; y++)
                {
                    w[x, y] = GetRandomNumber(-0.5, 0.5);
                }
            }
        } // рандом весов
        public static void riad(double[,] test_questionsdouble, double[,] test_answers, int i)
        {
            Console.WriteLine("вопрос тест");
            for (int j = 1; j < test_questionsdouble.GetLength(1) + 1; j++)
            {
                if (j % 3 == 0)
                {
                    Console.WriteLine(test_questionsdouble[i, j - 1]);
                }
                else
                {
                    Console.Write(test_questionsdouble[i, j - 1]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("ответ тест");
            for (int j = 0; j < test_answers.GetLength(1); j++)
            {
                Console.Write(test_answers[i, j]);
            }
        } // вести новые значения
        public static void write(double[,] n3)
        {
            Console.WriteLine("ответ реал");
            for (int j = 0; j < n3.GetLength(0); j++)
            {
                Console.Write(n3[j, 0] + " ");
            }
            Console.WriteLine();
        } // вывод значения
        public static void writeW(double[,] w)
        {
            for (int i = 0; i < w.GetLength(0); i++)
            {
                for (int j = 0; j < w.GetLength(1); j++)
                {
                    Console.Write("{0,3} ", w[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        } // вывод весов
    }
}