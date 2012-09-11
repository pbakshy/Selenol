// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    public class TestSelectElement : BaseHtmlElementTest<SelectElement>
    {
        [Test]
        public void GetOptions()
        {
            var option1 = MockRepository.GenerateStub<IWebElement>();
            var option2 = MockRepository.GenerateStub<IWebElement>();
            var option3 = MockRepository.GenerateStub<IWebElement>();

            option1.Stub(x => x.Text).Return("a");
            option2.Stub(x => x.Text).Return("b");
            option3.Stub(x => x.Text).Return("c");

            option1.Stub(x => x.GetAttribute("value")).Return("a1");
            option2.Stub(x => x.GetAttribute("value")).Return("b2");
            option3.Stub(x => x.GetAttribute("value")).Return("c3");

            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { option1, option2, option3 }));

            //TODO: create a helper with custom comparer
            this.TypedElement.Options
                .Select(x => new Tuple<string, string>(x.Text, x.Value))
                .Should().Equal(new Tuple<string, string>("a", "a1"), new Tuple<string, string>("b", "b2"), new Tuple<string, string>("c", "c3"));
        }

        protected override SelectElement CreateElement()
        {
            return new SelectElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("select");
        }
    }
}