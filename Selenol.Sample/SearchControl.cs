// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;
using Selenol.Controls;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Sample
{
    public class SearchFormControl : Control
    {
        public SearchFormControl(IWebElement webElement)
            : base(webElement)
        {
        }

        [Class("search-page-input")]
        public virtual TextboxElement Text { get; set; }

        [TagName("button")]
        public virtual ButtonElement SearchButton { get; set; }

        public void Search(string text)
        {
            this.Text.Clear();
            this.Text.TypeText("selenol");
            this.SearchButton.Click();
        }
    }
}