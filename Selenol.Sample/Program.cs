// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium.Firefox;
using Selenol.Extensions;

namespace Selenol.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var firefox = new FirefoxDriver())
            {
                var mainPage = firefox.GoTo<MainPage>("https://github.com");
                mainPage.SearchBox.TypeText("Seleno");

                var searchPage = mainPage.GoTo<MainPage, SearchPage>(x => x.SearchBox.SendEnter());
                searchPage.Results.ToList().ForEach(x => Console.WriteLine(x.Text));

                searchPage.SearchElement.Clear();
                searchPage.SearchElement.TypeText("selenol");
                searchPage = searchPage.GoTo<SearchPage, SearchPage>(x => x.SearchButton.Click());

                var repoPage = searchPage.GoTo<SearchPage, RepositoryPage>(x => x.Results.First().Click());

                Debug.Assert(string.Equals("pbakshy", repoPage.Author.Text), "Wrong author");
                Debug.Assert(string.Equals("Selenol", repoPage.RepositoryName.Text), "Wrong repo");

                if (Debugger.IsAttached)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}
