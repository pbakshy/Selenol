// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

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
    [TestFixture]
    public class TestListboxElement : BaseSelectElementTest<ListboxElement>
    {
        [Test]
        public void GetTheOnlySelectedOption()
        {
            this.SetSelectedAndOptions();
            this.TypedElement.SelectedOptions.Select(x => x.Value).Should().Equal(new[] { "b2" }.AsEnumerable());
        }

        [Test]
        public void GetSelectedOptions()
        {
            this.Option1.Stub(x => x.GetAttribute("selected")).Return(null);
            this.Option2.Stub(x => x.GetAttribute("selected")).Return(string.Empty);
            this.Option3.Stub(x => x.GetAttribute("selected")).Return("selected");

            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.Option1, this.Option2, this.Option3 }));

            this.TypedElement.SelectedOptions.Select(x => x.Value).Should().Equal(new[] { "b2", "c3" }.AsEnumerable());
        }

        [Test]
        public void GetDefaultSelectedOption()
        {
            this.Option1.Stub(x => x.GetAttribute("selected")).Return(null);
            this.Option2.Stub(x => x.GetAttribute("selected")).Return(null);
            this.Option3.Stub(x => x.GetAttribute("selected")).Return(null);

            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.Option1, this.Option2, this.Option3 }));
            this.TypedElement.SelectedOptions.Should().BeEmpty();
        }

        [Test]
        public void GetSelectedOptionFromEmptySelect()
        {
            this.WebElement.Stub(x => x.FindElements(By.TagName("option"))).Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));
            this.TypedElement.SelectedOptions.Should().BeEmpty();
        }

        protected override ListboxElement CreateElement()
        {
            return new ListboxElement(this.WebElement);
        }
    }
}