using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Translate_NoApi
{
    public class Parser
    {
        public string ParsJson(string content)
        {
            dynamic json = JsonConvert.DeserializeObject(content);
            return json[0][0][0].ToString();
        }

    }
}
