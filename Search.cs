using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Search_NoApi
{
    public class SearchDuckduckgo
    {
        IWebDriver driver = new ChromeDriver();
        private IWebElement element;
        ~SearchDuckduckgo()
        {
            driver.Quit();
        } 
        private void Start()
        {
            driver.Navigate().GoToUrl(Constants.ConstantsDuckduckgo.UrlDuckduckgo);
            element = driver.FindElement(By.XPath(Constants.ConstantsDuckduckgo.ClickDuckduckgo));
        }
        private void SearchClick()
        {
            element = driver.FindElement(By.XPath(Constants.ConstantsDuckduckgo.SearchDuckduckgo));
            element.Click();
        }

        /// <summary>
        /// Получает ссылку при поиска
        /// </summary>
        /// <param name="search">Запрашиваемая промнт</param>
        /// <returns>url</returns>
        public string SearchURLDuckduckgo(string search)
        {
            Start();
            element.SendKeys(search);
            SearchClick();
            driver.Quit();
            return driver.Url;
        }

        /// <summary>
        /// Получает первые N ссылок поиска
        /// </summary>
        /// <param name="search">Запрашиваемая промнт</param>
        /// <param name="LinksSIZE">Колличестов ссылок</param>
        /// <returns>url-ы</returns>
        public string[] SearchAllDuckduckgo(string search, int LinksSIZE)
        {
            Start();
            element.SendKeys(search);
            SearchClick();

            string[] links = new string[LinksSIZE];
            for (int i = 0; i < LinksSIZE; i++)
            {
                element = driver.FindElement(By.XPath($"//*[@id=\"r1-{i}\"]/div[2]/h2/a"));
                links[i] = element.GetAttribute("href");
            }

            driver.Quit();
            return links;
        }
    }
}
