// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using OpenQA.Selenium;

using Selenol.Validation.Element;

namespace Selenol.Elements
{
    /// <summary>The listbox html element.</summary>
    [Tag(HtmlElements.Select)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class ListboxElement : BaseSelectElement
    {
        /// <summary>Initializes a new instance of the <see cref="ListboxElement"/> class.</summary>
        /// <param name="webElement">The web element. </param>
        public ListboxElement(IWebElement webElement)
            : base(webElement)
        {
        }

        /// <summary>Gets the selected elements.</summary>
        public IEnumerable<OptionElement> SelectedOptions
        {
            get
            {
                return this.Options.Where(x => x.IsSelected).ToArray();
            }
        }
    }
}