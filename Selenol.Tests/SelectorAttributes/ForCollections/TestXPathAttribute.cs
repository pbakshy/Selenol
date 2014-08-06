// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Selenol.Controls;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes.ForCollections
{
    public class TestXPathAttribute : BaseSelectorAttributeForCollectionTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.XPath(selectorValue);
        }

        public class PageWithCollectionTypeProperties : SimplePageForTest
        {
            [XPath(TestSelector)]
            public virtual IEnumerable<FormElement> Enumerable { get; set; }

            [XPath(TestSelector)]
            public virtual IEnumerable<FormControl> ControlEnumerable { get; set; }

            [XPath(TestSelector)]
            public virtual ICollection<FormElement> Collection { get; set; }

            [XPath(TestSelector)]
            public virtual ICollection<FormControl> ControlCollection { get; set; }

            [XPath(TestSelector)]
            public virtual IList<FormElement> List { get; set; }

            [XPath(TestSelector)]
            public virtual IList<FormControl> ControlList { get; set; }

            [XPath(TestSelector)]
            public virtual ReadOnlyCollection<FormElement> ReadOnlyCollection { get; set; }

            [XPath(TestSelector)]
            public virtual ReadOnlyCollection<FormControl> ControlReadOnlyCollection { get; set; }

            [XPath(TestSelector, CacheValue = true)]
            public virtual IEnumerable<LinkElement> Links { get; set; }
        }

        public class PageWithIncorrectPropertyCollectionTypes : SimplePageForTest
        {
            [XPath(TestSelector)]
            public virtual ButtonElement[] Array { get; set; }

            [XPath(TestSelector)]
            public virtual List<ButtonElement> List { get; set; }

            [XPath(TestSelector)]
            public virtual IEnumerable Enumerable { get; set; }

            [XPath(TestSelector)]
            public virtual IEnumerable<BaseHtmlElement> AbstractElementCollection { get; set; }

            [XPath(TestSelector)]
            public virtual IEnumerable<Control> AbstractControlCollection { get; set; }
        }

        public class PageWithWritableProperty : BasePageWithWritableProperty
        {
            [XPath(TestSelector)]
            public override IEnumerable<RadioButtonElement> RadioButtons { get; set; }
        }
    }
}