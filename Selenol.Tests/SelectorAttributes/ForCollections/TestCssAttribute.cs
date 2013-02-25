// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes.ForCollections
{
    public class TestCssAttribute : BaseSelectorAttributeForCollectionTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.CssSelector(selectorValue);
        }

        public class PageWithCollectionTypeProperties : SimplePageForTest
        {
            [Css(TestSelector)]
            public virtual IEnumerable<FormElement> Enumerable { get; set; }

            [Css(TestSelector)]
            public virtual ICollection<FormElement> Collection { get; set; }

            [Css(TestSelector)]
            public virtual IList<FormElement> List { get; set; }

            [Css(TestSelector)]
            public virtual ReadOnlyCollection<FormElement> ReadOnlyCollection { get; set; }

            [Css(TestSelector, CacheValue = true)]
            public virtual IEnumerable<LinkElement> Links { get; set; }
        }

        public class PageWithIncorrectPropertyCollectionTypes : SimplePageForTest
        {
            [Css(TestSelector)]
            public virtual ButtonElement[] Array { get; set; }

            [Css(TestSelector)]
            public virtual List<ButtonElement> List { get; set; }

            [Css(TestSelector)]
            public virtual IEnumerable Enumerable { get; set; }

            [Css(TestSelector)]
            public virtual IEnumerable<BaseHtmlElement> AbstractCollection { get; set; }
        }

        public class PageWithWritableProperty : BasePageWithWritableProperty
        {
            [Css(TestSelector)]
            public override IEnumerable<RadioButtonElement> RadioButtons { get; set; }
        }
    }
}