// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using OpenQA.Selenium;
using Selenol.Page;
using Selenol.Utils;

namespace Selenol.Extensions 
{
    /// <summary>
    /// The extension methods related to browser navigation.
    /// </summary>
    public static class NavigationExtensions
    {
        /// <summary>
        /// Navigates to specified url and try bind web page to Page object.
        /// </summary>
        /// <typeparam name="TPage">The page object type.</typeparam>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="url">The url.</param>
        /// <returns>Created page object.</returns>
        public static TPage GoTo<TPage>(this IWebDriver webDriver, string url) where TPage : BasePage, new()
        {
            webDriver.Navigate().GoToUrl(url);
            //TODO move default timeout to config
            Wait.For(() => webDriver.Url.StartsWith(url), TimeSpan.FromSeconds(5), "browser url '{0}'".F(url));

            var jsExecutor = webDriver as IJavaScriptExecutor;
            if (jsExecutor == null)
            {
                throw new ArgumentException(
                    "The implementation of IWebDriver '{0}' does not implements IJavascriptExecutor. Page object can be created from WebDriver which implements IWebDriver and IJavascriptExecutor."
                        .F(webDriver.GetType().Name));
            }

            return ContainerFactory.Create<TPage>(webDriver, jsExecutor);
        }

        /// <summary>Navigates to the new page after some action on the current page will be performed.</summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="navigationAction">The navigation action.</param>
        /// <typeparam name="TCurrentPage">The current page type.</typeparam>
        /// <typeparam name="TNewPage">The type of the new page, where should be navigated.</typeparam>
        /// <returns>An instance of the new page type.</returns>
        public static TNewPage GoTo<TCurrentPage, TNewPage>(this TCurrentPage currentPage, Action<TCurrentPage> navigationAction)
            where TCurrentPage : BasePage, new()
            where TNewPage : BasePage, new()
        {
            var webDriver = currentPage.WebDriver;
            var newPageType = typeof(TNewPage);

            navigationAction(currentPage);
            //TODO move default timeout to config
            Wait.For(() => PageUtil.IsValid(newPageType, webDriver.Url), TimeSpan.FromSeconds(5), "url matched '{0}' page.".F(newPageType.Name));

            return ContainerFactory.Create<TNewPage>(currentPage.WebDriver, currentPage.JavaScriptExecutor);
        }
    }
}