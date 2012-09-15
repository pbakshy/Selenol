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
    public class TestTableElement : BaseGenericContainerElementTest<TableElement>
    {
        private IWebElement row1, row2, row3;

        [Test]
        public void GetHeadRows()
        {
            this.WebElement.Stub(x => x.FindElements(By.CssSelector("thead tr")))
                .Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.row1, this.row3 }));

            this.TypedElement.HeadRows.Select(x => x.Id).Should().Equal(new[] { "row-1", "row-3" }.AsEnumerable());
        }

        [Test]
        public void GetBodyRows()
        {
            this.WebElement.Stub(x => x.FindElements(By.CssSelector("tbody tr")))
                .Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.row2, this.row3 }));

            this.TypedElement.BodyRows.Select(x => x.Id).Should().Equal(new[] { "row-2", "row-3" }.AsEnumerable());
        }

        [Test]
        public void GetFooterRows()
        {
            this.WebElement.Stub(x => x.FindElements(By.CssSelector("tfoot tr")))
                .Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.row2, this.row1 }));

            this.TypedElement.FooterRows.Select(x => x.Id).Should().Equal(new[] { "row-2", "row-1" }.AsEnumerable());
        }

        [Test]
        public void GetAllRows()
        {
            this.WebElement.Stub(x => x.FindElements(By.TagName("tr")))
                .Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { this.row1, this.row2, this.row3 }));

            this.TypedElement.AllRows.Select(x => x.Id).Should().Equal(new[] { "row-1", "row-2", "row-3" }.AsEnumerable());
        }

        protected override TableElement CreateElement()
        {
            return new TableElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("table");
        }

        protected override void OnItit()
        {
            this.row1 = MockRepository.GenerateStub<IWebElement>();
            this.row2 = MockRepository.GenerateStub<IWebElement>();
            this.row3 = MockRepository.GenerateStub<IWebElement>();

            this.row1.Stub(x => x.TagName).Return("tr");
            this.row2.Stub(x => x.TagName).Return("tr");
            this.row3.Stub(x => x.TagName).Return("tr");

            this.row1.Stub(x => x.GetAttribute("id")).Return("row-1");
            this.row2.Stub(x => x.GetAttribute("id")).Return("row-2");
            this.row3.Stub(x => x.GetAttribute("id")).Return("row-3");
        }
    }
}