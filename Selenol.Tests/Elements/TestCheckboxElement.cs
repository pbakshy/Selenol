// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestCheckboxElement : BaseHtmlElementTest<CheckboxElement>
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
        public void CheckInUncheckedState()
        {
            this.WebElement.Stub(x => x.GetAttribute("checked")).Return(null);
            this.TypedElement.Check();
            this.WebElement.AssertWasCalled(x => x.Click());
        }

        [Test]
        public void CheckInCheckedState()
        {
            this.WebElement.Stub(x => x.GetAttribute("checked")).Return(string.Empty);
            this.TypedElement.Check();
            this.WebElement.AssertWasNotCalled(x => x.Click());
        }

        [Test]
        public void UncheckInUncheckedState()
        {
            this.WebElement.Stub(x => x.GetAttribute("checked")).Return(null);
            this.TypedElement.Uncheck();
            this.WebElement.AssertWasNotCalled(x => x.Click());
        }

        [Test]
        public void UncheckInCheckedState()
        {
            this.WebElement.Stub(x => x.GetAttribute("checked")).Return(string.Empty);
            this.TypedElement.Uncheck();
            this.WebElement.AssertWasCalled(x => x.Click());
        }

        [Test]
        public void ToggleCheckInCheckedState()
        {
            this.WebElement.Stub(x => x.GetAttribute("checked")).Return(null);
            this.TypedElement.ToggleCheck();
            this.WebElement.AssertWasCalled(x => x.Click());
        }

        [Test]
        public void ToggleCheckInUncheckedState()
        {
            this.WebElement.Stub(x => x.GetAttribute("checked")).Return(string.Empty);
            this.TypedElement.ToggleCheck();
            this.WebElement.AssertWasCalled(x => x.Click());
        }

        protected override CheckboxElement CreateElement()
        {
            return new CheckboxElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("input");
            this.WebElement.Stub(x => x.GetAttribute("type")).Return("checkbox");
        }
    }
}