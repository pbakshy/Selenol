// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Linq;

using OpenQA.Selenium;

namespace Selenol.Elements
{
    /// <summary>The select html element.</summary>
    public class SelectElement : BaseSelectElement
    {
        /// <summary>Initializes a new instance of the <see cref="SelectElement"/> class.</summary>
        /// <param name="webElement">The web element.</param>
        public SelectElement(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>Gets the selected option.</summary>
        public OptionElement SelectedOption
        {
            get
            {
                var options = this.Options.ToArray();
                return options.FirstOrDefault(x => x.IsSelected) ?? options.FirstOrDefault();
            }
        }
    }
}