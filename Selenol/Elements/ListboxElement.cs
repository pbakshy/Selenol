// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using OpenQA.Selenium;

using Selenol.Extensions;
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

        /// <summary>Deselects an option by text.</summary>
        /// <param name="text">The text.</param>
        public void DeselectOptionByText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            this.DeselectBy(x => string.Equals(x.Text, text), "text '{0}'".F(text));
        }

        /// <summary>Deselects an option by value.</summary>
        /// <param name="value">The value.</param>
        public void DeselectOptionByValue(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.DeselectBy(x => string.Equals(x.Value, value), "value '{0}'".F(value));
        }

        /// <summary>Clears selection inside the listbox.</summary>
        public void ClearSelection()
        {
            foreach (var option in this.SelectedOptions)
            {
                option.Deselect();
            }
        }

        private void DeselectBy(Func<OptionElement, bool> predicate, string description)
        {
            var option = this.Options.FirstOrDefault(predicate);
            if (option == null)
            {
                throw new ElementNotFoundException("Cannot find option with {0}.".F(description));
            }

            option.Deselect();
        }
    }
}