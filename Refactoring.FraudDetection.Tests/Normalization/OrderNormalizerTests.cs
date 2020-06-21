namespace Refactoring.FraudDetection.Tests.Normalization
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Entities;
    using Refactoring.FraudDetection.Normalization;

    [TestClass]
    public class OrderNormalizerTests
    {
        private readonly OrderNormalizer _orderNormalizer;

        public OrderNormalizerTests()
        {
            _orderNormalizer = new OrderNormalizer();
        }

        [TestMethod]
        public void Normalize_GivenNullOrder_ThenThrowArgumentNullException()
        {
            // Act
            Action action = () => _orderNormalizer.Normalize(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow("a@@a.com", "street", "state", "a@a.com", "street", "state")]
        [DataRow("a.a@a.com", "street", "state", "aa@a.com", "street", "state")]
        [DataRow("a.a+b.b@a.com", "street", "state", "aa+@a.com", "street", "state")]
        [DataRow("a@a_a.a.com", "street", "state", "a@a_a.a.com", "street", "state")]
        [DataRow("a@a.com", "main st.", "state", "a@a.com", "main street", "state")]
        [DataRow("a@a.com", "main rd.", "state", "a@a.com", "main road", "state")]
        [DataRow("a@a.com", "street", "il", "a@a.com", "street", "illinois")]
        [DataRow("a@a.com", "street", "ca", "a@a.com", "street", "california")]
        [DataRow("a@a.com", "street", "ny", "a@a.com", "street", "new york")]
        public void Normalize_GivenNormalizableValues_ThenCreateValidNormalizedOrder(
            string email,
            string street,
            string state,
            string expectedEmail,
            string expectedStreet,
            string expectedState
        )
        {
            // Arrange
            var order = new Order(
                    1,
                    2,
                    email,
                    street,
                    "city",
                    state,
                    "32500",
                    "1234-1234-1234-1234"
                );

            // Act
            var normalizedOrder = _orderNormalizer.Normalize(order);

            // Assert
            normalizedOrder.Should().NotBeNull();
            normalizedOrder.Email.Should().Be(expectedEmail);
            normalizedOrder.Street.Should().Be(expectedStreet);
            normalizedOrder.State.Should().Be(expectedState);
            normalizedOrder.OrderId.Should().Be(order.OrderId);
            normalizedOrder.DealId.Should().Be(order.DealId);
            normalizedOrder.City.Should().Be(order.City);
            normalizedOrder.ZipCode.Should().Be(order.ZipCode);
            normalizedOrder.CreditCard.Should().Be(order.CreditCard);
        }
    }
}
