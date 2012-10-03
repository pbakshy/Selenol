// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Drawing;

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestElementStyles
    {
        private IWebElement webElement;

        private ElementStyles styles;

        [SetUp]
        public void Init()
        {
            this.webElement = MockRepository.GenerateStub<IWebElement>();
            this.styles = new ElementStyles(this.webElement);
        }

        [Test] 
        public void GetWidth()
        {
            this.webElement.Expect(x => x.Size).Return(new Size(150, 100));
            this.styles.Width.Should().Be(150);
        }

        [Test]
        public void GetHeight()
        {
            this.webElement.Expect(x => x.Size).Return(new Size(150, 100));
            this.styles.Height.Should().Be(100);
        }

        [Test]
        public void GetStyle()
        {
            this.webElement.Expect(x => x.GetCssValue("font-size")).Return("12pt");
            this.styles.GetStyle("font-size").Should().Be("12pt");
        }

        [Test]
        public void GetNotExistingStyle()
        {
            this.webElement.Expect(x => x.GetCssValue("font-size")).Return(null);
            this.styles.GetStyle("font-size").Should().Be(string.Empty);
        }

        [Test, TestCaseSource("GetColorProperty")]
        public void GetColorHashColor(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return("#f1b203");
            accessor(this.styles).Should().Be(Color.FromArgb(241, 178, 3));
        }

        [Test, TestCaseSource("GetColorProperty")]
        public void GetColorShortHashColor(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return("#fb3");
            accessor(this.styles).Should().Be(Color.FromArgb(255, 187, 51));
        }

        [Test, TestCaseSource("GetColorProperty")]
        public void GetColorRGB(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return("rgb(112, 80, 250)");
            accessor(this.styles).Should().Be(Color.FromArgb(112, 80, 250));
        }

        [Test, TestCaseSource("GetColorProperty")]
        public void GetColorRGBA(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return("rgba(112, 80, 0, 0.7)");
            accessor(this.styles).Should().Be(Color.FromArgb(178, 112, 80, 0));
        }

        [Test, TestCaseSource("GetColorProperty"), Ignore("Was not implemented yet.")]
        public void GetColorRGBPercents(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return("rgb(50%, 0, 25%)");
            accessor(this.styles).Should().Be(Color.FromArgb(128, 0, 64));
        }

        [Test, TestCaseSource("GetColorProperty"), Ignore("Was not implemented yet.")]
        public void GetColorRGBAPercents(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return("rgba(50%, 0, 25%, 0.5)");
            accessor(this.styles).Should().Be(Color.FromArgb(128, 128, 0, 64));
        }

        [Test, TestCaseSource("GetColorProperty"), Ignore("Was not implemented yet.")]
        public void GetColorHSL(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return("hsl(344, 88%, 33%)");
            accessor(this.styles).Should().Be(Color.FromArgb(156, 10, 50));
        }

        [Test, TestCaseSource("GetColorProperty")]
        public void GetColorKnownColor(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return("lightgray");
            accessor(this.styles).Should().Be(Color.LightGray);
        }

        [Test, TestCaseSource("GetColorProperty")]
        public void GetColorDoesNotExist(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return(string.Empty);
            accessor(this.styles).Should().Be(Color.Empty);
        }

        [Test, TestCaseSource("GetColorProperty"), ExpectedException(typeof(InvalidValueException))]
        public void GetColorIncorrectValue(string style, Func<ElementStyles, Color> accessor)
        {
            this.webElement.Expect(x => x.GetCssValue(style)).Return("something wrong");
            accessor(this.styles);
        }

        protected IEnumerable<TestCaseData> GetColorProperty()
        {
            yield return new TestCaseData("color", new Func<ElementStyles, Color>(x => x.ForegroundColor));
            yield return new TestCaseData("background-color", new Func<ElementStyles, Color>(x => x.BackgroundColor));
        }
    }
}