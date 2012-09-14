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
    public class TestTableRowElement : BaseHtmlElementTest<TableRowElement>
    {
        [Test]
        public void GetCells()
        {
            var cell1 = MockRepository.GenerateStub<IWebElement>();
            var cell2 = MockRepository.GenerateStub<IWebElement>();
            var cell3 = MockRepository.GenerateStub<IWebElement>();

            cell1.Stub(x => x.TagName).Return("td");
            cell2.Stub(x => x.TagName).Return("th");
            cell3.Stub(x => x.TagName).Return("td");

            cell1.Stub(x => x.GetAttribute("id")).Return("cell-1");
            cell2.Stub(x => x.GetAttribute("id")).Return("cell-2");
            cell3.Stub(x => x.GetAttribute("id")).Return("cell-3");

            this.WebElement.Stub(x => x.FindElements(By.CssSelector("td,th")))
                .Return(new ReadOnlyCollection<IWebElement>(new List<IWebElement> { cell1, cell2, cell3 }));

            this.TypedElement.Cells.Select(x => x.Id).Should().Equal(new[] { "cell-1", "cell-2", "cell-3" }.AsEnumerable());
        }

        protected override TableRowElement CreateElement()
        {
            return new TableRowElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("tr");
        }
    }
}