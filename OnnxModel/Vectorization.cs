using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace NearestIMG.OnnxModel
{
    public static class Vectorization
    {
        public static float[] ProcessImage(Bitmap image, InferenceSession session)
        {
            // Преобразование изображения в тензор
            var inputTensor = ImageToTensor(image);

            // Подготовка входных данных для модели
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("images", inputTensor)
            };

            // Запуск модели и получение результата
            using (var results = session.Run(inputs))
            {
                // Извлекаем результат из модели
                var output = results.First().AsTensor<float>();

                // Преобразуем тензор в одномерный массив float[]
                return output.ToArray();
            }
        }

        public static Tensor<float> ImageToTensor(Bitmap image)
        {
            // Приведение изображения к размеру 224x224
            var resizedImage = new Bitmap(image, new Size(224, 224));

            // Преобразование изображения в массив тензора [3, 224, 224]
            float[] imageData = new float[3 * 224 * 224];
            int index = 0;

            for (int y = 0; y < resizedImage.Height; y++)
            {
                for (int x = 0; x < resizedImage.Width; x++)
                {
                    // Получаем цвет пикселя
                    Color pixel = resizedImage.GetPixel(x, y);

                    // Нормализуем значения пикселей (например, между 0 и 1)
                    imageData[index] = pixel.R / 255.0f; // Red channel
                    imageData[index + 224 * 224] = pixel.G / 255.0f; // Green channel
                    imageData[index + 2 * 224 * 224] = pixel.B / 255.0f; // Blue channel

                    index++;
                }
            }

            // Создание тензора из массива данных
            var dimensions = new[] { 1, 3, 224, 224 }; // Batch size = 1, Channels = 3 (RGB), Height = 224, Width = 224
            return new DenseTensor<float>(imageData, dimensions);
        }
    }
}
