using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Translate_NoApi
{
    public class AccessURL
    {
        public async Task<string> GetTextAsync(string text)
        {
            // создаем клиент
            using var client = new HttpClient();

            // указываем URL, по которому выполняем запрос
            string url = Constants.Constants.Url + Constants.Constants.ISOlanguage + Constants.Constants.UrlPlus + Uri.EscapeDataString(text);

            HttpResponseMessage response;
            try
            {
                // отправляем GET-запрос и получаем ответ
                response = await client.GetAsync(url);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка в QuerySQL.QuestBySportDate, запрос по промежутку времени" + e.Message);
                throw;
            }

            // читаем содержимое ответа
            string content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
