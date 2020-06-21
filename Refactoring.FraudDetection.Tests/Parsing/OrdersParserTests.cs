using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactoring.FraudDetection.Entities;
using Refactoring.FraudDetection.Exceptions;
using Refactoring.FraudDetection.Parsing;
using Refactoring.FraudDetection.Validation;

namespace Refactoring.FraudDetection.Tests.Parsing
{
    [TestClass]
    public class OrdersParserTests
    {
        private readonly Mock<IOrderLineParser> _orderLineParserMock;
        private readonly IOrdersParser _ordersParser;

        public OrdersParserTests()
        {
            _orderLineParserMock = new Mock<IOrderLineParser>();

            _ordersParser = new OrdersParser(
                    _orderLineParserMock.Object
                );
        }

        [TestMethod]
        public void Ctor_GivenNullOrderLineParser_ThenThrowArgumentNullException()
        {
            // Act
            Action action = () => new OrdersParser(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ParseLines_GivenNullOrderLines_ThenThrowArgumentNullException()
        {
            // Act
            Action action = () => _ordersParser.ParseOrders(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ParseLines_GivenInvalidOrderLines_ThenThrowValidationException()
        {
            // Arrange
            var orderLine = "orderLine";
            var validationException = new ValidationException(
                    new ValidationResult(new List<ValidationError> {
                        new ValidationError("field", "error")
                    })
                );
            _orderLineParserMock.Setup(it => it.ParseOrderLine(orderLine))
                .Throws(validationException);

            // Act
            Action action = () => _ordersParser.ParseOrders(new List<string> { orderLine });

            // Assert
            action.Should().Throw<ValidationException>();
            _orderLineParserMock.Verify(it => it.ParseOrderLine(orderLine), Times.Once);
        }

        [TestMethod]
        public void ParseLines_GivenInvalidOrderLineId_ThenThrowValidationException()
        {
            // Arrange
            var orderLine = "orderLine";
            var validationException = new ValidationException(
                    new ValidationResult(new List<ValidationError> {
                        new ValidationError("field", "error")
                    })
                );
            _orderLineParserMock.Setup(it => it.ParseOrderLine(orderLine))
                .Returns(new Order(
                        2,
                        2,
                        "a@a.com",
                        "street",
                        "city",
                        "state",
                        "32500",
                        "1234-1234-1234-1234"
                    ));

            // Act
            Action action = () => _ordersParser.ParseOrders(new List<string> { orderLine });

            // Assert
            action.Should().Throw<ValidationException>();
            _orderLineParserMock.Verify(it => it.ParseOrderLine(orderLine), Times.Once);
        }

        [TestMethod]
        public void ParseLines_GivenRepeatedOrderLineIds_ThenThrowValidationException()
        {
            // Arrange
            var orderLine = "orderLine";
            var validationException = new ValidationException(
                    new ValidationResult(new List<ValidationError> {
                        new ValidationError("field", "error")
                    })
                );
            _orderLineParserMock.Setup(it => it.ParseOrderLine(orderLine))
                .Returns(new Order(
                        1,
                        2,
                        "a@a.com",
                        "street",
                        "city",
                        "state",
                        "32500",
                        "1234-1234-1234-1234"
                    ));

            // Act
            Action action = () => _ordersParser.ParseOrders(new List<string> { orderLine, orderLine });

            // Assert
            action.Should().Throw<ValidationException>();
            _orderLineParserMock.Verify(it => it.ParseOrderLine(orderLine), Times.Exactly(2));
        }

        [TestMethod]
        public void ParseLines_GivenValidOrderLines_ThenReturnParsedOrderLines()
        {
            // Arrange
            var orderLine1 = "orderLine1";
            var orderLine2 = "orderLine2";
            var order1 = new Order(
                        1,
                        1,
                        "a@a.com",
                        "street1",
                        "city1",
                        "state1",
                        "32500",
                        "1234-1234-1234-1234"
                    );
            var order2 = new Order(
                        2,
                        2,
                        "b@a.com",
                        "street2",
                        "city2",
                        "state2",
                        "32500",
                        "1234-1234-1234-1235"
                    );
            var validationException = new ValidationException(
                    new ValidationResult(new List<ValidationError> {
                        new ValidationError("field", "error")
                    })
                );
            _orderLineParserMock.Setup(it => it.ParseOrderLine(orderLine1))
                .Returns(order1);
            _orderLineParserMock.Setup(it => it.ParseOrderLine(orderLine2))
                .Returns(order2);

            // Act
            var result = _ordersParser.ParseOrders(new List<string> { orderLine1, orderLine2 });

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(new List<Order> { order1, order2 });
            _orderLineParserMock.Verify(it => it.ParseOrderLine(orderLine1), Times.Once);
            _orderLineParserMock.Verify(it => it.ParseOrderLine(orderLine2), Times.Once);
        }
    }
}
