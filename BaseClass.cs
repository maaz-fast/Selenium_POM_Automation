using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace Selenium_POM_Automation
{
    public class BaseClass
    {
        public static ChromeDriver chromeDriver;

        public void DriverConfiguration()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");

            chromeDriver = new ChromeDriver(options);
        }

        public void OpenBrowserAndURL()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.json");

            if (!File.Exists(filePath))
            {
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
                filePath = Path.Combine(projectRoot, "Data.json");

                if (!File.Exists(filePath))
                {
                    projectRoot = Path.GetFullPath(Path.Combine(projectRoot, "..\\"));
                    filePath = Path.Combine(projectRoot, "Data.json");
                }
            }

            if (File.Exists(filePath))
            {
                var jsonData = JObject.Parse(File.ReadAllText(filePath));
                var baseUrl = jsonData["baseUrl"].ToString();

                chromeDriver.Manage().Window.Maximize();
                chromeDriver.Navigate().GoToUrl(baseUrl);
            }
            else
            {
                throw new FileNotFoundException($"Data.json file not found! Please check the file at this location: {filePath}");
            }
        }

        public void CloseBrowser()
        {
            if (chromeDriver != null)
            {
                chromeDriver.Quit();
            }
        }
    }
}