using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translate_NoApi.Constants
{
    /// <summary>
    /// Константы для запроса в гугл переводчик
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Яык куда переводят
        /// </summary>
        public static string ISOlanguage { get; set; } = "en";
        public static string Url { get; set; } = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=";
        public static string UrlPlus { get; set; } = "&dt=t&q=";
    }
}
