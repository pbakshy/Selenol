// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes.ForCollections
{
    public class TestNameAttribute : BaseSelectorAttributeForCollectionTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.Name(selectorValue);
        }

        public class PageWithCollectionTypeProperties : SimplePageForTest
        {
            [Name(TestSelector)]
            public virtual IEnumerable<FormElement> Enumerable { get; set; }

            [Name(TestSelector)]
            public virtual ICollection<FormElement> Collection { get; set; }

            [Name(TestSelector)]
            public virtual IList<FormElement> List { get; set; }

            [Name(TestSelector)]
            public virtual ReadOnlyCollection<FormElement> ReadOnlyCollection { get; set; }
        }

        public class PageWithIncorrectPropertyCollectionTypes : SimplePageForTest
        {
            [Name(TestSelector)]
            public virtual ButtonElement[] Array { get; set; }

            [Name(TestSelector)]
            public virtual List<ButtonElement> List { get; set; }

            [Name(TestSelector)]
            public virtual IEnumerable Enumerable { get; set; }

            [Name(TestSelector)]
            public virtual IEnumerable<BaseHtmlElement> AbstractCollection { get; set; }
        }
    }
}