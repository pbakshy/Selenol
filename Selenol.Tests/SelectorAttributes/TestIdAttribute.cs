// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using NUnit.Framework;
using OpenQA.Selenium;
using Selenol.Controls;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes
{
    [TestFixture]
    public class TestIdAttribute : BaseSelectorAttributeTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.Id(selectorValue);
        }

        public class PageWithSelectorAttribute : SimplePageForTest
        {
            [Id(TestSelector)]
            public virtual ButtonElement Button { get; private set; }

            [Id(TestSelector, CacheValue = true)]
            public virtual SelectElement Select { get; private set; }

            [Id(TestSelector)]
            public virtual TableControl TableControl { get; private set; }

            [Id(TestSelector, CacheValue = true)]
            public virtual TableControl CachedControl { get; private set; }
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
            public virtual int InvalidType { get; set; }

            [Id(TestSelector)]
            public virtual BaseHtmlElement AbstractElement { get; set; }

            [Id(TestSelector)]
            public virtual Control AbstractControl { get; set; }

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

            [Id(TestSelector)]
            internal RadioButtonElement InternalProperty { get; set; }

            [Id(TestSelector)]
            // ReSharper disable UnusedMember.Local
            private CheckboxElement PrivateProperty { get; set; }
            // ReSharper restore UnusedMember.Local
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

        public class PageWithProtectedProperty : SimplePageForTest
        {
            [Id(TestSelector)]
            protected virtual ButtonElement Button { get; set; }

            [Id(TestSelector)]
            protected virtual TableControl TableControl { get; set; }
        }

        public class PageWithWritableProperty : BasePageWithWritableProperty
        {
            [Id(TestSelector)]
            public override LinkElement Link { get; set; }
        }
    }
}