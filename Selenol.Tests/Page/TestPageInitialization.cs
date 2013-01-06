using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using Selenol.Page;

namespace Selenol.Tests.Page
{
    [TestFixture]
    public class TestPageInitialization
    {
        [Test]
        public void AllPublicPropertiesCheckIfPageHasBeenInitialized()
        {
            var page = new SimplePageForTest();
            var properties = typeof(BasePage).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var propertyInfo in properties)
            {
                var info = propertyInfo;
                var exception = Assert.Throws<TargetInvocationException>(() => info.GetValue(page, null));
                exception.InnerException.Should().BeOfType<PageInitializationException>("because page was initialized incorrect.");
            }
        }

        [Test]
        public void AllPublicMethodsCheckIfPageHasBeenInitialized()
        {
            var publicMethodCallExpressions = new Expression<Action<BasePage>>[]
                {
                    x => x.ExecuteScript("return 1;"),
                    x => x.ExecuteAsyncScript("return 2;")
                };
            var page = new SimplePageForTest();
            var methods = typeof(BasePage).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(x => !x.IsSpecialName);
            var coveredMethods = publicMethodCallExpressions.Select(x => x.Body).Cast<MethodCallExpression>().Select(x => x.Method.Name);
            var uncoveredMethods = methods.Where(x => !coveredMethods.Contains(x.Name)).Select(x => x.Name).ToArray();

            Assert.IsEmpty(uncoveredMethods, "'{0}' methods does not covered. Please add expressions to test them.", string.Join(", ", uncoveredMethods));
            foreach (var publicMethodCallExpression in publicMethodCallExpressions)
            {
                Assert.Throws<PageInitializationException>(() => publicMethodCallExpression.Compile()(page));
            }
        }
    }
}