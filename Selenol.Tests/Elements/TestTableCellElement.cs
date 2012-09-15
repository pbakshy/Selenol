// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestTableCellElement : BaseGenericContainerElementTest<TableCellElement>
    {
        private TableElement parentTable;

        private TableRowElement parentRow;

        [Test]
        public void GetParent()
        {
            this.TypedElement.Parent.Should().Be(this.parentRow);
        }

        [Test]
        public void GetIndex()
        {
            this.TypedElement.Index.Should().Be(2);
        }

        protected override TableCellElement CreateElement()
        {
            var element = MockRepository.GenerateStub<IWebElement>();
            element.Stub(x => x.TagName).Return("table");
            this.parentTable = new TableElement(element);

            element = MockRepository.GenerateStub<IWebElement>();
            element.Stub(x => x.TagName).Return("tr");
            this.parentRow = new TableRowElement(element, this.parentTable, 1);
            return new TableCellElement(this.WebElement, this.parentRow, 2);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("td");
        }
    }
}