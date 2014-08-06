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

            //TODO: move default timeout to config
            Wait.For(() => webDriver.Url.StartsWith(url), TimeSpan.FromSeconds(5), "browser url '{0}'".F(url));

            var jsExecutor = webDriver as IJavaScriptExecutor;
            if (jsExecutor == null)
            {
                throw new ArgumentException(
                    "The implementation of IWebDriver '{0}' does not implements IJavascriptExecutor. Page object can be created from WebDriver which implements IWebDriver and IJavascriptExecutor."
                        .F(webDriver.GetType().Name));
            }

            return ContainerFactory.Page<TPage>(webDriver, jsExecutor);
        }

        /// <summary>Creates new <see cref="NavigationContext{TCurrentPage}"/>.</summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="navigationAction">The navigation action.</param>
        /// <typeparam name="TCurrentPage">The current page type.</typeparam>
        /// <returns>An instance of the new navigation context.</returns>
        public static NavigationContext<TCurrentPage> Go<TCurrentPage>(this TCurrentPage currentPage, Action<TCurrentPage> navigationAction)
            where TCurrentPage : BasePage, new()
        {
            return new NavigationContext<TCurrentPage>(currentPage, navigationAction);
        }
    }
}