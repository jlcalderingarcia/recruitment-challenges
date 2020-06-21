namespace Refactoring.FraudDetection.Tests.Parsing
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Exceptions;
    using Refactoring.FraudDetection.Parsing;

    [TestClass]
    public class OrderLineParserTests
    {
        private readonly OrderLineParser _orderLineParser;

        public OrderLineParserTests()
        {
            _orderLineParser = new OrderLineParser();
        }

        [TestMethod]
        public void ParseOrderLine_GivenValidLinesLine_ThenCreateOrderWithValues()
        {
            // Arrange
            int orderId = 1;
            int dealId = 2;
            string email = "a@a.com";
            string street = "street";
            string city = "city";
            string state = "state";
            string zipCode = "32500";
            string creditCard = "1234-1234-1234-1234";
            string line = $"{orderId},{dealId},{email},{street},{city},{state},{zipCode},{creditCard}";

            // Act
            var order = _orderLineParser.ParseOrderLine(line);

            // Assert
            order.Should().NotBeNull();
            order.OrderId.Should().Be(orderId);
            order.DealId.Should().Be(dealId);
            order.Email.Should().Be(email);
            order.Street.Should().Be(street);
            order.City.Should().Be(city);
            order.State.Should().Be(state);
            order.ZipCode.Should().Be(zipCode);
            order.CreditCard.Should().Be(creditCard);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("\n")]
        [DataRow("1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010,a")]
        [DataRow("1,1,bugs@bunny.com,123 Sesame St.,New York,NY")]
        [DataRow("a,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010")]
        [DataRow("1,a,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010")]
        [DataRow("0,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010")]
        [DataRow("-1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010")]
        [DataRow("1,0,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010")]
        [DataRow("1,-1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010")]
        [DataRow("1,1, ,123 Sesame St.,New York,NY,10011,12345689010")]
        [DataRow("1,1,bugs@bunny.com,  ,New York,NY,10011,12345689010")]
        [DataRow("1,1,bugs@bunny.com,123 Sesame St.,  ,NY,10011,12345689010")]
        [DataRow("1,1,bugs@bunny.com,123 Sesame St.,New York,  ,10011,12345689010")]
        [DataRow("1,1,bugs@bunny.com,123 Sesame St.,New York,NY,  ")]
        public void ParseOrderLine_GivenInvalidLinesLine_ThenThroValidationException(string orderLine)
        {
            // Act
            Action action = () => _orderLineParser.ParseOrderLine(orderLine);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ValidationException>();
        }
    }
}
