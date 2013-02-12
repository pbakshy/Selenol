// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes
{
    public class TestNameAttribute : BaseSelectorAttributeTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.Name(selectorValue);
        }

        public class PageWithSelectorAttribute : SimplePageForTest
        {
            [Name(TestSelector)]
            public virtual ButtonElement Button { get; private set; }
        }

        public class PageInheritsPropertiesWithSelectorAttribute : PageWithSelectorAttribute
        {
        }

        public class PageWithIncorrectSelectorAttributeUsage : SimplePageForTest
        {
            private TextboxElement notAuthoProperty;

            [Name(TestSelector)]
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

            [Name(TestSelector)]
            public virtual TextAreaElement PropertyWithoutSetter
            {
                get
                {
                    return null;
                }
            }

            [Name(TestSelector)]
            public virtual int NotElement { get; set; }

            [Name(TestSelector)]
            public virtual BaseHtmlElement AbstractElement { get; set; }

            [Name(TestSelector)]
            public SelectElement NotVirtualProperty { get; set; }

            [Name(TestSelector)]
            public virtual TextboxElement PropertyWithoutGetter
            {
                set
                {
                    this.notAuthoProperty = value;
                }
            }
        }

        public class PageWithNullSelector : SimplePageForTest
        {
            [Name(null)]
            public virtual TextboxElement TextboxElement { get; set; }
        }

        public class PageWithEmptySelector : SimplePageForTest
        {
            [Name("")]
            public virtual FormElement FormElement { get; set; }
        }
    }
}