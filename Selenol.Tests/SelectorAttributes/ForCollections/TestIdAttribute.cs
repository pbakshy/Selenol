// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes.ForCollections
{
    public class TestIdAttribute : BaseSelectorAttributeForCollectionTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.Id(selectorValue);
        }

        public class PageWithCollectionTypeProperties : SimplePageForTest
        {
            [Id(TestSelector)]
            public virtual IEnumerable<FormElement> Enumerable { get; set; }

            [Id(TestSelector)]
            public virtual ICollection<FormElement> Collection { get; set; }

            [Id(TestSelector)]
            public virtual IList<FormElement> List { get; set; }

            [Id(TestSelector)]
            public virtual ReadOnlyCollection<FormElement> ReadOnlyCollection { get; set; }

            [Id(TestSelector, CacheValue = true)]
            public virtual IEnumerable<LinkElement> Links { get; set; }
        }

        public class PageWithIncorrectPropertyCollectionTypes : SimplePageForTest
        {
            [Id(TestSelector)]
            public virtual ButtonElement[] Array { get; set; }

            [Id(TestSelector)]
            public virtual List<ButtonElement> List { get; set; }

            [Id(TestSelector)]
            public virtual IEnumerable Enumerable { get; set; }

            [Id(TestSelector)]
            public virtual IEnumerable<BaseHtmlElement> AbstractCollection { get; set; }
        }

        public class PageWithWritableProperty : BasePageWithWritableProperty
        {
            [Id(TestSelector)]
            public override IEnumerable<RadioButtonElement> RadioButtons { get; set; }
        }
    }
}