using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace yndexCSMZeT
{
    class YandexBase
    {
        public IWebDriver driver { get; set; }

        public YandexBase(IWebDriver Driver)
        {
            driver = Driver;
        }
        /// <summary>
        /// Функция ожидания появления элемента на странице:
        /// </summary>
        /// <param name="iClassName">Поиск по ...</param>
        public void WaitShowElement(By iClassName)
        {
            WebDriverWait iWait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            iWait.Until(ExpectedConditions.ElementIsVisible(iClassName));
        }

        /// <summary>
        /// Функция ожидания скрытия элемента на странице:
        /// </summary>
        /// <param name="iClassName">Поиск по ...</param>
        public void WaitHideElement(By iClassName)
        {
            WebDriverWait iWait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            iWait.Until(ExpectedConditions.InvisibilityOfElementLocated(iClassName));
            // iClassName: By.Id("id"), By.CssSelector("selector") и т.д.
        }

        /// <summary>
        /// Yandex login
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        public void LoginYandex( string email, string password)
        {
            IWebElement emailHtml = driver.FindElement(By.Id("b-domik_popup-username"));
            //<input id="b-domik_popup-username" class="b-form-input__input" name="login">
            IWebElement passwordHtml = driver.FindElement(By.Id("b-domik_popup-password"));
            //<input id="b-domik_popup-password" class="b-form-input__input" type="password" name="passwd">
            emailHtml.SendKeys(email);
            passwordHtml.SendKeys(password);
            passwordHtml.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Поиск элемента слова
        /// </summary>
        /// <param name="searchWord">Ключевое слово</param>
        public void SearchWord(string searchWord)
        {
            //Поиск элемента
            IWebElement searchTextBox = driver.FindElement(By.ClassName("b-form-input__input"));
            searchTextBox.SendKeys(searchWord);
            searchTextBox.SendKeys(OpenQA.Selenium.Keys.Enter);
        }

        public void saveWords(int numberPages)
        {
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            StreamWriter textFile = new StreamWriter("YandexSave.txt");
            for (int z = 1; z <= numberPages; z++)
            {
                bool pageLoading = false;
                while (!pageLoading)
                {
                    //Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver Web) =>
                    //{
                    //    Console.WriteLine(Web.FindElement(By.ClassName("b-pager__current")).GetAttribute("innerHTML"));
                    //    return true;
                    //});
                    //wait.Until(waitForElement);

                    //WebDriverWait iWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                    IWebElement numberPageCheck = driver.FindElement(By.ClassName("b-pager__current"));
                    if (int.Parse(numberPageCheck.Text) == z)
                    {
                        pageLoading = true;
                    }

                    //Thread.Sleep(500);


                    //************************************************* Рабочий код **************************************************************
                    //IWebElement numberPageCheck = driver.FindElement(By.ClassName("b-pager__current"));
                    //if (int.Parse(numberPageCheck.Text) == z)
                    //{
                    //    pageLoading = true;
                    //}
                    //Thread.Sleep(500);
                    //************************************************* Рабочий код **************************************************************


                    //IWebElement pageLoadCheck = driver.FindElement(By.ClassName("b-copyright__link"));
                    //<a class="b-copyright__link" href="http://www.yandex.ru">Яндекс</a>

                    // Не помогло
                    //if (pageLoadCheck.Text=="Яндекс")
                    //{
                    //    IWebElement numberPageCheck = driver.FindElement(By.ClassName("b-pager__current"));
                    //    if (int.Parse(numberPageCheck.Text) == z)
                    //    {
                    //        pageLoading = true;
                    //    }
                    //}
                }

                List<IWebElement> keyWords = driver.FindElements(By.ClassName("b-word-statistics__td-phrase")).ToList();
                List<IWebElement> keyNumber = driver.FindElements(By.ClassName("b-word-statistics__td-number")).ToList();
                for (int i = 0; i < keyWords.Count; i++)
                {
                    textFile.WriteLine(keyWords[i].Text + "\t" + keyNumber[i].Text);
                    //Element not found in the cache - perhaps the page has changed since it was looked up
                }
                if (z < numberPages)
                {
                    IWebElement nextPage = driver.FindElement(By.ClassName("b-pager__next"));
                    nextPage.Click();
                    //Thread.Sleep(250);
                }
            }
            textFile.Close();
        }
    }
}
