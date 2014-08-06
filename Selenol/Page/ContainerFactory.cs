// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using Castle.DynamicProxy;
using OpenQA.Selenium;
using Selenol.Controls;
using Selenol.Extensions;
using Selenol.SelectorAttributes;
using Selenol.SelectorAttributes.Interceptors;

namespace Selenol.Page
{
    /// <summary>The page factory.</summary>
    public class ContainerFactory
    {
        private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        private static readonly ProxyGenerationOptions proxyGenerationOptions = new ProxyGenerationOptions(new PageProxyGenerationHook())
            {
                Selector = new InterceptorSelector()
            };

        /// <summary>Creates user defined page.</summary>
        /// <param name="webDriver">The web driver. </param>
        /// <param name="javaScriptExecutor">The js executor. </param>
        /// <typeparam name="TPage">The page type. </typeparam>
        /// <returns>The new instance of user defined page. </returns>
        public static TPage Page<TPage>(IWebDriver webDriver, IJavaScriptExecutor javaScriptExecutor) where TPage : BasePage, new()
        {
            var page = proxyGenerator.CreateClassProxy<TPage>(proxyGenerationOptions,
                new PropertyInterceptor(),
                new CollectionPropertyInterceptor(),
                new InvalidWriteOperationInterceptor());
            page.Initialize(webDriver, javaScriptExecutor);
            return page;
        }

        /// <summary>Create the user defined Control based from IWebElement.</summary>
        /// <param name="webElement">The web element.</param>
        /// <typeparam name="TControl">The type of user defined control.</typeparam>
        /// <returns>The user defined control.</returns>
        public static TControl Control<TControl>(IWebElement webElement) where TControl : Control
        {
            if (webElement == null)
            {
                throw new ArgumentNullException("webElement");
            }

            var controlType = typeof(TControl);
            if (controlType.IsAbstract)
            {
                throw new ArgumentException("Unable to create the Control instance of abstract class \"{0}\".".F(controlType.FullName));
            }

            return (TControl)proxyGenerator.CreateClassProxy(controlType,
                                                             proxyGenerationOptions,
                                                             new object[] { webElement },
                                                             new PropertyInterceptor(),
                                                             new CollectionPropertyInterceptor(),
                                                             new InvalidWriteOperationInterceptor());
        }
    }
}