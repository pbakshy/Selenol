// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using NUnit.Framework;
using Selenol.Elements;
using Selenol.Page;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes
{
    [TestFixture]
    public class TestIdAttribute : BaseSelectorAttributeTest<TestIdAttribute.PageWithSelectorAttribute,
                                       TestIdAttribute.PageInheritsPropertiesWithSelectorAttribute>
    {
        protected override ButtonElement GetButton(PageWithSelectorAttribute page)
        {
            return page.Button;
        }

        protected override BasePage CreatePageWithIncorrectAttributeUsage()
        {
            return PageFactory.Create<PageWithIncorrectSelectorAttributeUsage>(this.WebDriver, this.JavaScriptExecutor);
        }

        protected override BasePage CreatePageWithNullSelector()
        {
            return PageFactory.Create<PageWithNullSelector>(this.WebDriver, this.JavaScriptExecutor);
        }

        protected override BasePage CreatePageWithEmptySelector()
        {
            return PageFactory.Create<PageWithEmptySelector>(this.WebDriver, this.JavaScriptExecutor);
        }

        public class PageWithSelectorAttribute : SimplePageForTest
        {
            [Id(TestSelector)]
            public virtual ButtonElement Button { get; private set; }
        }

        public class PageInheritsPropertiesWithSelectorAttribute : PageWithSelectorAttribute
        {
        }

        public class PageWithIncorrectSelectorAttributeUsage : SimplePageForTest
        {
            private TextboxElement notAuthoProperty;

            [Id(TestSelector)]
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

            [Id(TestSelector)]
            public virtual TextAreaElement PropertyWithoutSetter
            {
                get
                {
                    return null;
                }
            }

            [Id(TestSelector)]
            public virtual int NotElement { get; set; }

            [Id(TestSelector)]
            public virtual BaseHtmlElement AbstractElement { get; set; }

            [Id(TestSelector)]
            public SelectElement NotVirtualProperty { get; set; }

            [Id(TestSelector)]
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
            [Id(null)]
            public virtual TextboxElement TextboxElement { get; set; } 
        }

        public class PageWithEmptySelector : SimplePageForTest
        {
            [Id("")]
            public virtual FormElement FormElement { get; set; }
        }
    }
}