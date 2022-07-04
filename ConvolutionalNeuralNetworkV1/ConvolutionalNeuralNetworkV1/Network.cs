using System;
using System.Drawing;

namespace ConvolutionalNeuralNetworkV1
{
    public class Network
    {
        public int[,] ImageMatrix(string link)
        {

            Bitmap Bmp = new Bitmap(link);

            int[,] Matrix = new int[Bmp.Width, Bmp.Height];

            for (int i = 0; i < Bmp.Width; i++)
            {
                for (int j = 0; j < Bmp.Height; j++)
                {
                    Color s = Bmp.GetPixel(i, j);
                    Matrix[i, j] = Convert.ToInt32((s.R + s.G + s.B) / 3);
                }
            }

            return Matrix;
        }
        public int[,] NeuronConvolution(int[,] MatrixFirst, int[] decrease, int[,] Filters) // decrease сжатие , тип // Filters матрица
        {
            int[,] Decrease = MaxPooling(MatrixFirst, decrease[0], decrease[1]);
            int[,] Pooling = InitializationMatrix(Decrease, Filters);

            return Pooling;
        }

        // ---------------------------------------------- 
        private static int[,] MultiplicationMatrix(int[,] firstMatrix, int[,] secondMatrix)
        {
            int[,] resultMatrix = new int[firstMatrix.GetLength(0), firstMatrix.GetLength(0)];
            for (int i = 0; i < firstMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < secondMatrix.GetLength(1); j++)
                {
                    for (int k = 0; k < secondMatrix.GetLength(0); k++)
                    {
                        resultMatrix[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            }
            return resultMatrix;
        }
        private static int[,] MaxPooling(int[,] MatrixFirst, int reduction, int parameter)
        {
            int[,] Matrix = new int[MatrixFirst.GetLength(0) / reduction, MatrixFirst.GetLength(1) / reduction];

            for (int i = 0; i < MatrixFirst.GetLength(0) - reduction; i += reduction)
            {
                for (int j = 0; j < MatrixFirst.GetLength(1) - reduction; j += reduction)
                {
                    int ii = i / reduction;
                    int jj = j / reduction;

                    int meaning = 0;
                    int temporary_1 = 0;
                    int temporary_2 = 0;

                    for (int o = i; o < reduction + i; o++)
                    {
                        for (int k = j; k < reduction + j; k++)
                        {
                            switch (parameter)
                            {
                                case 0:
                                    if (temporary_1 < MatrixFirst[o, k])
                                    {
                                        meaning = MatrixFirst[o, k];
                                        temporary_1 = MatrixFirst[o, k];
                                    }
                                    break;
                                case 1:
                                    temporary_1 += MatrixFirst[o, k];
                                    temporary_2++;
                                    break;
                                case 2:
                                    meaning += MatrixFirst[o, k];
                                    break;
                                default:
                                    meaning = MatrixFirst[o, k];
                                    break;
                            }
                        }
                    }

                    if (parameter == 1)
                    {
                        meaning = Convert.ToInt32(temporary_1 / temporary_2);
                    }

                    Matrix[ii, jj] = meaning;
                }
            }
            return Matrix;
        }
        private static int[,] InitializationMatrix(int[,] MatrixFirst, int[,] weights)
        {
            int[,] matrix = new int[MatrixFirst.GetLength(0), MatrixFirst.GetLength(1)];
            for (int i = 0; i < MatrixFirst.GetLength(0) - weights.GetLength(0); i += weights.GetLength(0))
            {
                for (int j = 0; j < MatrixFirst.GetLength(1) - weights.GetLength(0); j += weights.GetLength(0))
                {
                    int[,] firstMatrix = new int[weights.GetLength(0), weights.GetLength(0)];
                    for (int o = i, oo = 0; o < weights.GetLength(0) + i; o++, oo++)
                    {
                        for (int k = j, kk = 0; k < weights.GetLength(0) + j; k++, kk++)
                        {
                            firstMatrix[oo, kk] = MatrixFirst[o, k];
                        }
                    }
                    int[,] secondMatrix = MultiplicationMatrix(firstMatrix, weights);
                    for (int o = i, oo = 0; o < weights.GetLength(0) + i; o++, oo++)
                    {
                        for (int k = j, kk = 0; k < weights.GetLength(0) + j; k++, kk++)
                        {
                            matrix[o, k] = secondMatrix[oo, kk];
                        }
                    }
                }
            }
            return matrix;
        }
    }
}