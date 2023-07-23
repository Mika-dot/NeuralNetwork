using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Xml.Linq;
using System.IO;

namespace Search_NoApi
{
    public class Gpt
    {



        private IWebDriver driver;
        private IWebElement element;
        private WebDriverWait wait;
        private int answer = 0;

        public Gpt(string model = "1", string jailbreak = "1", bool online = false)
        {

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(Constants.ConstantsGpt.UrlGpt);

            Model(model);
            Jailbreak(jailbreak);
            Online(online);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        public void GptQuit()
        {
            driver.Quit();
        }

        public void Model(string model)
        {
            // Номер модели
            switch (model)
            {
                case "1":
                    element = driver.FindElement(By.XPath("//*[@id=\"model\"]/option[1]"));
                    element.Click();
                    break;
                case "2":
                    element = driver.FindElement(By.XPath("//*[@id=\"model\"]/option[2]"));
                    element.Click();
                    break;
                case "3":
                    element = driver.FindElement(By.XPath("//*[@id=\"model\"]/option[3]"));
                    element.Click();
                    break;
                case "4":
                    element = driver.FindElement(By.XPath("//*[@id=\"model\"]/option[4]"));
                    element.Click();
                    break;
                default:
                    element = driver.FindElement(By.XPath("//*[@id=\"model\"]/option[1]"));
                    element.Click();
                    break;
            }
        }
        public void Jailbreak(string jailbreak)
        {
            // Тип модели
            switch (jailbreak)
            {
                case "1":
                    element = driver.FindElement(By.XPath("//*[@id=\"jailbreak\"]/option[1]"));
                    element.Click();
                    break;
                case "2":
                    element = driver.FindElement(By.XPath("//*[@id=\"jailbreak\"]/option[2]"));
                    element.Click();
                    break;
                case "3":
                    element = driver.FindElement(By.XPath("//*[@id=\"jailbreak\"]/option[3]"));
                    element.Click();
                    break;
                default:
                    element = driver.FindElement(By.XPath("//*[@id=\"jailbreak\"]/option[1]"));
                    element.Click();
                    break;
            }
        }
        public void Online(bool online)
        {
            //Онлайн
            if (online)
            {            
                element = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[4]/div/div[2]/label"));
                element.Click();
            }
        }

        public (Bitmap?, string?) Request(string search)
        {
            answer += 2;

            element = driver.FindElement(By.XPath(Constants.ConstantsGpt.Search));
            element.SendKeys(search);
            element = driver.FindElement(By.XPath(Constants.ConstantsGpt.Click));
            element.Click();
            wait.Until(d => d.FindElements(By.XPath(Constants.ConstantsGpt.PresenceFlag)).FirstOrDefault());
            wait.Until(driver => driver.FindElements(By.XPath(Constants.ConstantsGpt.PresenceFlag)).Count == 0);
            Thread.Sleep(Constants.ConstantsGpt.Time);

            //// Поиск элемента с использованием XPath
            var elements = driver.FindElements(By.XPath($"/html/body/div[1]/div[2]/div[2]/div[{answer}]/div[2]/p[1]/img"));

            Bitmap croppedImage = null;

            if (elements.Count > 0)
            {
                Thread.Sleep(100);

                //// Получение скриншота всей страницы
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                //// Преобразование скриншота в изображение
                Bitmap image = (Bitmap)System.Drawing.Image.FromStream(new MemoryStream(screenshot.AsByteArray));

                //// Вырезание области, соответствующей элементу
                Rectangle elementRect = new Rectangle(element.Location, element.Size);
                croppedImage = image.Clone(elementRect, image.PixelFormat);

            }
            // /html/body/div[1]/div[2]/div[2]/div[2]/div[2]

            string text = null;
            var elementstext = driver.FindElement(By.XPath($"/html/body/div[1]/div[2]/div[2]/div[{answer}]/div[2]"));  
            if (elementstext.Text != null)
            {
                text = driver.FindElement(By.XPath($"/html/body/div[1]/div[2]/div[2]/div[{answer}]/div[2]")).Text;
            }

            return (croppedImage, text);
        }
    }
}
