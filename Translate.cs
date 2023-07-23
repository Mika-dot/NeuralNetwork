using System.Security.AccessControl;

namespace Translate_NoApi
{
    public class Translate
    {
        public async Task<string> AutoTranslationAsync(string text,
                                      string ISOlanguage = "en")
        {
            return new Parser().ParsJson(await new AccessURL().GetTextAsync(text));
        }
    }
}