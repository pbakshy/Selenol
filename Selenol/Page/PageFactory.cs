// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

namespace Selenol.Page
{
    /// <summary>The page factory.</summary>
    public class PageFactory
    {
        /// <summary>The create.</summary>
        /// <param name="webDriver">The driver. </param>
        /// <param name="javaScriptExecutor">The js executor. </param>
        /// <typeparam name="TPage">The page type. </typeparam>
        /// <returns>The TPage. </returns>
        public static TPage Create<TPage>(IWebDriver webDriver, IJavaScriptExecutor javaScriptExecutor) where TPage : BasePage, new()
        {
            var page = new TPage();
            page.Initialize(webDriver, javaScriptExecutor);
            return page;
        }
    }
}