// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using NUnit.Framework;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes
{
    [TestFixture]
    public class TestXPathAttribute : BaseSelectorAttributeTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.XPath(selectorValue);
        }
        
        public class PageWithSelectorAttribute : SimplePageForTest
        {
            [XPath(TestSelector)]
            public virtual ButtonElement Button { get; private set; }

            [XPath(TestSelector, CacheValue = true)]
            public virtual SelectElement Select { get; private set; }
        }

        public class PageInheritsPropertiesWithSelectorAttribute : PageWithSelectorAttribute
        {
        }

        public class PageWithIncorrectSelectorAttributeUsage : SimplePageForTest
        {
            private TextboxElement notAuthoProperty;

            [XPath(TestSelector)]
            public virtual TextboxElement NotAuthoProperty
            {
                get
                {
                    return this.notAuthoProperty;
                }

                set
                {
                    this.notAuthoProperty = value;
                }
            }

            [XPath(TestSelector)]
            public virtual TextAreaElement PropertyWithoutSetter
            {
                get
                {
                    return null;
                }
            }

            [XPath(TestSelector)]
            public virtual int NotElement { get; set; }

            [XPath(TestSelector)]
            public virtual BaseHtmlElement AbstractElement { get; set; }

            [XPath(TestSelector)]
            public SelectElement NotVirtualProperty { get; set; }

            [XPath(TestSelector)]
            public virtual TextboxElement PropertyWithoutGetter
            {
                set
                {
                    this.notAuthoProperty = value;
                }
            }

            [XPath(TestSelector)]
            internal RadioButtonElement InternalProperty { get; set; }

            [XPath(TestSelector)]
            // ReSharper disable UnusedMember.Local
            private CheckboxElement PrivateProperty { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        public class PageWithNullSelector : SimplePageForTest
        {
            [XPath(null)]
            public virtual TextboxElement TextboxElement { get; set; }
        }

        public class PageWithEmptySelector : SimplePageForTest
        {
            [XPath("")]
            public virtual FormElement FormElement { get; set; }
        }

        public class PageWithProtectedProperty : SimplePageForTest
        {
            [XPath(TestSelector)]
            protected virtual ButtonElement Button { get; set; }
        }

        public class PageWithWritableProperty : BasePageWithWritableProperty
        {
            [XPath(TestSelector)]
            public override LinkElement Link { get; set; }
        }
    }
}