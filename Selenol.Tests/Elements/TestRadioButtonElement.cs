// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestRadioButtonElement : BaseHtmlElementTest<RadioButtonElement>
    {
        [Test]
        [TestCase("checked")]
        [TestCase("")]
        [TestCase("abc")]
        public void GetIsCheckedTrue(string value)
        {
            this.WebElement.Stub(x => x.GetAttribute("checked")).Return(value);
            this.TypedElement.IsChecked.Should().BeTrue();
        }

        [Test]
        public void GetIsCheckedFalse()
        {
            this.WebElement.Stub(x => x.GetAttribute("checked")).Return(null);
            this.TypedElement.IsChecked.Should().BeFalse();
        }

        [Test]
        public void GetValue()
        {
            this.WebElement.Stub(x => x.GetAttribute("value")).Return("abc");
            this.TypedElement.Value.Should().Be("abc");
        }

        [Test]
        public void GetNotExistingValue()
        {
            this.WebElement.Stub(x => x.GetAttribute("value")).Return(null);
            this.TypedElement.Value.Should().Be(string.Empty);
        }

        [Test]
        public void Check()
        {
            this.TypedElement.Check();
            this.WebElement.AssertWasCalled(x => x.Click());
        }

        protected override RadioButtonElement CreateElement()
        {
            return new RadioButtonElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("input");
            this.WebElement.Stub(x => x.GetAttribute("type")).Return("radio");
        }
    }
}