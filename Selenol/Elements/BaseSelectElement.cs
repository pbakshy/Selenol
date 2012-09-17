// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using OpenQA.Selenium;

using Selenol.Extensions;

namespace Selenol.Elements
{
    /// <summary>The base select element.</summary>
    public abstract class BaseSelectElement : BaseHtmlElement
    {
        /// <summary>Initializes a new instance of the <see cref="BaseSelectElement"/> class. Initializes a new instance of the <see cref="SelectElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        protected BaseSelectElement(IWebElement webElement)
            : base(webElement, x => x.TagName == HtmlElements.Select)
        {
        }

        /// <summary>Gets the options.</summary>
        public IEnumerable<OptionElement> Options
        {
            get
            {
                return this.WebElement.FindElements(By.TagName(HtmlElements.Option)).Select(x => new OptionElement(x)).ToArray();
            }
        }

        /// <summary>Select option by text.</summary>
        /// <param name="text">The text. </param>
        public void SelectOptionByText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            this.SelectBy(x => Equals(x.Text, text), "text '{0}'".F(text));
            this.Options.First().Select();
        }

        /// <summary>Select option by value.</summary>
        /// <param name="value">The value. </param>
        public void SelectOptionByValue(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.SelectBy(x => Equals(x.Value, value), "value '{0}'".F(value));
        }

        private void SelectBy(Func<OptionElement, bool> predicate, string description)
        {
            var option = this.Options.FirstOrDefault(predicate);
            if (option == null)
            {
                throw new ElementNotFoundException("Cannot find option with {0}.".F(description));
            }

            option.Select();
        }
    }
}