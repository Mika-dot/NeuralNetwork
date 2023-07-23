using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_NoApi.Constants
{
    /// <summary>
    /// Константы для запроса в гугл
    /// </summary>
    public static class ConstantsDuckduckgo
    {
        public static string UrlDuckduckgo { get; set; } = "https://duckduckgo.com/";
        public static string ClickDuckduckgo { get; set; } = "//*[@id=\"search_form_input_homepage\"]";
        public static string SearchDuckduckgo { get; set; } = "//*[@id=\"search_button_homepage\"]";

    }
    public static class ConstantsGpt
    {
        public static string UrlGpt { get; set; } = "https://chat.ramxn.dev/chat/";
        public static string PresenceFlag { get; set; } = "//*[@id=\"cursor\"]";

        public static string Click { get; set; } = "//*[@id=\"send-button\"]/i";
        public static string Search { get; set; } = "//*[@id=\"message-input\"]";

        public static int Time { get; set; } = 500;
    }
}
