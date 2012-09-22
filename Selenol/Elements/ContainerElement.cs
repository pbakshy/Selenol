// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;

using OpenQA.Selenium;

using Selenol.Validation.Element;

namespace Selenol.Elements
{
    /// <summary>The generic container element.</summary>
    [NotInput]
    public class ContainerElement : BaseHtmlElement, ISearchContext
    {
        /// <summary>Initializes a new instance of the <see cref="ContainerElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public ContainerElement(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>Gets the element text.</summary>
        public string Text
        {
            get
            {
                return this.WebElement.Text;
            }
        }

        /// <summary>
        /// Finds the first <see cref="T:OpenQA.Selenium.IWebElement"/> using the given method. 
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>
        /// The first matching <see cref="T:OpenQA.Selenium.IWebElement"/> on the current context.
        /// </returns>
        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public IWebElement FindElement(By by)
        {
            return this.WebElement.FindElement(by);
        }

        /// <summary>
        /// Finds all <see cref="T:OpenQA.Selenium.IWebElement">IWebElements</see> within the current context 
        ///             using the given mechanism.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>
        /// A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"/> of all <see cref="T:OpenQA.Selenium.IWebElement">WebElements</see>
        ///             matching the current criteria, or an empty list if nothing matches.
        /// </returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return this.WebElement.FindElements(by);
        }
    }
}