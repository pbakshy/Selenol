// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes.ForCollections
{
    [TestFixture]
    public class TestClassAttribute : BaseSelectorAttributeForCollectionTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.ClassName(selectorValue);
        }

        public class PageWithCollectionTypeProperties : SimplePageForTest
        {
            [Class(TestSelector)]
            public virtual IEnumerable<FormElement> Enumerable { get; set; }

            [Class(TestSelector)]
            public virtual ICollection<FormElement> Collection { get; set; }

            [Class(TestSelector)]
            public virtual IList<FormElement> List { get; set; }

            [Class(TestSelector)]
            public virtual ReadOnlyCollection<FormElement> ReadOnlyCollection { get; set; }

            [Class(TestSelector, CacheValue = true)]
            public virtual IEnumerable<LinkElement> Links { get; set; }
        }

        public class PageWithIncorrectPropertyCollectionTypes : SimplePageForTest
        {
            [Class(TestSelector)]
            public virtual ButtonElement[] Array { get; set; }

            [Class(TestSelector)]
            public virtual List<ButtonElement> List { get; set; }

            [Class(TestSelector)]
            public virtual IEnumerable Enumerable { get; set; }

            [Class(TestSelector)]
            public virtual IEnumerable<BaseHtmlElement> AbstractCollection { get; set; }
        }

        public class PageWithWritableProperty : BasePageWithWritableProperty
        {
            [Class(TestSelector)]
            public override IEnumerable<RadioButtonElement> RadioButtons { get; set; }
        }
    }
}