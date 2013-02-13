// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using NUnit.Framework;
using OpenQA.Selenium;
using Selenol.Elements;
using Selenol.SelectorAttributes;

namespace Selenol.Tests.SelectorAttributes
{
    [TestFixture]
    public class TestClassAttribute : BaseSelectorAttributeTest
    {
        protected override By GetByCriteria(string selectorValue)
        {
            return By.ClassName(selectorValue);
        }

        public class PageWithSelectorAttribute : SimplePageForTest
        {
            [Class(TestSelector)]
            public virtual ButtonElement Button { get; private set; }
        }

        public class PageInheritsPropertiesWithSelectorAttribute : PageWithSelectorAttribute
        {
        }

        public class PageWithIncorrectSelectorAttributeUsage : SimplePageForTest
        {
            private TextboxElement notAuthoProperty;

            [Class(TestSelector)]
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

            [Class(TestSelector)]
            public virtual TextAreaElement PropertyWithoutSetter
            {
                get
                {
                    return null;
                }
            }

            [Class(TestSelector)]
            public virtual int NotElement { get; set; }

            [Class(TestSelector)]
            public virtual BaseHtmlElement AbstractElement { get; set; }

            [Class(TestSelector)]
            public SelectElement NotVirtualProperty { get; set; }

            [Class(TestSelector)]
            public virtual TextboxElement PropertyWithoutGetter
            {
                set
                {
                    this.notAuthoProperty = value;
                }
            }

            [Class(TestSelector)]
            internal RadioButtonElement InternalProperty { get; set; }

            [Class(TestSelector)]
            // ReSharper disable UnusedMember.Local
            private CheckboxElement PrivateProperty { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        public class PageWithNullSelector : SimplePageForTest
        {
            [Class(null)]
            public virtual TextboxElement TextboxElement { get; set; } 
        }

        public class PageWithEmptySelector : SimplePageForTest
        {
            [Class("")]
            public virtual FormElement FormElement { get; set; }
        }

        public class PageWithProtectedProperty : SimplePageForTest
        {
            [Class(TestSelector)]
            protected virtual ButtonElement Button { get; set; }
        }
    }
}