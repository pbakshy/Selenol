// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using NUnit.Framework;
using OpenQA.Selenium;
using Selenol.Controls;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes
{
    [TestFixture]
    public class TestCssAttribute : BaseSelectorAttributeTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.CssSelector(selectorValue);
        }
        
        public class PageWithSelectorAttribute : SimplePageForTest
        {
            [Css(TestSelector)]
            public virtual ButtonElement Button { get; private set; }

            [Css(TestSelector, CacheValue = true)]
            public virtual SelectElement Select { get; private set; }

            [Css(TestSelector)]
            public virtual TableControl TableControl { get; private set; }

            [Css(TestSelector, CacheValue = true)]
            public virtual TableControl CachedControl { get; private set; }
        }

        public class PageInheritsPropertiesWithSelectorAttribute : PageWithSelectorAttribute
        {
        }

        public class PageWithIncorrectSelectorAttributeUsage : SimplePageForTest
        {
            private TextboxElement notAuthoProperty;

            [Css(TestSelector)]
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

            [Css(TestSelector)]
            public virtual TextAreaElement PropertyWithoutSetter
            {
                get
                {
                    return null;
                }
            }

            [Css(TestSelector)]
            public virtual int InvalidType { get; set; }

            [Css(TestSelector)]
            public virtual BaseHtmlElement AbstractElement { get; set; }

            [Css(TestSelector)]
            public virtual Control AbstractControl { get; set; }

            [Css(TestSelector)]
            public SelectElement NotVirtualProperty { get; set; }

            [Css(TestSelector)]
            public virtual TextboxElement PropertyWithoutGetter
            {
                set
                {
                    this.notAuthoProperty = value;
                }
            }

            [Css(TestSelector)]
            internal RadioButtonElement InternalProperty { get; set; }

            [Css(TestSelector)]
// ReSharper disable UnusedMember.Local
            private CheckboxElement PrivateProperty { get; set; }
// ReSharper restore UnusedMember.Local
        }

        public class PageWithNullSelector : SimplePageForTest
        {
            [Css(null)]
            public virtual TextboxElement TextboxElement { get; set; }
        }

        public class PageWithEmptySelector : SimplePageForTest
        {
            [Css("")]
            public virtual FormElement FormElement { get; set; }
        }

        public class PageWithProtectedProperty : SimplePageForTest
        {
            [Css(TestSelector)]
            protected virtual ButtonElement Button { get; set; }

            [Css(TestSelector)]
            protected virtual TableControl TableControl { get; set; }
        }

        public class PageWithWritableProperty : BasePageWithWritableProperty
        {
            [Css(TestSelector)]
            public override LinkElement Link { get; set; }
        }
    }
}