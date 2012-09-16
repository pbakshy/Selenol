// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The form element.</summary>
    public class FormElement : ContainerElement
    {
        /// <summary>Initializes a new instance of the <see cref="FormElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public FormElement(IWebElement webElement)
            : base(webElement, x => x.TagName == HtmlElements.Form)
        {
        }

        /// <summary>Submits the form.</summary>
        public void Submit()
        {
            this.WebElement.Submit();
        }
    }
}