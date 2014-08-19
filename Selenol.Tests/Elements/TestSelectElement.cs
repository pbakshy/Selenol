// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    public class TestSelectElement : BaseSelectElementTest<SelectElement>
    {
        [Test]
        public void GetSelectedOption()
        {
            this.SetSelectedAndOptions();
            this.TypedElement.SelectedOption.Value.Should().Be("b2");
        }

        [Test]
        public void GetDefaultSelectedOption()
        {
            this.Option1.Stub(x => x.GetAttribute("selected")).Return(null);
            this.Option2.Stub(x => x.GetAttribute("selected")).Return(null);
            this.Option3.Stub(x => x.GetAttribute("selected")).Return(null);

            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.Option1, this.Option2, this.Option3 }));
            this.TypedElement.SelectedOption.Value.Should().Be("a1");
        }

        [Test]
        public void GetSelectedOptionFromEmptySelect()
        {
            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));
            this.TypedElement.SelectedOption.Should().BeNull();
        }

        protected override SelectElement CreateElement()
        {
            return new SelectElement(this.WebElement);
        }
    }
}