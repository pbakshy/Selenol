// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using Castle.DynamicProxy;
using OpenQA.Selenium;
using Selenol.SelectorAttributes;

namespace Selenol.Page
{
    /// <summary>The page factory.</summary>
    public class PageFactory
    {
        private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        private static readonly ProxyGenerationOptions proxyGenerationOptions = new ProxyGenerationOptions(new PageProxyGenerationHook())
            {
                Selector = new InterceptorSelector()
            };

        /// <summary>The create.</summary>
        /// <param name="webDriver">The driver. </param>
        /// <param name="javaScriptExecutor">The js executor. </param>
        /// <typeparam name="TPage">The page type. </typeparam>
        /// <returns>The TPage. </returns>
        public static TPage Create<TPage>(IWebDriver webDriver, IJavaScriptExecutor javaScriptExecutor) where TPage : BasePage, new()
        {
            var page = proxyGenerator.CreateClassProxy<TPage>(proxyGenerationOptions, new SelectorInterceptor(), new InvalidWriteOperationInterceptor());
            page.Initialize(webDriver, javaScriptExecutor);
            return page;
        }
    }
}