using NearestIMG.Tupe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NearestIMG.Single
{
    public static class PictureSerializer
    {
        // Сохранение списка объектов Picture в JSON файл
        public static void SaveToFile(List<Picture> pictures, string filePath)
        {
            // Сериализуем только векторы и информацию, которая может быть восстановлена
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(pictures, options);
            File.WriteAllText(filePath, jsonString);
        }

        // Чтение списка объектов Picture из JSON файла
        public static List<Picture> LoadFromFile(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Picture>>(jsonString);
        }
    }
}
