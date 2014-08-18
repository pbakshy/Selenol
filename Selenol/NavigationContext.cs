// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using Selenol.Extensions;
using Selenol.Page;
using Selenol.Utils;

namespace Selenol
{
    /// <summary>The navigation context.</summary>
    /// <typeparam name="TCurrentPage">The current page type.</typeparam>
    public class NavigationContext<TCurrentPage> where TCurrentPage : BasePage
    {
        private readonly TCurrentPage currentPage;
        private readonly Action<TCurrentPage> navigationAction;

        /// <summary>Initializes a new instance of the <see cref="NavigationContext{TCurrentPage}"/> class.</summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="navigationAction">The navigation action.</param>
        public NavigationContext(TCurrentPage currentPage, Action<TCurrentPage> navigationAction)
        {
            if (currentPage == null)
            {
                throw new ArgumentNullException("currentPage");
            }

            if (navigationAction == null)
            {
                throw new ArgumentNullException("navigationAction");
            }

            this.currentPage = currentPage;
            this.navigationAction = navigationAction;
        }

        /// <summary>Navigates to the new page after navigation action on the current page will be performed.</summary>
        /// <typeparam name="TNewPage">The new page type.</typeparam>
        /// <returns>An instance of the new page type.</returns>
        public TNewPage To<TNewPage>() where TNewPage : BasePage, new()
        {
            var webDriver = this.currentPage.WebDriver;
            var newPageType = typeof(TNewPage);
            var beforeActionUrl = webDriver.Url;

            this.navigationAction(this.currentPage);
            
            //TODO: move default timeout to config
            var timeout = TimeSpan.FromSeconds(5);
            var isWaitSucceed = TryWait.For(() => PageUtil.IsValid(newPageType, webDriver.Url), timeout);
            if (!isWaitSucceed && webDriver.Url.Equals(beforeActionUrl))
            {
                throw new TimeoutException("Timed out after {0}. Waited for url matched '{1}' page.".F(timeout, newPageType.Name));
            }

            return ContainerFactory.Page<TNewPage>(this.currentPage.WebDriver, this.currentPage.JavaScriptExecutor);
        }
    }
}