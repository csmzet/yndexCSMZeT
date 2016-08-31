using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace yndexCSMZeT
{
    public partial class Form1 : Form
    {
        public string email, password, searchWord;
        public int pageNumbers;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            email = textBoxEmail.Text;
            password = textBoxPassword.Text;
            searchWord = textBoxSearchWord.Text;
            pageNumbers = int.Parse(textBoxPageNumbers.Text);

            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("https://wordstat.yandex.ru/");
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(50));
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(500));
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(500));

            driver.Manage().Window.Maximize();
            YandexBase YandexWork = new YandexBase(driver);

            YandexWork.SearchWord(searchWord);
            YandexWork.LoginYandex(email, password);
            YandexWork.saveWords(pageNumbers);

            driver.Quit();
        }
    }
}
