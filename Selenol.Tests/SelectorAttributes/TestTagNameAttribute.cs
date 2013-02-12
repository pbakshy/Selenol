// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes
{
    public class TestTagNameAttribute : BaseSelectorAttributeTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.TagName(selectorValue);
        }

        public class PageWithSelectorAttribute : SimplePageForTest
        {
            [TagName(TestSelector)]
            public virtual ButtonElement Button { get; private set; }
        }

        public class PageInheritsPropertiesWithSelectorAttribute : PageWithSelectorAttribute
        {
        }

        public class PageWithIncorrectSelectorAttributeUsage : SimplePageForTest
        {
            private TextboxElement notAuthoProperty;

            [TagName(TestSelector)]
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

            [TagName(TestSelector)]
            public virtual TextAreaElement PropertyWithoutSetter
            {
                get
                {
                    return null;
                }
            }

            [TagName(TestSelector)]
            public virtual int NotElement { get; set; }

            [TagName(TestSelector)]
            public virtual BaseHtmlElement AbstractElement { get; set; }

            [TagName(TestSelector)]
            public SelectElement NotVirtualProperty { get; set; }

            [TagName(TestSelector)]
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
            [TagName(null)]
            public virtual TextboxElement TextboxElement { get; set; }
        }

        public class PageWithEmptySelector : SimplePageForTest
        {
            [TagName("")]
            public virtual FormElement FormElement { get; set; }
        }
    }
}