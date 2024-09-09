using NearestIMG.Tupe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearestIMG
{
    public static class PictureComparer
    {
        public static float CosineSimilarity(float[] vectorA, float[] vectorB)
        {
            if (vectorA.Length != vectorB.Length)
                throw new ArgumentException("Vectors must be of the same length.");

            float dotProduct = 0;
            float magnitudeA = 0;
            float magnitudeB = 0;

            for (int i = 0; i < vectorA.Length; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                magnitudeA += vectorA[i] * vectorA[i];
                magnitudeB += vectorB[i] * vectorB[i];
            }

            magnitudeA = (float)Math.Sqrt(magnitudeA);
            magnitudeB = (float)Math.Sqrt(magnitudeB);

            if (magnitudeA == 0 || magnitudeB == 0)
                return 0; // Если один из векторов нулевой, сходство равно 0.

            return dotProduct / (magnitudeA * magnitudeB);
        }

        // Параллельный метод для нахождения ближайшего изображения
        public static (Picture closestPicture, float similarity) FindClosestMatchParallel(List<Picture> original, Picture fresh)
        {
            Picture closestPicture = null;
            float maxSimilarity = -1;

            object lockObject = new object();

            Parallel.ForEach(original, (picture) =>
            {
                float similarity = CosineSimilarity(picture.Vector, fresh.Vector);

                // Синхронизируем доступ к общим переменным
                lock (lockObject)
                {
                    if (similarity > maxSimilarity)
                    {
                        maxSimilarity = similarity;
                        closestPicture = picture;
                    }
                }
            });

            return (closestPicture, maxSimilarity);
        }

        // Метод для нахождения ближайших n векторов и среднего сходства до них
        public static (List<(Picture picture, float similarity)> closestPictures, float averageSimilarity)
        FindClosestMatches(List<Picture> original, Picture fresh, int n)
        {
            // Создаем список пар (Picture, сходство)
            List<(Picture picture, float similarity)> similarities = new List<(Picture picture, float similarity)>();

            foreach (var picture in original)
            {
                float similarity = CosineSimilarity(picture.Vector, fresh.Vector);
                similarities.Add((picture, similarity));
            }

            // Сортируем по убыванию сходства
            similarities = similarities.OrderByDescending(s => s.similarity).Take(n).ToList();

            // Вычисляем среднее значение сходства для ближайших n векторов
            float averageSimilarity = similarities.Average(s => s.similarity);

            return (similarities, averageSimilarity);
        }

        // Метод для нахождения класса, к которому принадлежит fresh
        public static (List<Picture> closestClass, float averageSimilarity)
        ClassifyPicture(List<List<Picture>> classes, Picture fresh)
        {
            List<Picture> bestClass = null;
            float maxAverageSimilarity = -1;

            foreach (var classGroup in classes)
            {
                // Вычисляем среднее сходство между fresh и всеми элементами класса
                float averageSimilarity = classGroup
                    .Average(picture => PictureComparer.CosineSimilarity(picture.Vector, fresh.Vector));

                if (averageSimilarity > maxAverageSimilarity)
                {
                    maxAverageSimilarity = averageSimilarity;
                    bestClass = classGroup;
                }
            }

            return (bestClass, maxAverageSimilarity);
        }
    }
}
